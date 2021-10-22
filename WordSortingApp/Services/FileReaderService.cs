using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordSortingApp.Interfaces;

namespace WordSortingApp.Services
{
    public class FileReaderService : ITextCollectorStrategy
    {
        public void CollectText(string[] filePaths)
        {
            ReadAndProcessFiles(filePaths);
        }

        private void ReadAndProcessFiles(string[] filePaths)
        {
            // Our thread-safe collection used for the handover.
            var words = new BlockingCollection<string>();

            // Build the pipeline.
            var readWordsFromFile = Task.Run(() =>
            {
                try
                {
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                    foreach (var filePath in filePaths)
                    {
                        using (var reader = new StreamReader(filePath))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] wordsInLine = line.Split(' ');
                                foreach (var singleWord in wordsInLine)
                                {
                                    //remove non alphanumeric chars
                                    string cleanWord = rgx.Replace(singleWord, "");
                                    // Hand over to addWordsToWordCounter and continue reading.
                                    words.Add(cleanWord.ToLower());
                                }
                                
                            }
                        }
                    }
                }
                finally
                {
                    words.CompleteAdding();
                }
            });

            var addWordsToWordCounter = Task.Run(() =>
            {
                // Process lines on a ThreadPool thread
                // as soon as they become available.
                foreach (var word in words.GetConsumingEnumerable())
                {
                    // Send to word counter service
                    WordCounterService.AddWordToCollection(word);
                }
            });

            // Block until both tasks have completed.
            // This makes this method prone to deadlocking.
            // Consider using 'await Task.WhenAll' instead.
            Task.WaitAll(readWordsFromFile, addWordsToWordCounter);
        }
    }
}
