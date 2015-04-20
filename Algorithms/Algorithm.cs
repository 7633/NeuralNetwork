using System;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Algorithms
{
    internal class Algorithm
    {
        // TODO допилить вызовы алгоритмов, время работы
        public BackPropagation BackPropagation;
        public GeneticAlgorithm.GeneticAlgorithm GeneticAlgorithm;
        private XorNeuralNet _net;
        private DateTime time;

        public Algorithm(XorNeuralNet net)
        {
            _net = net;
        }
    }
}