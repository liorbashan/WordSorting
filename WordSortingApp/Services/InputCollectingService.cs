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
        public void CreateCollectionFromInputs(string[] files, string[] urls)
        {
            var collectFromFiles = Task.Run(() =>
            {
                _fileTextCollectorAdapter.ReadAndAddToCollection(files);
            });
            var collectFromUrls = Task.Run(() =>
            {
                _urlTextCollectorAdapter.ReadAndAddToCollection(urls);

            });

            Task.WaitAll(collectFromFiles, collectFromUrls);
        }
    }
}
