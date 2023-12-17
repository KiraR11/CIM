using Model_MathOperation;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CIM
{
    class PlotViewModel
    {
        public PlotViewModel(List<PointF> plots) 
        {
            try
            {
                // определяем линию: цвет толщину и легенду
                var line1 = new OxyPlot.Series.LineSeries()
                {
                    Title = $"Series 1",
                    Color = OxyPlot.OxyColors.Blue,
                    StrokeThickness = 1,
                };

                PlotModel = new PlotModel { Title = "Example 1" };

                foreach (var item in plots)
                {
                    line1.Points.Add(new DataPoint(item.Y, item.X));
                }
                PlotModel.Series.Add(line1);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public PlotModel PlotModel { get; set; }
    }
}
