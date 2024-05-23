using MIS.Application._Enums;
using System.Collections.Generic;

namespace MIS.Application._ViewModels
{
    public class GridViewQuery
    {
        public List<FieldValuePair> Filters { get; set; }

        public string SortKey { get; set; }

        public SortDirection SortDirection { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
