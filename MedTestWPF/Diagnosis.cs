using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MedTestWPF
{
    internal class Diagnosis
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string FormattedDate
        {
            get
            {
                return $"{Date.Day,2:D2}-{Date.Month,2:D2}-{Date.Year}";
            }
        }
    }
}
