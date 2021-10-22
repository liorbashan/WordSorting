using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSortingApp.Interfaces
{
    public interface ITextCollectorStrategy
    {
        public void CollectText(string[] source);
    }
}
