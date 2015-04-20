using System;
using NeuralNetwork.NeuralNet;

namespace NeuralNetwork.Networks
{
    public class SofmNet
    {
        private const int ClasterCount = 10;
        private int _bmuIdx;

        private EntryLevelNeural[] _entryNeurals = new EntryLevelNeural[2];

        private ClasterLevelNeural[] _exitNeurals = new ClasterLevelNeural[ClasterCount]
        {
            new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(),
            new ClasterLevelNeural(),
            new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(), new ClasterLevelNeural(),
            new ClasterLevelNeural()
        };

        public SofmNet()
        {
            _entryNeurals[0] = new EntryLevelNeural(0.0);
            _entryNeurals[1] = new EntryLevelNeural(0.0);
        }

        public int BmuIdx
        {
            get { return _bmuIdx; }
        }

        public EntryLevelNeural[] EntryNeurals
        {
            get { return _entryNeurals; }
            set { _entryNeurals = value; }
        }

        public ClasterLevelNeural[] ExitNeurals
        {
            get { return _exitNeurals; }
            set { _exitNeurals = value; }
        }

        public void SetWeights(double[] weights1, double[] weights2)
        {
            _exitNeurals[0].Weights.Clear();
            int iw = 0;
            foreach (ClasterLevelNeural neural in _exitNeurals)
            {
                neural.Weights.Add(_entryNeurals[0], weights1[iw]);
                neural.Weights.Add(_entryNeurals[1], weights2[iw++]);
            }
        }

        public void LaunchNet(double enter1, double enter2)
        {
            _entryNeurals[0].Initialize(enter1);
            _entryNeurals[1].Initialize(enter2);

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