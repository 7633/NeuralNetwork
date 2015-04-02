using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    abstract class INeural
    {
        public abstract double Enter { get; set; }
        public abstract double Exit { get; set; }
    }
}
