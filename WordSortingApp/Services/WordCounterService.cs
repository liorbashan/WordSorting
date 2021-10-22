using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordSortingApp.Services
{
    public static class WordCounterService
    {
        public static Dictionary<string, int> wordsTable = new Dictionary<string, int>();

        public static void AddWordToCollection(string word)
        {
            string newValue = NormalizeAndClean(word);
            if(!String.IsNullOrEmpty(newValue))
            {
                if (wordsTable.ContainsKey(newValue))
                {
                    wordsTable[newValue] = wordsTable[newValue] + 1;
                }
                else
                {
                    wordsTable.Add(newValue.ToLower(), 1);
                }
            }
        }

        public static int GetNumberOfIsntances(string word)
        {
            return wordsTable.ContainsKey(word) ? wordsTable[word] : 0;
        }

        private static string NormalizeAndClean(string rawWord)
        {
            if(!String.IsNullOrEmpty(rawWord))
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                //remove non alphanumeric chars
                string cleanWord = rgx.Replace(rawWord, "");
                return cleanWord.Trim().ToLower();
            }
            return null;
        }
    }
}
