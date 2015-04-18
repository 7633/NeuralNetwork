using System;
using System.Collections.Generic;

namespace NeuralNetwork.XorNeuralNet
{
    public class XorNeuralNet
    {
        private EntryLevelNeural[] _entryNeurals = new EntryLevelNeural[2];

        private ExternalEnvironmentNeural[] _externalNeurals = new ExternalEnvironmentNeural[2];

        private AssasinLevelNeural[] _assasinNeurals = new AssasinLevelNeural[]
        {
            new AssasinLevelNeural(), new AssasinLevelNeural()
        };

        private AssasinLevelNeural _exitNeural = new AssasinLevelNeural();

        public EntryLevelNeural[] EntryNeurals
        {
            get { return _entryNeurals; }
            set { _entryNeurals = value; }
        }

        public ExternalEnvironmentNeural[] ExternalNeurals
        {
            get { return _externalNeurals; }
            set { _externalNeurals = value; }
        }

        public AssasinLevelNeural[] AssasinNeurals
        {
            get { return _assasinNeurals; }
            set { _assasinNeurals = value; }
        }

        public AssasinLevelNeural ExitNeural
        {
            get { return _exitNeural; }
            set { _exitNeural = value; }
        }

        private double _exitValue;

        public double ExitValue
        {
            get { return _exitValue; }
        }
        
        // Коллекция образцов: входной_1, входной_2, выходной
        public readonly List<Tuple<double, double, double>> XorPairs = new List<Tuple<double, double, double>>
            {   
                Tuple.Create(0.0, 0.0, 0.0),
                Tuple.Create(1.0, 1.0, 0.0),
                Tuple.Create(0.0, 1.0, 1.0),
                Tuple.Create(1.0, 0.0, 1.0)
            };

        public XorNeuralNet()
        {
            _externalNeurals[0] = new ExternalEnvironmentNeural(1);
            _externalNeurals[1] = new ExternalEnvironmentNeural(1);

            _entryNeurals[0] = new EntryLevelNeural(XorPairs[0].Item1);
            _entryNeurals[1] = new EntryLevelNeural(XorPairs[0].Item2);

            var startWeights = _generateWeights();

            SetWeights(startWeights);
            _exitValue = _exitNeural.Exit;
        }

        public void SetWeights(double[] weigths)
        {
            _assasinNeurals[0].Weights.Clear();
            _assasinNeurals[0].Weights.Add(_entryNeurals[0], weigths[0]);
            _assasinNeurals[0].Weights.Add(_entryNeurals[1], weigths[1]);
            _assasinNeurals[0].Weights.Add(_externalNeurals[0], weigths[2]);

            _assasinNeurals[1].Weights.Clear();
            _assasinNeurals[1].Weights.Add(_entryNeurals[0], weigths[3]);
            _assasinNeurals[1].Weights.Add(_entryNeurals[1], weigths[4]);
            _assasinNeurals[1].Weights.Add(_externalNeurals[0], weigths[5]);

            _exitNeural.Weights.Clear();
            _exitNeural.Weights.Add(_assasinNeurals[0], weigths[6]);
            _exitNeural.Weights.Add(_assasinNeurals[1], weigths[7]);
            _exitNeural.Weights.Add(_externalNeurals[1], weigths[8]);
        }

        public double LaunchNet(double enter1, double enter2)
        {
            _entryNeurals[0].Initialize(enter1);
            _entryNeurals[1].Initialize(enter2);

            _assasinNeurals[0].CreateNeural();
            _assasinNeurals[1].CreateNeural();
            _exitNeural.CreateNeural();

            _exitValue = _exitNeural.Exit;
            return _exitValue;
        }
        
        private double[] _generateWeights()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            var randomWeigths = new double[9];

            for (int i = 0; i < 9; i++)
            {
                randomWeigths[i] = rand.NextDouble()*0.6 - 0.3;
            }

            return randomWeigths;
        }
        
        public void Print()
        {
            //TODO
        }
    }
}
