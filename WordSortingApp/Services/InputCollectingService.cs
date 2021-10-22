using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSortingApp.Interfaces;

namespace WordSortingApp.Services
{
    public class InputCollectingService
    {
        private ITextCollector _fileTextCollectorAdapter { get; set; }
        private ITextCollector _urlTextCollectorAdapter { get; set; }
        public InputCollectingService(FileReaderService fileReader, UrlReaderService urlReader)
        {
            _fileTextCollectorAdapter = fileReader;
            _urlTextCollectorAdapter = urlReader;
        }
        public void CreateCollectionFromInputs(InputType inputType, string[] sources)
        {
            //switch (inputType)
            //{
            //    case InputType.File:
            //        _fileTextCollectorAdapter = new FileReaderService();
            //        break;
            //    case InputType.URL:
            //        break;
            //    default:
            //        break;
            //}
            //this._fileTextCollectorAdapter.ReadAndAddToCollection(sources);
            _urlTextCollectorAdapter.ReadAndAddToCollection(sources);

            bool contineu = true;
            while (contineu)
            {
                contineu = ResetUserInterface();
            }
        }

        public bool ResetUserInterface()
        {
            Console.WriteLine("Type a word and get the amount of instances it apears in text");
            string word = Console.ReadLine();
            int count = WordCounterService.GetNumberOfIsntances(word);
            Console.WriteLine($"The word {word} apears in text {count} times");

            Console.WriteLine("Continue? Y/N");
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "y")
                return true;
            else
                return false;
        }
    }
}
