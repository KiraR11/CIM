using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows;
using Math_Model;
using Math_Model.MathExpression;
using Model_MathOperation.MathExpression;
using ScottPlot;

namespace CIM
{
    public partial class MainWindow : Window
    {
        private ChartViewModel ChartViewModel;
        public MainWindow()
        {
            InitializeComponent();
            ChartViewModel = new ChartViewModel(chart_scottPlot);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ResultCIM> resultPoints = CalcResultPoints();

                OutputResults(resultPoints);
            }
            catch (ArgumentException ex)
            {
                OutputErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                OutputErrorMessage(ex.Message);
            }
        }
        private List<ResultCIM> CalcResultPoints()
        {
            float startPoint = GetStartPoint();
            float step = GetStep();
            float accuracy = GetAccuracy();
            int countIterution = GetCountIteration();
            
            Equation fun = GetFun();
            Equation defFun = GetDerivativeFun();

            CubicInterpolation cubicInterpolation = new CubicInterpolation(fun, defFun, startPoint, step, accuracy,countIterution);
            return cubicInterpolation.FindAbsoluteMin();
        }

        private void OutputResults(List<ResultCIM> resultPoints)
        {
            ChartViewModel.ChartClear();

            PrintTable(resultPoints);
            ChartViewModel.PaintOptimavationProgress(resultPoints);

            if(CheckRenderingResolution())
                ChartViewModel.PaintInterpolationPolinoms(resultPoints);

            ResultCIM minPoint = FindMinPoint(resultPoints);
            PrintResultPoint(minPoint);
        }

        private void PrintResultPoint(ResultCIM minPoint)
        {
            tb_resultPoint.Text = $"Найденный минимум:\n ({minPoint.Optimal.X};{minPoint.Optimal.Y})";
        }

        private ResultCIM FindMinPoint(List<ResultCIM> resultPoints)
        {
            ResultCIM resultCIM = resultPoints[0];
            foreach (ResultCIM resultPoint in resultPoints)
            {
                if(resultPoint.Optimal.Y < resultCIM.Optimal.Y)
                    resultCIM = resultPoint;
            }
            return resultCIM;
        }

        private bool CheckRenderingResolution()
        {
            bool? result = cb_printPaintPolinom.IsChecked;
            if (result != null)
                return result.Value;
            else 
                return false;
        }
        private float GetStartPoint()
        {
            if (CheckCorrectSingleValue(tb_StartPoint.Text))
            {
                return Single.Parse(tb_StartPoint.Text);
            }
            else throw new ArgumentException("Некорректные данные в веденной начальной точке");
        }

        private int GetCountIteration()
        {
            if (CheckCorrectSingleValue(tb_CountIterution.Text))
            {
                return Int32.Parse(tb_CountIterution.Text);
            }
            else throw new ArgumentException("Некорректные данные в веденном максимальном количестве итераций");
        }

        private float GetStep()
        {
            if (CheckCorrectSingleValue(tb_Step.Text))
            {
                return Single.Parse(tb_Step.Text);
            }
            else throw new ArgumentException("Некорректные данные в веденном шаге");
        }
        private float GetAccuracy()
        {
            if (CheckCorrectSingleValue(tb_Accuracy.Text))
            {
                return Single.Parse(tb_Accuracy.Text);
            }
            else throw new ArgumentException("Некорректные данные в веденной точности");
        }
        private bool CheckCorrectSingleValue(string inputValue)
        {
            if (string.IsNullOrEmpty(inputValue))
                return false;
            else
            {
                try
                {
                    Single.Parse(inputValue);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private Equation GetFun()
        {
            return new Equation(tb_Fun.Text);
        }
        private Equation GetDerivativeFun()
        {
            return new Equation(tb_DefFun.Text);
        }
        private void OutputErrorMessage(string messageThis)
        {
            MessageBox.Show(messageThis,"Ошибка");
        }
        private void PrintTable(List<ResultCIM> points) 
        {
            DataTable table = new DataTable();

            table.Columns.Add("x№");
            table.Columns.Add("координата x");
            table.Columns.Add("значение функции");
           
            for (int i = 0; i < points.Count; i++)
            {
                var newRow = table.NewRow();
                newRow["x№"] = i;
                newRow["координата x"] = points[i].Optimal.X;
                newRow["значение функции"] = points[i].Optimal.Y;
                table.Rows.Add(newRow);
            }
            dg_OutputResult.DataContext = table;
        }
    }
}
