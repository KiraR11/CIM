using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Math_Model;
using Math_Model.MathExpression;
using Model_MathOperation;
using OxyPlot;
using OxyPlot.Series;
using ScottPlot;
using ScottPlot.WPF;

namespace CIM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CubicInterpolation cubicInterpolation;
            try
            {
                Equation fun = new Equation(tb_Fun.Text);
                Equation defFun = new Equation(tb_DefFun.Text);
                double startPoint = Double.Parse(tb_StartPoint.Text);
                double step = Double.Parse(tb_Step.Text);
                double accuracy = Double.Parse(tb_Accuracy.Text);
                cubicInterpolation = new CubicInterpolation(fun, defFun, startPoint, step, accuracy);


                List<PointDouble> points = cubicInterpolation.FindAbsoluteMin();
                InputTable(points);
                InputChart(points);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректные данные", $"Ошибка: {ex.Message}");
            }
            

        }
        private void InputTable(List<PointDouble> points) 
        { 
            dg_OutputResult.ItemsSource = points;
        }
        private void InputChart(List<PointDouble> points)
        {
            List<OxyPlot.DataPoint> pointsGrup = ConvertPointDoubleInDataPoint(points);
            chart.Model = new PlotModel { Title = "Метод кубической интерполяции" };
            FunctionSeries fs = new FunctionSeries();
            fs.Points.AddRange(pointsGrup);
            chart.Model.Series.Add(fs);
            
            chart.ResetAllAxes();
            chart.Focus();
        }
        private List<OxyPlot.DataPoint> ConvertPointDoubleInDataPoint(List<PointDouble> pointsDouble)
        {
            List<OxyPlot.DataPoint> pointsGrup = new List<OxyPlot.DataPoint>();

            foreach (PointDouble pointDouble in pointsDouble)
            {
                OxyPlot.DataPoint x = new OxyPlot.DataPoint(pointDouble.X,pointDouble.Y);
                pointsGrup.Add(x);
            }
            return pointsGrup;
        }

    }
}
