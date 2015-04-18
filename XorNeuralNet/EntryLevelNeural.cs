namespace NeuralNetwork.XorNeuralNet
{
    public class EntryLevelNeural : INeural
    {
        private double _enter;
        private double _exit;

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

        public double Error { get; set; }

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
