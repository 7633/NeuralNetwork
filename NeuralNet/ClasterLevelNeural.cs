using System;
using System.Collections.Generic;

namespace NeuralNetwork.NeuralNet
{
    public class ClasterLevelNeural : INeural
    {
        private double _exit;
        private Dictionary<INeural, double> _weights = new Dictionary<INeural, double>();

        public override double Enter { get; set; }

        public override double Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public double Error { get; set; }

        public Dictionary<INeural, double> Weights
        {
            get { return _weights; }
            set { _weights = value; }
        }

        public double CreateNeural()
        {
            var phi = new INeural[2];
            _weights.Keys.CopyTo(phi, 0);

            var lyambda = new double[2];
            _weights.Values.CopyTo(lyambda, 0);

            _exit = _exitFunction(phi[0].Exit, phi[1].Exit, Math.Abs(lyambda[0] - lyambda[1]));
            return _exit;
        }

        private double _exitFunction(double phi1, double phi2, double deltaLyambda)
        {
            return 2*Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((phi2 - phi1)/2), 2)
                                         + Math.Cos(phi1)*Math.Cos(phi2)*Math.Pow(Math.Sin((deltaLyambda)/2), 2)));
        }
    }
}