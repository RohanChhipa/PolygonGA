using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MersenneTwister;
using PolygonGa.Algorithm.Crossover;
using PolygonGa.Algorithm.Fitness;
using PolygonGa.Algorithm.Mutation;
using PolygonGa.Algorithm.Selection;
using Serilog;

namespace PolygonGa.Algorithm
{
    public class AlgorithmRunner
    {
        private readonly GaParameters _parameters;
        private readonly ILogger _logger;
        private readonly Image _targetImage;

        private readonly IFitnessFunction _fitnessFunction;
        private readonly ICrossover _crossover;
        private readonly ISelection _selection;
        private readonly IList<IMutation> _mutations;
        private readonly string _outputDirectory;

        public AlgorithmRunner(GaParameters parameters, ILogger logger)
        {
            _parameters = parameters;
            _logger = logger;

            _logger.Information("Loading image {Image}", _parameters.TargetImage);
            _targetImage = Image.FromFile(_parameters.TargetImage);
            _logger.Information("Image size - Width {Width} Height {Height}", _targetImage.Width, _targetImage.Height);

            _fitnessFunction = new MseFitnessFunction(_targetImage);
            _crossover = new SinglePointCrossover();
            _selection = new TournamentSelection(_parameters.TournamentSize);
            _mutations = new List<IMutation>
            {
                new PolygonMoveMutation(_targetImage.Size),
                new PolygonRotateMutation(),
                new PolygonColourMutation(),
                new PolygonSwapMutation(),
                new PolygonShrinkMutation(),
                new PolygonGrowMutation(),
                new PolygonShearMutationMutation(),
            };

            _outputDirectory = $"Output/{_parameters.ExecutionId}";
        }

        public async Task Run()
        {
            _logger.Information($"Creating output image directory {_outputDirectory}");
            Directory.CreateDirectory(_outputDirectory);

            var population = InitPopulation();
            await CalculateFitness(population);

            var bestChromosome = population.First();
            for (var gen = 0; gen < _parameters.Generations; gen++)
            {
                population = Crossover(population);
                Mutation(population);

                await CalculateFitness(population);
                population = population.OrderBy(chromosome => chromosome.Fitness).ToList();

                bestChromosome = population.First();
                _logger.Information("Generation {Gen} best chromosome {Fitness}", gen, bestChromosome.Fitness);

                if (gen % 200 == 0)
                {
                    SaveSolution(bestChromosome);
                }
            }

            SaveSolution(bestChromosome);
        }

        private List<Chromosome> InitPopulation()
        {
            _logger.Information("Creating population of size {size}", _parameters.PopulationSize);
            return Enumerable.Range(0, _parameters.PopulationSize)
                .Select(_ => new Chromosome(_targetImage.Size, _parameters.PolygonCap, _parameters.PointCap))
                .ToList();
        }

        private async Task CalculateFitness(List<Chromosome> population)
        {
            // foreach (var chromosome in population)
            // {
            //     using var image = GenerateImage(chromosome, _targetImage.Size.Width, _targetImage.Size.Height);
            //     chromosome.Fitness = _fitnessFunction.Calculate(image);
            //     // _logger.Information("Fitness {fitness}", chromosome.Fitness);
            // }

            // var tasks = population.Select(chromosome => new
            //     {
            //         Chromosome = chromosome,
            //         Image = GenerateImage(chromosome, _targetImage.Size)
            //     }).Select(arg => Task.Run(() => { arg.Chromosome.Fitness = _fitnessFunction.Calculate(arg.Image); }))
            //     .ToList();

            var groupSize = 5;
            var width = _targetImage.Width;
            var height = _targetImage.Height;

            var tasks = Enumerable.Range(0, (int) Math.Ceiling(population.Count / (double) groupSize))
                .Select(i => population.Skip(i * groupSize).Take(groupSize).ToList())
                .Select(list =>
                {
                    return Task.Run(() =>
                    {
                        foreach (var chromosome in list)
                        {
                            var image = GenerateImage(chromosome, width, height);
                            chromosome.Fitness = _fitnessFunction.Calculate(image);
                        }
                    });
                });

            await Task.WhenAll(tasks);
        }

        private List<Chromosome> Crossover(List<Chromosome> population)
        {
            var children = new List<Chromosome>();
            while (children.Count != population.Count)
            {
                var parentA = _selection.Apply(population);
                var parentB = _selection.Apply(population);

                var (childA, childB) = _crossover.Apply(parentA, parentB);
                children.Add(childA);
                children.Add(childB);
            }

            return children;
        }

        private void Mutation(List<Chromosome> population)
        {
            foreach (var chromosome in population)
            {
                foreach (var mutation in _mutations)
                {
                    if (Randoms.NextDouble() < _parameters.MutationRate)
                        mutation.Apply(chromosome);
                }
            }
        }

        private Bitmap GenerateImage(Chromosome chromosome, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.Black);

            foreach (var polygon in chromosome.Polygons)
            {
                var fromArgb = Color.FromArgb(
                    polygon.Rgb[0],
                    polygon.Rgb[1],
                    polygon.Rgb[2]);
                var brush = new SolidBrush(fromArgb
                );
                graphics.FillPolygon(brush, polygon.Points.ToArray());
            }

            return bitmap;
        }

        private void SaveSolution(Chromosome chromosome)
        {
            _logger.Information("Generating final image with fitness {Fitness} and polygons {Polygons}",
                chromosome.Fitness, chromosome.Polygons.Count);
            var generatedImage = GenerateImage(chromosome, _targetImage.Width, _targetImage.Height);

            var destinationPath = $"{_outputDirectory}/{DateTimeOffset.Now.Ticks} - {chromosome.Fitness}.jpg";
            _logger.Information("Saving image to {Path}", destinationPath);
            generatedImage.Save(destinationPath);
        }
    }
}
