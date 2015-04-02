using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class AssasinLevelNeural : INeural
    {
        private Dictionary<INeural, double> _weights = new Dictionary<INeural,double>();
        private double _enter;
        private double _exit;
        private double _error; 

        public override double Enter
        {
            get { return _enter; }
            set { _enter = value; }
        }

        public override double Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public double Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public Dictionary<INeural, double> Weights
        {
            get { return _weights; }
            set { _weights = value; }
        }

        public double CreateNeural()
        {
            _enter = _weights.Sum(w1 => w1.Key.Exit * w1.Value);
            _exit = _exitFunction(_enter);
            return _exit;
        }

        private double _exitFunction(double enter)
        {
            return 1/(1 + Math.Exp(-enter));
        }
    }
}
