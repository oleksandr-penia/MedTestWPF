using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTestWPF
{
    public class Measurement
    {
        public string Type { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Type + Value.ToString() + Date.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            var m = obj as Measurement;
            return m != null && m.Type == Type && m.Value == Value && m.Date == Date;

        }
    }
}
