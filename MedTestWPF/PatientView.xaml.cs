using ScottPlot;
using ScottPlot.Renderable;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MedTestWPF
{
    /// <summary>
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : Window
    {
        public PatientView()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk-UA");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            InitializeComponent();

            var dataContext = new PatientViewModel();
            dataContext.PlotDataChanged += RefreshPlot;
            DataContext = dataContext;

            var padding = new ScottPlot.PixelPadding(35, 5, 70, 0);
            MeasurementsPlot.Plot.ManualDataArea(padding);
            MeasurementsPlot.Plot.XAxis.DateTimeFormat(true);
            MeasurementsPlot.Plot.XAxis.TickLabelStyle(rotation: 45);
            MeasurementsPlot.Plot.XAxis.TickDensity(1.5);
            MeasurementsPlot.Plot.YAxis.TickDensity(2);
        }

        public void OnSearchTextBoxTextChanged(object sender, EventArgs e)
        {
            if (Search.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"./Resources/Static/Images/SearchBG.png", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;
                // Use the brush to paint the button's background.
                Search.Background = textImageBrush;
            }
            else
            {
                Search.Background = null;
            }
        }

        public void RefreshPlot()
        {
            var data = DataContext as PatientViewModel;
            if (data != null)
            {
                if (data.SelectedMeasurementType != null)
                {
                    var filtered = data.Measurements.Where(x => x.Type == data.SelectedMeasurementType);
                    var values = filtered.Select(x => x.Value).ToArray();
                    var marks = filtered.Select(x => x.Date.ToOADate()).ToArray();
                    var avg = (values.Max() + values.Min()) / 2;
                    (double[] smoothXs, double[] smoothYs) = ScottPlot.Statistics.Interpolation.Cubic.InterpolateXY(marks, values, 10 * values.Length);

                    var pillsDates = data.Pills.Select(x => x.Date.ToOADate()).ToArray();
                    var pillValues = Enumerable.Repeat(avg, pillsDates.Length).ToArray();
                    
                    MeasurementsPlot.Plot.Clear();
                    MeasurementsPlot.Plot.AddScatterLines(smoothXs, smoothYs, System.Drawing.Color.Green);
                    MeasurementsPlot.Plot.AddScatter(pillsDates, pillValues, System.Drawing.Color.Red, 0, markerSize: 5, markerShape: MarkerShape.filledDiamond);
                    MeasurementsPlot.Refresh();
                }
            }
            
        }
    }
}
