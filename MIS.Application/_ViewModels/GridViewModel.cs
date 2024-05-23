using System.Collections.Generic;

namespace MIS.Application._ViewModels
{
    public class GridViewModel<T>
    {
        public int TotalDataCount { get; set; }

        public IEnumerable<T> Data { get; set; }

        public int FilteredDataCount { get; set; }
    }
}
