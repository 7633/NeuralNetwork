namespace NeuralNetwork.HelperTypes
{
    public class Coord
    {
        public int Claster = -1;
        public double Latitude = 0.0;
        public double Longitude = 0.0;

        public Coord(double lat, double @long, int claster)
        {
            Latitude = lat;
            Longitude = @long;
            Claster = claster;
        }
    }
}