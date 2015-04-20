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
        private const double Epsilon = 0.0001;
        private const int ClasterCount = 10;

        private readonly List<Coord> _coordClast = new List<Coord>();
        private readonly double _etta = 0.9;
        private readonly int _iterations = 0;

        public SelfOrganizingFeatureMaps(SofmNet net, List<Coord> coordinates)
        {
            double delta = _etta/coordinates.Count;

            var rand = new Random((int) DateTime.Now.Ticks);

            net.SetWeights(
                _generateWeights(rand, coordinates.Select(w => w.Loc[0]).Min(), coordinates.Select(w => w.Loc[0]).Max()),
                _generateWeights(rand, coordinates.Select(w => w.Loc[1]).Min(), coordinates.Select(w => w.Loc[1]).Max()));

            while (_etta > Epsilon)
            {
                int coordIdx = rand.Next(0, coordinates.Count);
                Coord latLon = coordinates[coordIdx];

                net.LaunchNet(latLon.Loc[0], latLon.Loc[1]);

                _updateWeight(net, net.BmuIdx);

                _etta -= delta;
                latLon.Claster = net.BmuIdx;
                _coordClast.Add(latLon);
                coordinates.Remove(latLon);
            }
        }

        public List<Coord> CoordClast
        {
            get { return _coordClast; }
        }

        private void _updateWeight(SofmNet net, int bestIdx)
        {
            ClasterLevelNeural claster = net.ExitNeurals[bestIdx];

            claster.Weights[net.EntryNeurals[0]] += _etta*net.EntryNeurals[0].Exit;
            claster.Weights[net.EntryNeurals[1]] += _etta*net.EntryNeurals[1].Exit;
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