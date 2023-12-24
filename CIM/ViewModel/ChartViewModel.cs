using Model_MathOperation;
using Model_MathOperation.MathExpression;
using OxyPlot;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CIM
{
    class ChartViewModel
    {
        public ChartViewModel(WpfPlot plotModel) 
        {
            PlotModel = plotModel;
        }
        public WpfPlot PlotModel { get; set; }

        public void ChartClear()
        {
            PlotModel.Plot.Clear();
        }
        public void PaintInterpolationPolinoms(List<ResultCIM> points)
        {
            double[][] PolinomsX = new double[points.Count][];
            double[][] PolinomsY = new double[points.Count][];

            int j = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].PolinomInterval.Count != 0)
                {
                    j++;
                    PolinomsX[i] = points[i].PolinomInterval.Select(point => (double)point.X).ToArray();
                    PolinomsY[i] = points[i].PolinomInterval.Select(point => (double)point.Y).ToArray();

                    if (j == 1)
                        PlotModel.Plot.AddScatter(PolinomsX[i], PolinomsY[i], Color.Blue, label: $"InterpolationPolinom");
                    else
                        PlotModel.Plot.AddScatter(PolinomsX[i], PolinomsY[i], Color.Blue);
                }
            }
            PlotModel.Plot.Legend();
            PlotModel.Refresh();
        }
        public void PaintOptimavationProgress(List<ResultCIM> points)
        {
            double[] dataX = new double[points.Count];
            double[] dataY = new double[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                dataX[i] = points[i].Optimal.X;
                dataY[i] = points[i].Optimal.Y;

                var myDraggableMarker = new ScottPlot.Plottable.DraggableMarkerPlot()
                {
                    X = points[i].Optimal.X,
                    Y = points[i].Optimal.Y,
                    Color = Color.Green,
                    MarkerSize = 8,
                    Text = $"x{i}",
                };
                myDraggableMarker.TextFont.Size = 15;

                PlotModel.Plot.Add(myDraggableMarker);
            }
            PlotModel.Plot.AddScatter(dataX, dataY, Color.Green, label: "Optimization");

            PlotModel.Plot.Legend();
            PlotModel.Refresh();
        }
    }
}
