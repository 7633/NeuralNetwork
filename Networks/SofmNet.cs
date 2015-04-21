using System;
using System.Linq;
using NeuralNetwork.NeuralNet;

namespace NeuralNetwork.Networks
{
    public class SofmNet
    {
        private const int ClasterCount = 10;
        private int _bmuIdx;

        private EntryLevelNeural _latitudaNeural = new EntryLevelNeural(0.0);
        private EntryLevelNeural _longitudeNeural = new EntryLevelNeural(0.0);

        private ClasterLevelNeural[] _exitNeurals =
        {
            new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(),
            new ClasterLevelNeural(),
            new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(),
            new ClasterLevelNeural()
        };

        public int BmuIdx
        {
            get { return _bmuIdx; }
        }

        public EntryLevelNeural LatitudaNeural
        {
            get { return _latitudaNeural; }
            set { _latitudaNeural = value; }
        }

        public EntryLevelNeural LongitudeNeural
        {
            get { return _longitudeNeural; }
            set { _longitudeNeural = value; }
        }

        public ClasterLevelNeural[] ExitNeurals
        {
            get { return _exitNeurals; }
            set { _exitNeurals = value; }
        }

        public void SetWeights(double[] latWeights, double[] longWeights)
        {
            //TODO что это за бубуйня? как устанавливаются веса для сети?
            //хочешь сказать, что для всех кластеров обновляются его веса
            foreach (var neural in _exitNeurals)
            {
                neural.Weights.Clear();
            }

            //затем для каждого кластера добавляется вес для широ-ты и долго-ты
            int iw = 0;
            foreach (ClasterLevelNeural neural in _exitNeurals)
            {
                neural.Weights.Add(_latitudaNeural, latWeights[iw]);
                neural.Weights.Add(_longitudeNeural, longWeights[iw++]);
            }
        }

        public void LaunchNet(double lat, double @long)
        {
            _latitudaNeural.Initialize(lat);
            _longitudeNeural.Initialize(@long);

            foreach (ClasterLevelNeural clasterLevelNeural in _exitNeurals)
            {
                clasterLevelNeural.CreateNeural();
            }

            _bmuIdx = _getBestMatchingUnit();
        }

        private int _getBestMatchingUnit()
        {
            double min = Double.MaxValue;
            int idx = 0;
            for (int i = 0; i < ClasterCount; i++)
            {
                if (!(_exitNeurals[i].Exit <= min)) continue;
                min = _exitNeurals[i].Exit;
                idx = i;
            }
            
            return idx;
        }
    }
}