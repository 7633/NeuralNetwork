using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class ExternalEnvironmentNeural : INeural
    {
        private float _exit;

        public override float Enter { get; set; }

        public override float Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public ExternalEnvironmentNeural(float exit)
        {
            _exit = exit;
        }
    }
}
