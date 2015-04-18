using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class EntryLevelNeural : INeural
    {
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
        public EntryLevelNeural(float enter)
        {
            _enter = enter;
            _exit = enter;
        }
    }
}
