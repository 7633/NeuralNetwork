using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class ExternalEnvironmentNeural : INeural
    {
        private double _exit;

        public override double Enter { get; set; }

        public override double Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public ExternalEnvironmentNeural(double exit)
        {
            _exit = exit;
        }
    }
}
