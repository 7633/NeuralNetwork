using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralFabric neuralNetwork = new NeuralFabric();
            neuralNetwork.TeachMe();
            neuralNetwork.Print();
        }
    }
}