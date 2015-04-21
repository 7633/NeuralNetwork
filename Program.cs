using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using NeuralNetwork.Algorithms;
using NeuralNetwork.HelperTypes;
using NeuralNetwork.Networks;
using Newtonsoft.Json;

namespace NeuralNetwork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var neuralNetwork = new XorNeuralNet();

            //new GeneticAlgorithm(neuralNetwork);
            //Console.WriteLine();
            //new BackPropagation(neuralNetwork);

            var fromFile = new List<Coord>();

            const string filePath =
                @"D:\Study\Семестр 8\Системы искусственного интеллекта\NeuralNetwork\NeuralNetwork\lat_lng.csv";
            const string outFilePath = "out.json";

            using (var file = new StreamReader(filePath, Encoding.Default))
            {
                while (!file.EndOfStream)
                {
                    string[] line = file.ReadLine().Split(',');
                    var coord = new Coord(Double.Parse(line[0], CultureInfo.InvariantCulture),
                        Double.Parse(line[1], CultureInfo.InvariantCulture), -1);
                    fromFile.Add(coord);
                }
            }

            var sofmNet = new SofmNet();
            var sofm = new SelfOrganizingFeatureMaps(sofmNet, fromFile);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Claster\t{0} have\t{1}\tcoordinates", i + 1,
                    sofm.CoordClast.Distinct().ToList().Select(c => c).Where(c => c.Claster == i).ToList().Count);
            }

            List<SerializebleCoord> serializebleData = sofm.CoordClast.Distinct().ToList().ConvertAll(input => new SerializebleCoord(input));

            string jsonString = JsonConvert.SerializeObject(serializebleData);

            File.WriteAllText(outFilePath, jsonString);
        }
    }
}