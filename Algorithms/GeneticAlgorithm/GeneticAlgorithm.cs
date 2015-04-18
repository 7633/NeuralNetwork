using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Algorithms.GeneticAlgorithm
{
    class GeneticAlgorithm
    {
        public const int PopulationLength = 40;
        private int _iterations = 0;
        private readonly Random _rand = new Random((int)DateTime.Now.Ticks);

        private List<Speciman> _population = new List<Speciman>(PopulationLength);
        
        private void _initializePopulation()
        {
            
            for (int i = 0; i < PopulationLength; i++)
            {
                _population.Add(new Speciman(_rand));
            }
        }

        public GeneticAlgorithm(XorNeuralNet.XorNeuralNet net)
        {
            _initializePopulation();

            while (true)
            {
                _iterations++;
                foreach (var speciman in _population)
                {
                    if (speciman.CheckFitness(net))
                    {
                        Console.WriteLine("We find it! " + speciman.Fitness);
                        Console.WriteLine("Iterations of Genetic algortihm: " + _iterations);
                        return;
                    }
                }

                // селекция хромосом
                _selection();
                var matingPool = new List<Speciman>(_population);

                // Генетические операторы
                int n = matingPool.Count;
                while (n > 0)
                {
                    int idx1 = _rand.Next(0, n-- / 2);
                    var temp1 = matingPool[idx1];
                    matingPool.Remove(temp1);
                    int idx2 = _rand.Next(n / 2, n--);
                    var temp2 = matingPool[idx2];
                    matingPool.Remove(temp2);

                    int lokus = _rand.Next(0, Speciman.Length);
                    
                    _population.Add(Speciman.Crossover(temp1, temp2, lokus));
                    _population.Add(Speciman.Crossover(temp2, temp1, lokus));
                }

                for (int i = PopulationLength/2; i < PopulationLength; i++)
                {
                    int lokus = _rand.Next(0, Speciman.Length);
                    Speciman.Mutation(_population[i], lokus);
                }
            }
        
        }


        private void _selection()
        {
            int n = PopulationLength;
            var tempChList = new List<Speciman>(_population);
            while (n > 0)
            {
                int idx1 = _rand.Next(0, n-- / 2);
                var ch1 = tempChList[idx1];
                tempChList.RemoveAt(idx1);
                int idx2 = _rand.Next(n / 2, n--);
                var ch2 = tempChList[idx2];
                tempChList.RemoveAt(idx2);

                _population.RemoveAt(ch1.Fitness < ch2.Fitness ? idx2 : idx1);
            }
        }
        
    }
}
