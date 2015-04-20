namespace NeuralNetwork.NeuralNet
{
    public class ExternalEnvironmentNeural : INeural
    {
        private double _exit;

        public ExternalEnvironmentNeural(double exit)
        {
            _exit = exit;
        }

        public override double Enter { get; set; }

        public override double Exit
        {
            get { return _exit; }
            set { _exit = value; }
        }
    }
}