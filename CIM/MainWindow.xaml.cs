using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
                ChartClear();
                InputTable(points);
                PaintOptimavationProgress(points);
                PaintInterpolationPolinoms(points);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка при отрисовке");
            }
        }

        private void ChartClear()
        {
            chart_scottPlot.Plot.Clear();
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
        private void InputTable(List<ResultCIM> points) 
        {
            DataTable table = new DataTable();

            table.Columns.Add("№");
            table.Columns.Add("координата x");
            table.Columns.Add("значение функции");
           
            for (int i = 0; i < points.Count; i++)
            {
                var newRow = table.NewRow();
                newRow["№"] = i + 1;
                newRow["координата x"] = points[i].Optimal.X;
                newRow["значение функции"] = points[i].Optimal.Y;
                table.Rows.Add(newRow);
            }
            dg_OutputResult.DataContext = table;
        }
        private void PaintInterpolationPolinoms(List<ResultCIM> points)
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

                    chart_scottPlot.Plot.AddScatter(PolinomsX[i], PolinomsY[i], Color.Blue, label: $"InterpolationPolinom {j}");
                }
            }
            chart_scottPlot.Plot.Legend();
            chart_scottPlot.Refresh();
        }
        private void PaintOptimavationProgress(List<ResultCIM> points)
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

                chart_scottPlot.Plot.Add(myDraggableMarker);
            }
            chart_scottPlot.Plot.AddScatter(dataX, dataY, Color.Green, label: "Optimization");
            

            chart_scottPlot.Plot.Legend();
            chart_scottPlot.Refresh();
        }
    }
}
