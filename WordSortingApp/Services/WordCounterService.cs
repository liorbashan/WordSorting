using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSortingApp.Services
{
    public static class WordCounterService
    {
        public static Dictionary<string, int> wordsTable = new Dictionary<string, int>();

        public static void AddWordToCollection(string word)
        {
            if(wordsTable.ContainsKey(word))
            {
                wordsTable[word] = wordsTable[word] + 1;
            }
            else
            {
                wordsTable.Add(word, 1);
            }
        }

        public static int GetNumberOfIsntances(string word)
        {
            return wordsTable.ContainsKey(word) ? wordsTable[word] : 0;
        }
    }
}
