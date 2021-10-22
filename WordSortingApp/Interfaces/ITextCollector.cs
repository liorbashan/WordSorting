using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSortingApp.Interfaces
{
    public interface ITextCollector
    {
        public void ReadAndAddToCollection(string[] source);
    }
}
