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
            var tempK = new INeural[2];
            _weights.Keys.CopyTo(tempK, 0);

            var phi1 = tempK[0].Exit; // широта входная
            var lyambda1 = tempK[1].Exit; // долгота входная

            var tempV= new double[2];
            _weights.Values.CopyTo(tempV, 0);

            var phi2 = tempV[0]; // широта посчитанная
            var lyambda2 = tempV[1]; // долгота посчитанная

            _exit = _exitFunction(phi1 * Math.PI / 180, phi2 * Math.PI / 180, Math.Abs(lyambda1 * Math.PI / 180 - lyambda2 * Math.PI / 180));
            return _exit;
        }

        private double _exitFunction(double phi1, double phi2, double deltaLyambda)
        {
            return 2*Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((phi2 - phi1)/2), 2)
                                         + Math.Cos(phi1)*Math.Cos(phi2)*Math.Pow(Math.Sin((deltaLyambda)/2), 2)));
        }
    }
}