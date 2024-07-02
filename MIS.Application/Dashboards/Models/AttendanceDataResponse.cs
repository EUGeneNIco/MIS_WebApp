using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Dashboards.Models
{
    public class AttendanceDataResponse
    {
        public List<AttendanceDataSet> Datasets { get; set; }
    }

    public class AttendanceDataSet
    {
        public DateTime DateTime { get; set; }
        public string DateTimeLabel { get { return DateTime.ToString("ddd, MMM dd"); } }
        public int GuestsCount { get; set; }
        public int MembersCount { get; set; }
    }
}
