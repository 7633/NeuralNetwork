using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Algorithms.GeneticAlgorithm
{
    internal class Speciman
    {
        public const int Length = 9;
        public const int GenLength = 19;

        public double Fitness = 0.0;

        private List<bool[]> _chromosome = new List<bool[]>(Length);

        public Speciman()
        {
        }

        public Speciman(Random rand)
        {
            _makeChromosome(rand);
        }

        public List<bool[]> Chromosome
        {
            get { return _chromosome; }
            set { _chromosome = value; }
        }

        public bool CheckFitness(XorNeuralNet net)
        {
            net.SetWeights(GetChromosomeInDouble());
            foreach (var pairTuple in net.XorPairs)
            {
                net.LaunchNet(pairTuple.Item1, pairTuple.Item2);
                Fitness += Math.Pow(net.ExitValue - pairTuple.Item3, 2);
            }
            Fitness *= 0.25;

            return Fitness < 0.1;
        }

        public static Speciman Crossover(Speciman mother, Speciman father, int lokus)
        {
            return new Speciman
            {
                Chromosome = mother.Chromosome.Take(lokus).Concat(father.Chromosome.Skip(lokus)).ToList()
            };
        }

        public static void Mutation(Speciman stalker, int lokus)
        {
            for (int i = 0; i < GenLength; i++)
            {
                stalker.Chromosome[lokus][i] = !stalker.Chromosome[lokus][i];
            }
        }

        public double[] GetChromosomeInDouble()
        {
            var res = new List<double>(Length);

            foreach (var gen in Chromosome)
            {
                int[] input = gen.Select(c => c ? 1 : 0).ToArray();
                int sign = 1;
                // если число отрицательное, то приводим его к пригодгому типу
                if (input[0] != 0)
                {
                    int k = 7;
                    while (input[k] != 1)
                    {
                        k--;
                    }
                    input[k] = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        input[j] = input[j] == 0 ? 1 : 0;
                    }
                    sign = -1;
                }

                int left = 0;
                int num = 1;
                for (int j = 7; j >= 0; j--)
                {
                    left += input[j]*num;
                    num *= 2;
                }

                double den = 0.5;
                double right = 0;
                for (int j = 8; j < GenLength; j++)
                {
                    right += input[j]*den;
                    den /= 2;
                }
                res.Add(sign*Double.Parse((left + right).ToString()));
            }

            return res.ToArray();
        }

        private void _makeChromosome(Random rand)
        {
            for (int i = 0; i < Length; i++)
            {
                _chromosome.Add(_generateGen(rand));
            }
        }

        private bool[] _generateGen(Random rand)
        {
            var result = new bool[GenLength];

            for (int j = 4; j < GenLength; j++)
            {
                result[j] = rand.Next(0, 2) != 0;
            }
            if (rand.Next(0, 2) != 0)
            {
                for (int j = 0; j < 5; j++)
                {
                    result[j] = true;
                }
            }
            return result;
        }
    }
}