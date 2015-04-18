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
        private Dictionary<INeural, float> _weights = new Dictionary<INeural,float>();
        private float _enter;
        private float _exit;

        public override float Enter
        {
            get { return _enter; }
            set { _enter = value; }
        }

        public override float Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public Dictionary<INeural, float> Weights
        {
            get { return _weights; }
            set { _weights = value; }
        }

        public float CreateNeural()
        {
            _enter = _weights.Sum(w1 => w1.Key.Exit * w1.Value);
            //foreach(var w1 in _weights) {
            //    _enter += w1.Key.Exit * w1.Value;
            //}
            _exit = _exitFunction(_enter);
            return _exit;
        }

        private float _exitFunction(float enter)
        {
            //return (enter < 0) ? 0 : 1;
            return (float) (1/(1 + Math.Exp(-enter)));
        }
    }
}
