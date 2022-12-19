using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace MedTestWPF
{
    internal class PatientModel
    {
        public PatientModel()
        {
            Name = "Василь";
            Surname = "Василенко";
            Patronimic = "Васильович";
            BirthDate = new DateTime(1966, 8, 12);
            Id = "FD4820015";
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronimic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Id { get; set; }

        public IEnumerable<Test> Tests
        {
            get
            {
                return new List<Test>
                {
                    new Test() { Name = "Загальний аналіз крові", Date = new DateTime(2022, 12, 10, 8, 35, 0), Area = "Кардіологія", Description="Lorem ipsum dolor est Text 1" },
                    new Test() { Name = "Кардіограма", Date = new DateTime(2022, 12, 10, 9, 10, 0), Area = "Кардіологія", Image=@"/Resources/Dynamic/Images/cardio1.png", Description="Lorem ipsum dolor est Text 1"},
                    new Test() { Name = "КТ Легень", Date = new DateTime(2022, 12, 10, 10, 0, 0), Area = "Пульманологія", Image=@"/Resources/Dynamic/Images/kt1.png", Description="Lorem ipsum dolor est Text 2"},
                    new Test() { Name = "УЗД нирок", Date = new DateTime(2022, 12, 10, 11, 15, 0), Area = "Урологія", Image=@"/Resources/Dynamic/Images/uzd1.png", Description="Lorem ipsum dolor est  Text 3"},

                    new Test() { Name = "Кардіограма", Date = new DateTime(2022, 12, 2, 9, 10, 0), Area = "Кардіологія", Image=@"/Resources/Dynamic/Images/cardio1.png", Description="Lorem ipsum dolor est Text 1"},
                    new Test() { Name = "КТ Легень", Date = new DateTime(2022, 12, 2, 10, 0, 0), Area = "Пульманологія", Image=@"/Resources/Dynamic/Images/kt1.png", Description="Lorem ipsum dolor est Text 2"},

                    new Test() { Name = "Загальний аналіз сечі", Date = new DateTime(2022, 9, 2, 8, 35, 0), Area = "Урологія" },
                    new Test() { Name = "КТ", Date = new DateTime(2022, 9, 2, 9, 10, 0), Area = "Неврологія"},
                    new Test() { Name = "Аналіз калу", Date = new DateTime(2022, 9, 2, 10, 0, 0), Area = "Гастроентерологія" },
                    new Test() { Name = "Ааааа", Date = new DateTime(2022, 9, 2, 11, 15, 0), Area = "Отолорингологія"},
                };
            }
        }

        public IEnumerable<Diagnosis> Diagnoses
        {
            get
            {
                return new List<Diagnosis>
                {
                    new Diagnosis() { Name = "Плеврит", Date = new DateTime(2022, 02, 05) },
                    new Diagnosis() { Name = "Кардіосклероз", Date = new DateTime(2021, 11, 23) },
                    new Diagnosis() { Name = "Стенокардія напруження", Date = new DateTime(2021, 2, 14) },
                    new Diagnosis() { Name = "Пневмонія", Date = new DateTime(2020, 02, 18) },
                };
            }
        }

        public IEnumerable<Measurement> Measurements
        {
            get
            {
                return new List<Measurement>()
                {
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 1, 10, 00, 00), Value = 37.5 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 1, 22, 00, 00), Value = 36.8 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 2, 10, 00, 00), Value = 37.5 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 2, 22, 00, 00), Value = 36.9 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 3, 10, 00, 00), Value = 37.8 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 3, 22, 00, 00), Value = 36.7 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 4, 10, 00, 00), Value = 37.9 },
                    new Measurement() { Type = "Температура", Date = new DateTime(2022, 12, 4, 22, 00, 00), Value = 36.9 },

                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 1, 10, 00, 00), Value = 95 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 1, 22, 00, 00), Value = 96 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 2, 10, 00, 00), Value = 97 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 2, 22, 00, 00), Value = 95 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 3, 10, 00, 00), Value = 94 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 3, 22, 00, 00), Value = 95 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 4, 10, 00, 00), Value = 96 },
                    new Measurement() { Type = "Тиск", Date = new DateTime(2022, 12, 4, 22, 00, 00), Value = 95 },
                };
            }
        }

        public IEnumerable<Pill> Pills
        {
            get
            {
                return new List<Pill>()
                {
                    new Pill() { Type = "Жарознижувальні", Date = new DateTime(2022, 12, 1, 12, 00, 00), Dosage = 1.45 },
                    new Pill() { Type = "Жарознижувальні", Date = new DateTime(2022, 12, 2, 12, 00, 00), Dosage = 1 },
                    new Pill() { Type = "Жарознижувальні", Date = new DateTime(2022, 12, 4, 12, 00, 00), Dosage = 1.5 },
                };
            }
        }

        public List<StatTestResult> GetAnalisys(string measureType, int timeInterval = 12, int diffTestCount = 5)
        {
            bool inconclusive = false;
            List<StatTestResult> result = new List<StatTestResult>();

            var measurementsGrouped = Measurements
                .Where(x => x.Type == measureType)
                .OrderBy(x => x.Date)
                .ToList();

            // 1. Sort Measurements into "influenced" - i.e. having an influence between them in the timeInterval given
            // and "natural" - not having any influence
            Dictionary<string, List<(Pill, double)>> influence = new Dictionary<string, List<(Pill, double)>>();
            //List<Dictionary<Pill, double>> influence = new List<Dictionary<Pill, double>>();
            Dictionary<int, double> natural = new Dictionary<int, double>();

            for (int i = 0; i < measurementsGrouped.Count - 1; i++)
            {
                if ((measurementsGrouped[i+1].Date - measurementsGrouped[i].Date).TotalHours <= timeInterval)
                {
                    var pills = Pills.Where(x => x.Date >= measurementsGrouped[i].Date && x.Date <= measurementsGrouped[i + 1].Date);
                    if (pills.Any())
                    {
                        foreach (var pill in pills.GroupBy(x => x.Type))
                        {
                            List<(Pill, double)> current;
                            string pillName = pill.First().Type;
                            if (!influence.TryGetValue(pillName, out current))
                            {
                                current = new List<(Pill, double)>();
                                foreach (var item in pill)
                                {
                                    current.Add((item, measurementsGrouped[i + 1].Value - measurementsGrouped[i].Value));
                                }
                                influence.Add(pillName, current);
                            }
                            else
                            {
                                foreach (var item in pill)
                                {
                                    current.Add((item, measurementsGrouped[i + 1].Value - measurementsGrouped[i].Value));
                                }
                            }
                        }
                    }
                    else
                    {
                        natural.Add(i, measurementsGrouped[i + 1].Value - measurementsGrouped[i].Value);
                    }
                }
                // TODO : check if this makes sense
                /*
                else
                {
                    natural.Add(0, measurementsGrouped[i + 1].Value - measurementsGrouped[i].Value);
                }*/
            }

            // 2. Look for the effect of influence on measurements
            foreach (var item in influence)
            {
                var type = item.Key;
                var dosageAmounts = item.Value.Select(x => x.Item1.Dosage).ToList();
                var paramDifference = item.Value.Select(x => x.Item2).ToList();

                // 3. If there are enough measurements to do so, compare similarity of change for "influenced" and "natural"
                // using Student's t-test or Mann-Whitney u-test. If similar -  the correlation test is inconclusive
                if (influence.Count > diffTestCount && natural.Count > diffTestCount)
                {
                    inconclusive = TTest(paramDifference, natural.Values);
                }
                else
                {
                    inconclusive = UTest(paramDifference, natural.Values);
                }


                var avg = paramDifference.Average();

                var pCorr = Correlation.Pearson(dosageAmounts, paramDifference);
                var sCorr = Correlation.Spearman(dosageAmounts, paramDifference);

                if (Math.Abs(sCorr) > 0.85)
                {
                    if (inconclusive)
                    {
                        if (sCorr < 0)
                        {
                            result.Add(new StatTestResult(type, measureType, DependencyTestResult.DecreaseInconclusive, avg));
                        }
                        else
                        {
                            result.Add(new StatTestResult(type, measureType, DependencyTestResult.IncreaseInconclusive, avg));
                        }
                    }
                    else
                    {
                        if (sCorr < 0)
                        {
                            result.Add(new StatTestResult(type, measureType, DependencyTestResult.Decrease, avg));
                        }
                        else
                        {
                            result.Add(new StatTestResult(type, measureType, DependencyTestResult.Increase, avg));
                        }
                    }
                }
                else
                {
                    result.Add(new StatTestResult(type, measureType, DependencyTestResult.None, avg));
                }
            }

            // 4. Comprise result
            return result;
        }

        private bool TTest(IEnumerable<double> x, IEnumerable<double> y, double p = 0.05)
        {
            var d1 = Statistics.MeanStandardDeviation(x);
            var d2 = Statistics.MeanStandardDeviation(y);

            var tStat = (d1.Mean - d2.Mean) / Math.Sqrt(d1.StandardDeviation/x.Count() + d2.StandardDeviation/y.Count());
            var freedom = x.Count() + y.Count() - 2;

            var pVal = 2 * (1 - StudentT.CDF(0, 1, freedom, tStat));
            return pVal <= p;
        }

        private bool UTest(IEnumerable<double> x, IEnumerable<double> y, double p = 0.05)
        {
            var series = x.Select(x => (x, 0)).Union(y.Select(y => (y, 1)).OrderBy(v => v.Item1)).ToList();
            double r1 = 0, r2 = 0;
            for (int i = 0; i < series.Count; i++)
            {
                if (series[i].Item2 == 0)
                {
                    r1 += i;
                }
                else
                {
                    r2 += i;
                }
            }
            int xCount = x.Count(), yCount = y.Count();

            var u1 = xCount * yCount + xCount * (yCount + 1) / 2.0 - r1;
            var u2 = xCount * yCount + yCount * (xCount + 1) / 2.0 - r2;

            var uStat = Math.Min(u1, u2);

            var pVal = GetCriticalUTestValue(xCount, yCount, p);
            return uStat <= pVal;
        }

        private double GetCriticalUTestValue(int xCount, int yCount, double p)
        {
            int minCount = Math.Min(xCount, yCount);
            int maxCount = Math.Max(xCount, yCount);

            return _uTestValues[minCount - 3, maxCount - 3];
        }

        private double[,] _uTestValues = new double[,]
        {
            { 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8 },
            { 0, 0, 1, 2, 3, 4, 4, 5, 6, 7, 8, 9, 10, 11, 11, 12, 13, 14 },
            { 0, 1, 2, 3, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 17, 18, 19, 20 },
            { 1, 2, 3, 5, 6, 8, 10, 11, 13, 14, 16, 17, 19, 21, 22, 24, 25, 27 },
            { 1, 3, 5, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34 },
        };
    }
}