namespace NeuralNetwork.HelperTypes
{
    internal class SerializebleCoord
    {
        public string image_clster;
        public double[] loc = {0.0, 0.0};

        public SerializebleCoord(Coord c)
        {
            loc[0] = c.Latitude;
            loc[1] = c.Longitude;

            image_clster = "/img/_" + c.Claster + ".jpg";
        }
    }
}