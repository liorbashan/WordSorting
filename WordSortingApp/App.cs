using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSortingApp.Services;

namespace WordSortingApp
{
    public class App
    {
        public InputCollectingService inputCollector { get; set; }
        public App(InputCollectingService textCollector)
        {
            this.inputCollector = textCollector;
        }
        public void Run()
        {
            Console.WriteLine("Select input type: type 1 to read from files, type 2 to read from URL");
            string inputType = Console.ReadLine();
            string[] pathToFile = new string[] { Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets", "TextFile1.txt"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets", "TextFile2.txt"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets", "TextFile3.txt") };
            string[] urls = new string[] { "https://en.wikipedia.org/wiki/Structural_pattern", "https://en.wikipedia.org/wiki/Design_Patterns" };
            inputCollector.CreateCollectionFromInputs(InputType.File, urls);
        }
    }
}
