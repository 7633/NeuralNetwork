using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Algorithms
{
    class Algorithm
    {

        // TODO допилить вызовы алгоритмов, время работы
        private DateTime time;
        private XorNeuralNet.XorNeuralNet _net;

        public GeneticAlgorithm.GeneticAlgorithm GeneticAlgorithm;
        public BackPropagation BackPropagation;

        public Algorithm(XorNeuralNet.XorNeuralNet net)
        {
            _net = net;
        }
    }
}
