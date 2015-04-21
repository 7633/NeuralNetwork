using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.HelperTypes;
using NeuralNetwork.Networks;
using NeuralNetwork.NeuralNet;

namespace NeuralNetwork.Algorithms
{
    public class SelfOrganizingFeatureMaps
    {
        private const double Epsilon = 0.001;
        private const int ClasterCount = 10;

        private readonly List<Coord> _coordClast = new List<Coord>();
        private readonly double _etta = 0.9;
        private readonly int _iterations = 0;

        public SelfOrganizingFeatureMaps(SofmNet net, List<Coord> coordinates)
        {
            const double delta = 0.00001;

            _coordClast = new List<Coord>();
            _coordClast.AddRange(coordinates);
            var rand = new Random((int) DateTime.Now.Ticks);

            net.SetWeights(
                _generateWeights(rand, coordinates.Select(w => w.Latitude).Min(), coordinates.Select(w => w.Latitude).Max()),
                _generateWeights(rand, coordinates.Select(w => w.Longitude).Min(), coordinates.Select(w => w.Longitude).Max()));

            for (int i = 0; i < 20000000; i++)
            {
                int coordIdx = rand.Next(0, _coordClast.Count);

                net.LaunchNet(_coordClast[coordIdx].Latitude, _coordClast[coordIdx].Longitude);

                _updateWeight(net, net.BmuIdx);

                if (_etta > Epsilon)
                {
                    _etta -= delta;
                }
                _coordClast[coordIdx].Claster = net.BmuIdx;
            }
        }

        public List<Coord> CoordClast
        {
            get { return _coordClast; }
        }

        private void _updateWeight(SofmNet net, int bestIdx)
        {
            net.ExitNeurals[bestIdx].Weights[net.LatitudaNeural] += _etta * (net.LatitudaNeural.Exit - net.ExitNeurals[bestIdx].Weights[net.LatitudaNeural]);
            net.ExitNeurals[bestIdx].Weights[net.LongitudeNeural] += _etta * (net.LongitudeNeural.Exit - net.ExitNeurals[bestIdx].Weights[net.LongitudeNeural]);
        }

        private double[] _generateWeights(Random rand, double min, double max)
        {
            var randomWeigths = new double[ClasterCount];

            for (int i = 0; i < ClasterCount; i++)
            {
                randomWeigths[i] = rand.NextDouble()*(max - min) + min;
            }

            return randomWeigths;
        }
    }
}