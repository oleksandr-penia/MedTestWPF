using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTestWPF
{
    public class Pill
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Dosage { get; set; }

        public override bool Equals(object? obj)
        {
            var cmp = obj as Pill;
            return cmp != null
                && Date == cmp.Date
                && Type == cmp.Type
                && Dosage == cmp.Dosage;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}", Date, Type, Dosage);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
