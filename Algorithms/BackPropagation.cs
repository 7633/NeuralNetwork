using System;
using NeuralNetwork.Networks;

namespace NeuralNetwork.Algorithms
{
    internal class BackPropagation
    {
        private readonly int _iterations;

        public BackPropagation(XorNeuralNet net)
        {
            Console.WriteLine("Start back propagation");
            foreach (var pairTuple in net.XorPairs)
            {
                net.LaunchNet(pairTuple.Item1, pairTuple.Item2);
                while (Math.Abs(net.ExitValue - pairTuple.Item3) > 0.01)
                {
                    _updateWeights(net, pairTuple.Item3);
                    net.LaunchNet(pairTuple.Item1, pairTuple.Item2);
                    _iterations++;
                }
                Console.WriteLine(pairTuple.Item1 + " " + pairTuple.Item2 + " " + net.ExitValue);
            }

            Console.WriteLine("Iterations of Back propogation algortihm: " + _iterations);
        }

        private void _updateWeights(XorNeuralNet net, double real)
        {
            // выходной слой
            net.ExitNeural.Error = (1 - net.ExitNeural.Exit)*(real - net.ExitNeural.Exit)*net.ExitNeural.Exit;

            net.ExitNeural.Weights[net.AssasinNeurals[0]] += net.ExitNeural.Error*net.AssasinNeurals[0].Exit;
            net.ExitNeural.Weights[net.AssasinNeurals[1]] += net.ExitNeural.Error*net.AssasinNeurals[1].Exit;

            net.ExitNeural.Weights[net.ExternalNeurals[1]] += net.ExitNeural.Error*net.ExternalNeurals[1].Exit;

            // скрытый слой
            net.AssasinNeurals[0].Error = net.AssasinNeurals[0].Exit*(1 - net.AssasinNeurals[0].Exit)*
                                          (net.ExitNeural.Error*net.ExitNeural.Weights[net.AssasinNeurals[0]]
                                           + net.ExitNeural.Error*net.ExitNeural.Weights[net.AssasinNeurals[1]]);
            net.AssasinNeurals[1].Error = net.AssasinNeurals[1].Exit*(1 - net.AssasinNeurals[1].Exit)*
                                          (net.ExitNeural.Error*net.ExitNeural.Weights[net.AssasinNeurals[0]]
                                           + net.ExitNeural.Error*net.ExitNeural.Weights[net.AssasinNeurals[1]]);

            net.AssasinNeurals[0].Weights[net.EntryNeurals[0]] += net.AssasinNeurals[0].Error*net.EntryNeurals[0].Exit;
            net.AssasinNeurals[0].Weights[net.EntryNeurals[1]] += net.AssasinNeurals[0].Error*net.EntryNeurals[1].Exit;
            net.AssasinNeurals[0].Weights[net.ExternalNeurals[0]] += net.AssasinNeurals[0].Error*
                                                                     net.ExternalNeurals[0].Exit;

            net.AssasinNeurals[1].Weights[net.EntryNeurals[0]] += net.AssasinNeurals[1].Error*net.EntryNeurals[0].Exit;
            net.AssasinNeurals[1].Weights[net.EntryNeurals[1]] += net.AssasinNeurals[1].Error*net.EntryNeurals[1].Exit;
            net.AssasinNeurals[1].Weights[net.ExternalNeurals[0]] += net.AssasinNeurals[1].Error*
                                                                     net.ExternalNeurals[0].Exit;
        }
    }
}