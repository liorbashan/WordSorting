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
        public TextCollectingService textCollectorService { get; set; }
        public App(TextCollectingService textCollector)
        {
            this.textCollectorService = textCollector;
        }
        public void Run()
        {
            Console.WriteLine("Select input type: type 1 to read from files, type 2 to read from URL");
            string inputType = Console.ReadLine();
            string[] pathToFile = new string[] { Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets", "TextFile1.txt"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets", "TextFile2.txt"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets", "TextFile3.txt") };
            textCollectorService.CollectWords(InputType.File, pathToFile);
        }
    }
}
