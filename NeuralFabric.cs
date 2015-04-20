using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class NeuralFabric
    {
        private EntryLevelNeural _firstEntry;
        private EntryLevelNeural _secondEntry;

        private ExternalEnvironmentNeural _firstExternal = new ExternalEnvironmentNeural(1);
        private ExternalEnvironmentNeural _secondExternal = new ExternalEnvironmentNeural(1);

        private AssasinLevelNeural _firstAssasin = new AssasinLevelNeural(); // лучше не сокращать
        private AssasinLevelNeural _secondAssasin = new AssasinLevelNeural();
        private AssasinLevelNeural _exitNeural = new AssasinLevelNeural();

        private double _exitValue;

        // Коллекция образцов: входной, входной, выходной
        private List<Tuple<double, double, double>> _pairTuples = new List<Tuple<double, double, double>>
            {   
                Tuple.Create(0.0, 0.0, 0.0),
                Tuple.Create(1.0, 1.0, 0.0),
                Tuple.Create(0.0, 1.0, 1.0),
                Tuple.Create(1.0, 0.0, 1.0)
            };

        public double ExitValue
        {
            get { return _exitValue; }
        }

        // создаём класс и генерим его веса рандомными значениями (-0.3; 0.3)
        public NeuralFabric()
        {
            _firstEntry = new EntryLevelNeural(0.0);
            _secondEntry = new EntryLevelNeural(1.0);
            
            // следует сделать просто сетРандомВес. Видимо, сетВес не понадобится
            Random rand = new Random((int)DateTime.Now.Ticks);
            _setWeights(rand);
            _exitValue = _exitNeural.Exit;
        }

        private void _initialize(double enter1, double enter2)
        {
            _firstEntry.Initialize(enter1);
            _firstEntry.Initialize(enter2);

            _directPassage();
            //Console.WriteLine(enter1 + " " + enter2 + " = " +  _exitValue);
        }

        public void TeachMe()
        {
            foreach (var pairTuple in _pairTuples)
            {
                _initialize(pairTuple.Item1, pairTuple.Item2);
                while (Math.Abs(_exitValue - pairTuple.Item3) > 0.1)
                {
                    _reversePassage(pairTuple.Item3);
                    _directPassage();
                }
                Console.WriteLine(pairTuple.Item1 + " " + pairTuple.Item2 + " " + _exitValue);
            }
            
        }

        // прямой ход. по сути это обновление значений весов элементов и их подсчёт.
        private void _directPassage()
        {
            _firstAssasin.CreateNeural();
            _secondAssasin.CreateNeural();
            _exitNeural.CreateNeural();
            _exitValue = _exitNeural.Exit;
        }

        // первый, рандомный.
        private void _setWeights(Random rand)
        {           
            _firstAssasin.Weights.Add(_firstEntry, _nextRandom(rand));
            _firstAssasin.Weights.Add(_secondEntry, _nextRandom(rand));
            _firstAssasin.Weights.Add(_firstExternal, _nextRandom(rand));
            _firstAssasin.CreateNeural();

            _secondAssasin.Weights.Add(_firstEntry, _nextRandom(rand));
            _secondAssasin.Weights.Add(_secondEntry, _nextRandom(rand));
            _secondAssasin.Weights.Add(_firstExternal, _nextRandom(rand));
            _secondAssasin.CreateNeural();

            _exitNeural.Weights.Add(_firstAssasin, _nextRandom(rand));
            _exitNeural.Weights.Add(_secondAssasin, _nextRandom(rand));
            _exitNeural.Weights.Add(_secondExternal, _nextRandom(rand));
            _exitNeural.CreateNeural();
        }

        private double _nextRandom(Random rand)
        {
            return 0.3 * (rand.NextDouble() * 2 - 1);
        }

        private void _reversePassage(double real)
        {
            // выходной слой
            _exitNeural.Error = (1 - _exitNeural.Exit) * (real - _exitNeural.Exit) * _exitNeural.Exit;

            _exitNeural.Weights[_firstAssasin] += _exitNeural.Error*_firstAssasin.Exit;
            _exitNeural.Weights[_secondAssasin] += _exitNeural.Error*_secondAssasin.Exit;

            _exitNeural.Weights[_secondExternal] += _exitNeural.Error*_secondExternal.Exit;

            // скрытый слой
            _firstAssasin.Error = _firstAssasin.Exit * (1 - _firstAssasin.Exit) * 
                (_exitNeural.Error * _exitNeural.Weights[_firstAssasin] + _exitNeural.Error * _exitNeural.Weights[_secondAssasin]);
            _secondAssasin.Error = _secondAssasin.Exit * (1 - _secondAssasin.Exit) * 
                (_exitNeural.Error * _exitNeural.Weights[_firstAssasin] + _exitNeural.Error * _exitNeural.Weights[_secondAssasin]);

            _firstAssasin.Weights[_firstEntry] += _firstAssasin.Error*_firstEntry.Exit;
            _firstAssasin.Weights[_secondEntry] += _firstAssasin.Error*_secondEntry.Exit;
            _firstAssasin.Weights[_firstExternal] += _firstAssasin.Error*_firstExternal.Exit;

            _secondAssasin.Weights[_firstEntry] += _secondAssasin.Error*_firstEntry.Exit;
            _secondAssasin.Weights[_secondEntry] += _secondAssasin.Error*_secondEntry.Exit;
            _secondAssasin.Weights[_firstExternal] += _secondAssasin.Error*_firstExternal.Exit;
        }

        public void Print()
        {
            //TODO
        }
    }
}
