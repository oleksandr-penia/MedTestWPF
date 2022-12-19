using System.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedTestWPF.Resources.Static.Strings;
using MedTestWPF.Resources;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MedTestWPF
{
    internal class PatientViewModel : INotifyPropertyChanged
    {
        private PatientModel _patientModel;
        private ResourceManager _resources;

        private DateTime _selectedDate;
        private Test _selectedTest;
        private bool _showTestImage;
        private bool _showTestDescription;
        private string _selectedMeasurementType;

        private Dictionary<string, List<StatTestResult>> _testResultsCache;
        public PatientViewModel()
        {
            _patientModel = new PatientModel();
            _resources = Strings.ResourceManager;
            SelectedDateTime = DateTime.Now;//new DateTime(2022, 11, 10);
            ShowTestDescription = true;
            _testResultsCache = new Dictionary<string, List<StatTestResult>>();
        }

        public string PatientFullName
        {
            get
            {
                return String.Join(' ', _patientModel.Surname, _patientModel.Name, _patientModel.Patronimic);
            }
        }

        public string PatientAge
        {
            get
            {
                int years = (int)((DateTime.Now - _patientModel.BirthDate).Days / 365.2425) - 1;
                string appendix = "";
                if (years > 4 && years < 20)
                {
                    appendix = _resources.GetString("Years3");
                }
                else
                {
                    switch (years % 10)
                    {
                        case 0:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            appendix = _resources.GetString("Years3");
                            break;
                        case 1:
                            appendix = _resources.GetString("Years1");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            appendix = _resources.GetString("Years2");
                            break;
                    }
                }

                return $"{years} {appendix}";
            }
        }

        public string PatientId
        {
            get
            {
                return _patientModel.Id;
            }
        }

        public DateTime SelectedDateTime
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDateTime));
                OnPropertyChanged(nameof(AvailableTests));
                OnPropertyChanged(nameof(Areas));
                OnPropertyChanged(nameof(DatesOfTests));
            }
        }

        public IEnumerable<Test> AvailableTests
        {
            get
            {
                return _patientModel.Tests.Where(x => x.Date.Date == SelectedDateTime.Date);
            }
        }

        public IEnumerable<Area> Areas
        {
            get
            {
                var areas = _patientModel.Tests.Where(x => !String.IsNullOrEmpty(x.Area)).Select(x => x.Area).Distinct();
                var currentTests = _patientModel.Tests.Where(x => x.Date.Month == SelectedDateTime.Month && x.Date.Year == SelectedDateTime.Year);
                return areas.Select(x => new Area() { Name = x, IsActive = currentTests.Any(y => y.Area == x) }).OrderBy(x => x.Name);
            }
        }

        public IEnumerable<Diagnosis> Diagnoses
        {
            get
            {
                return _patientModel.Diagnoses;
            }
        }

        public IEnumerable<DateTime> DatesOfTests
        {
            get
            {
                var res = _patientModel.Tests.Where(x => x.Date.Year == SelectedDateTime.Year && x.Date.Month == SelectedDateTime.Month).Select(x => x.Date.Date).Distinct();
                return res;
            }
        }

        public Test SelectedTest
        {
            get
            {
                return _selectedTest;
            }
            set
            {
                _selectedTest = value;
                OnPropertyChanged(nameof(SelectedTest));
                OnPropertyChanged(nameof(SelectedTestImage));
                OnPropertyChanged(nameof(SelectedTestDescription));
                OnPropertyChanged(nameof(ShowTestDescription));
            }
        }

        public bool ShowTestDescription
        {
            get
            {
                return _showTestDescription;
            }
            set
            {
                _showTestDescription = value;
                OnPropertyChanged(nameof(ShowTestDescription));
            }
        }

        public string SelectedTestImage
        {
            get
            {
                if (SelectedTest == null)
                {
                    return "";
                }
                return string.IsNullOrEmpty(SelectedTest.Image) ? @"/Resources/Static/Images/NoImage.png" : SelectedTest.Image;
            }
        }

        public bool ShowTestImage
        {
            get
            {
                return _showTestImage;
            }
            set
            {
                _showTestImage = value;
                OnPropertyChanged(nameof(ShowTestImage));
            }
        }
        public string SelectedTestDescription
        {
            get
            {
                if (SelectedTest == null)
                {
                    return "";
                }
                else
                {
                    return string.IsNullOrEmpty(SelectedTest.Description) ? "No Description provided" : SelectedTest.Description;
                }
            }
        }

        public IEnumerable<string> MeasurementTypes
        {
            get
            {
                return Measurements.Select(x => x.Type).Distinct();
            }
        }

        public IEnumerable<Measurement> Measurements
        {
            get
            {
                return _patientModel.Measurements.Where(x => x.Date.Month == SelectedDateTime.Month && x.Date.Year == SelectedDateTime.Year);
            }
        }

        public string SelectedMeasurementType
        {
            get
            {
                return _selectedMeasurementType;
            }
            set
            {
                _selectedMeasurementType = value;
                OnPropertyChanged(nameof(SelectedMeasurementType));
                OnPropertyChanged(nameof(TestResults));
                PlotDataChanged();
            }
        }


        public IEnumerable<string> PillTypes
        {
            get
            {
                return Pills.Select(x => x.Type).Distinct();
            }
        }

        public IEnumerable<Pill> Pills
        {
            get
            {
                return _patientModel.Pills.Where(x => x.Date.Month == SelectedDateTime.Month && x.Date.Year == SelectedDateTime.Year);
            }
        }

        public List<StatTestResult> TestResults
        {
            get
            {
                List<StatTestResult> res = new List<StatTestResult>();
                if (SelectedMeasurementType == null)
                {
                    return res;
                }

                if (_testResultsCache.TryGetValue(SelectedMeasurementType, out res))
                {
                    return res;
                }
                else
                {
                    res = _patientModel.GetAnalisys(SelectedMeasurementType);
                    _testResultsCache.Add(SelectedMeasurementType, res);
                    return res;
                }
            }
        }

        #region UI refresh events (PropertyChanged, etc.)
        public event Action PlotDataChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
