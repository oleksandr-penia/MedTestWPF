using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTestWPF
{
    internal class Test
    {
        public Test()
        {
            Image = @"/Resources/Static/Images/NoImage.png";
        }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }

        public string FormattedTime
        {
            get
            {
                return $"{Date.Hour,2:D2}:{Date.Minute,2:D2}";
            }
        }
    }
}
