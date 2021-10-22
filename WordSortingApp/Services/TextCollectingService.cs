using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSortingApp.Interfaces;

namespace WordSortingApp.Services
{
    public class TextCollectingService
    {
        private ITextCollectorStrategy _textCollectingStrategy { get; set; }

        public void CollectWords(InputType inputType, string[] sources)
        {
            switch (inputType)
            {
                case InputType.File:
                    _textCollectingStrategy = new FileReaderService();
                    break;
                case InputType.URL:
                    break;
                default:
                    break;
            }
            this._textCollectingStrategy.CollectText(sources);

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
