using System;
using NeuralNetwork.Algorithms;
using NeuralNetwork.Algorithms.GeneticAlgorithm;

namespace NeuralNetwork
{
    internal class Program
    {
        private const int ChromosomeLength = 9;
        private const int GenLength = 19;

        private static void Main(string[] args)
        {
            var neuralNetwork = new XorNeuralNet.XorNeuralNet();

            new GeneticAlgorithm(neuralNetwork); 
            Console.WriteLine();
            new BackPropagation(neuralNetwork);
        }
    }
}