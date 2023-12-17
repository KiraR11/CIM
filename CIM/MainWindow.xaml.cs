using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
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
            Equation fun;
            Equation defFun;
            CubicInterpolation cubicInterpolation;
            double startPoint;
            double step;
            double accuracy;
            List<PointDouble> points;

            try
            {
                startPoint = Double.Parse(tb_StartPoint.Text);
                step = Double.Parse(tb_Step.Text);
                accuracy = Double.Parse(tb_Accuracy.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}","Некорректные данные в параменрах метода");
                return;
            }

            if (CheckCoorectFunInput(tb_Fun.Text))
                fun = new Equation(tb_Fun.Text);
            else return;

            if (CheckCoorectFunInput(tb_DefFun.Text))
                defFun = new Equation(tb_DefFun.Text);
            else return;


            try
            {
                cubicInterpolation = new CubicInterpolation(fun, defFun, startPoint, step, accuracy);
                points = cubicInterpolation.FindAbsoluteMin();
                InputTable(points);
                InputChartScottPlot(points);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Некорректные данные при подсчете");
                return;
            }

            try
            {
                InputTable(points);
                InputChartScottPlot(points);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка при отрисовке");
            }
        }
        public bool CheckCoorectFunInput(string textFun)
        {
            try
            {
                Equation equation = new Equation(textFun);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Некорректные данные в функциях");
                return false;
            }
        }
        private void InputTable(List<PointDouble> points) 
        { 
            dg_OutputResult.ItemsSource = points;
        }
        private void InputChartScottPlot(List<PointDouble> points)
        {
            chart_scottPlot.Plot.Clear();
             double[] dataX = points.Select(point => point.X).ToArray();
             double[] dataY = points.Select(point => point.Y).ToArray();

            chart_scottPlot.Plot.AddScatter(dataX,dataY);
            chart_scottPlot.Refresh();
        }
    }
}
