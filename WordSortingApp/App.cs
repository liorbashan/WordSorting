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
        public List<string> pathToFile = new List<string>();
        public List<string> fileBank = new List<string>();
        public List<string> urlList = new List<string>();
        public App(InputCollectingService textCollector)
        {
            this.inputCollector = textCollector;
        }
        public void Run()
        {
            PopulateFileBank();
            ShowGreeting();
            Console.WriteLine("Creating Report...");
            inputCollector.CreateCollectionFromInputs(pathToFile.ToArray(),urlList.ToArray());
            Console.WriteLine("Done!");
            ShowResults();
        }

        public void ShowGreeting()
        {
            Console.WriteLine("Greetings stranger and welcome to the Word Counter App\n\n");
            string inputType = ShowChooseInputType();
            while (!IsInputTypeSelectionValid(inputType))
            {
                ShowBadInputMessage();
                ShowChooseInputType();
                inputType = Console.ReadLine();
            }
            bool shouldContinue = true;
            while (shouldContinue)
            {
                ChooseSource(inputType);
                Console.WriteLine("Do you wish to add another source? Y/N");
                string selection = Console.ReadLine();
                if (selection.Trim().ToLower() != "y")
                    shouldContinue = false;
                else
                    inputType = ShowChooseInputType();
            }
        }

        public string ShowChooseInputType()
        {
            Console.WriteLine("Please add the input source you wish to count words for.");
            Console.WriteLine("Press 1 if the input is a file, Press 2 if the input is a URL address");
            return Console.ReadLine();
        }

        public bool IsInputTypeSelectionValid(string inputType)
        {
            return inputType == "1" || inputType == "2";
        }

        public void ShowBadInputMessage()
        {
            Console.WriteLine("Your seceltion is invalid, please try again");
        }
        public void ChooseSource(string inputType)
        {
            switch (inputType)
            {
                case "1":
                    ShowFilesList();
                    break;
                case "2":
                    SelectUrlAddress();
                    break;
                default:
                    break;
            }
        }

        public void ShowFilesList()
        {
            foreach (var file in fileBank)
            {
                int index = file.IndexOf("TextFile");
                string fileName = file.Substring(index);
                Console.WriteLine(fileName);
            }
            Console.WriteLine("Type selected file name:");
            string selectedFileName = Console.ReadLine();
            foreach (var file in fileBank.ToList())
            {
                if(file.ToLower().IndexOf(selectedFileName.ToLower()) > -1)
                {
                    pathToFile.Add(file);
                    fileBank.Remove(file);
                }
            }
        }

        public void SelectUrlAddress()
        {
            Console.WriteLine("Please type a URL address (start with https://)");
            string url = Console.ReadLine();
            if (url.StartsWith("http"))
                urlList.Add(url);
        }

        public void ShowResults()
        {
            bool checkResult = true;
            while(checkResult)
            {
                Console.WriteLine("Type a word and see how many itme it apears in input. (Press ESC to exit)");
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.Escape)
                {
                    checkResult = false;
                    break;
                }
                else
                {
                    string word = Console.ReadLine();
                    int count = WordCounterService.GetNumberOfIsntances(word);
                    Console.WriteLine($"The word {word} apears in text {count} times\n\n");
                }
            }
            
        }

        private void PopulateFileBank()
        {
            string pathToDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets");
            string[] filePaths = Directory.GetFiles(pathToDirectory, "*.txt", SearchOption.TopDirectoryOnly);
            foreach (var file in filePaths)
            {
                fileBank.Add(file);
            }
        }
    }
}
