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
    public class FileReaderService : ITextCollector
    {
        public void ReadAndAddToCollection(string[] filePaths)
        {
            ReadFilesFromPath(filePaths);
        }

        private void ReadFilesFromPath(string[] filePaths)
        {
            // Our thread-safe collection used for the handover.
            var words = new BlockingCollection<string>();

            // Build the pipeline.
            var readWordsFromFile = Task.Run(() =>
            {
                try
                {
                    foreach (var filePath in filePaths)
                    {
                        try
                        {
                            using (var reader = new StreamReader(filePath))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    string[] wordsInLine = line.Split(' ');
                                    foreach (var singleWord in wordsInLine)
                                    {
                                        // Hand over to addWordsToWordCounter and continue reading.
                                        words.Add(singleWord);
                                    }

                                }
                            }
                        }
                        catch (Exception)
                        {
                            continue;
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
                foreach (var word in words.GetConsumingEnumerable())
                {
                    WordCounterService.AddWordToCollection(word);
                }
            });

            Task.WaitAll(readWordsFromFile, addWordsToWordCounter);
        }
    }
}
