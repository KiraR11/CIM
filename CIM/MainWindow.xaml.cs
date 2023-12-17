using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Math_Model;
using Math_Model.MathExpression;
using Model_MathOperation;
using Model_MathOperation.MathExpression;
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
            float startPoint;
            float step;
            float accuracy;
            List<ResultCIM> points;

            try
            {
                startPoint = (float)Double.Parse(tb_StartPoint.Text);
                step = (float)Double.Parse(tb_Step.Text);
                accuracy = (float)Double.Parse(tb_Accuracy.Text);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Некорректные данные при подсчете");
                return;
            }

            try
            {
                //InputTable(points);
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
        private void InputTable(List<PointF> points) 
        { 
            dg_OutputResult.ItemsSource = points;
        }
        private void InputChartScottPlot(List<ResultCIM> points)
        {
            chart_scottPlot.Plot.Clear();

            double[] dataX = new double[points.Count];
            double[] dataY = new double[points.Count];

            double[][] PolinomsX = new double[points.Count][];
            double[][] PolinomsY = new double[points.Count][];

            for (int i = 0; i < points.Count; i++)
            {
                dataX[i] = points[i].Optimal.X;
                dataY[i] = points[i].Optimal.Y;
                if (points[i].PolinomInterval.Count != 0)
                {
                    PolinomsX[i] = points[i].PolinomInterval.Select(point => (double)point.X).ToArray();
                    PolinomsY[i] = points[i].PolinomInterval.Select(point => (double)point.Y).ToArray();

                    chart_scottPlot.Plot.AddScatter(PolinomsX[i], PolinomsY[i], Color.Blue, label: "Polinom");
                }
            }
            
            chart_scottPlot.Plot.AddScatter(dataX,dataY, Color.Green, label: "Optimization");
            // plot the original vs interpolated lines
            chart_scottPlot.Plot.Legend();
            chart_scottPlot.Refresh();
        }
    }
}
