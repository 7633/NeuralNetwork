using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            float x1 = 0;
            float x2 = 0;
            EntryLevelNeural el1 = new EntryLevelNeural(x1);
            EntryLevelNeural el2 = new EntryLevelNeural(x2);

            ExternalEnvironmentNeural ee1 = new ExternalEnvironmentNeural(1);

            AssasinLevelNeural a1 = new AssasinLevelNeural();
            a1.Weights.Add(el1, -1);
            a1.Weights.Add(el2, -1);
            a1.Weights.Add(ee1, 0.5f);
            a1.CreateNeural();

            AssasinLevelNeural a2 = new AssasinLevelNeural();
            a2.Weights.Add(el1, -1);
            a2.Weights.Add(el2, -1);
            a2.Weights.Add(ee1, 1.5f);
            a2.CreateNeural();

            ExternalEnvironmentNeural ee2 = new ExternalEnvironmentNeural(1);

            AssasinLevelNeural exit = new AssasinLevelNeural();
            exit.Weights.Add(a1, -1);
            exit.Weights.Add(a2, 1);
            exit.Weights.Add(ee2, -0.5f);
            exit.CreateNeural();

            Console.WriteLine(exit.Exit);
        }
    }
}
