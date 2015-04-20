namespace NeuralNetwork.HelperTypes
{
    public class Coord
    {
        public int Claster = 0;
        public double[] Loc = {0.0, 0.0};

        public Coord(double loc1, double loc2, int claster)
        {
            Loc[0] = loc1;
            Loc[1] = loc2;
            Claster = claster;
        }
    }
}