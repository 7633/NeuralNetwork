using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class EntryLevelNeural : INeural
    {
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

        public void Initialize(double enter)
        {
            _enter = enter;
            _exit = enter;
        }

        public EntryLevelNeural(double enter)
        {
            Initialize(enter);
        }
    }
}
