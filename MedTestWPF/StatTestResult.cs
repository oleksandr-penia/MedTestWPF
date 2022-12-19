using MedTestWPF.Resources.Static.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedTestWPF
{
    public class StatTestResult
    {
        public DependencyTestResult TestResult { get; set; }
        public string PillName { get; set; }
        public string MeasurementName { get; set; }
        public double AverageChange { get; set; }
        
        // Inferred properties for WPF display
        public string ImagePath
        {
            get
            {
                switch (TestResult)
                {
                    case DependencyTestResult.Increase:
                        return "/Resources/Static/Images/Increase.png";
                    case DependencyTestResult.IncreaseInconclusive:
                        return "/Resources/Static/Images/IncreaseInconclusive.png";
                    case DependencyTestResult.Decrease:
                        return "/Resources/Static/Images/Decrease.png";
                    case DependencyTestResult.DecreaseInconclusive:
                        return "/Resources/Static/Images/DecreaseInconclusive.png";
                    case DependencyTestResult.None:
                    default:
                        return "/Resources/Static/Images/None.png";
                }
            }
        }
        public string ToolTipText
        {
            get
            {
                switch (TestResult)
                {
                    case DependencyTestResult.Increase:
                        return String.Format(Strings.AvgIncrease, AverageChange);
                    case DependencyTestResult.IncreaseInconclusive:
                        return String.Format(Strings.AvgIncreaseIncoclusive, AverageChange);
                    case DependencyTestResult.Decrease:
                        return String.Format(Strings.AvgDecrease, AverageChange);
                    case DependencyTestResult.DecreaseInconclusive:
                        return String.Format(Strings.AvgDecreaseInconclusive, AverageChange);
                    case DependencyTestResult.None:
                    default:
                        return Strings.AvgNone;
                }
            }
        }

        public StatTestResult(string pill, string measure, DependencyTestResult testResult, double avgChange)
        {
            TestResult = testResult;
            PillName = pill;
            MeasurementName = measure;
            AverageChange = avgChange;
        }

        public StatTestResult() { }
    }
}
