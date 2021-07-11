using System;
using System.Collections.Generic;
using System.Drawing;
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

        private readonly FitnessFunction _fitnessFunction;
        private readonly TwoPointCrossover _crossover;
        private readonly TournamentSelection _selection;
        private readonly IList<IMutation> _mutations;

        public AlgorithmRunner(GaParameters parameters, ILogger logger)
        {
            _parameters = parameters;
            _logger = logger;

            _logger.Information("Loading image {Image}", _parameters.TargetImage);
            _targetImage = Image.FromFile(_parameters.TargetImage);

            _logger.Information("Image size - Width {Width} Height {Height}", _targetImage.Width, _targetImage.Height);

            _fitnessFunction = new FitnessFunction(_targetImage);
            _crossover = new TwoPointCrossover();
            _selection = new TournamentSelection(_parameters.TournamentSize);
            _mutations = new List<IMutation>
            {
                new PolygonColourMutation(),
                new PolygonSwapMutation(),
                new PolygonSizeMutation()
            };
        }

        public async Task Run()
        {
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
            foreach (var chromosome in population)
            {
                using var image = GenerateImage(chromosome, _targetImage.Size);
                chromosome.Fitness = _fitnessFunction.Calculate(image);
                // _logger.Information("Fitness {fitness}", chromosome.Fitness);
            }
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
                if (Randoms.NextDouble() > _parameters.MutationRate)
                    continue;

                var idx = Randoms.Next(_mutations.Count);
                _mutations[idx].Apply(chromosome);
            }
        }

        private Bitmap GenerateImage(Chromosome chromosome, Size size)
        {
            var bitmap = new Bitmap(size.Width, size.Height);
            var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.Black);

            foreach (var polygon in chromosome.Polygons)
            {
                var brush = new SolidBrush(Color.FromArgb(
                    polygon.Rgb[0],
                    polygon.Rgb[1],
                    polygon.Rgb[2])
                );
                graphics.FillPolygon(brush, polygon.Points.ToArray());
            }

            return bitmap;
        }

        private void SaveSolution(Chromosome bestChromosome)
        {
            _logger.Information("Generating final image with fitness {Fitness} and polygons {Polygons}",
                bestChromosome.Fitness, bestChromosome.Polygons.Count);
            var generatedImage = GenerateImage(bestChromosome, _targetImage.Size);

            var destinationPath = $"Output/{DateTimeOffset.Now.Ticks}.jpg";
            _logger.Information("Saving image to {Path}", destinationPath);
            generatedImage.Save(destinationPath);
        }
    }
}
