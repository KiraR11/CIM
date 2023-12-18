using Model_MathOperation.MathExpression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_MathOperation
{
    public class CubicPolinom
    {
        public CubicPolinom(IntervalWithExtreme interval, float valueFunInFirstArgument, float valueFunInSecondArgument, float valueDerFunInFirstArgument, float valueDerFunInSecondArgument) 
        {
            float[,] matrix = RecordMatrix(interval, valueFunInFirstArgument, valueFunInSecondArgument, valueDerFunInFirstArgument, valueDerFunInSecondArgument);
            float[] coof = SolveCoof(matrix);
            A = coof[0];
            B = coof[1];
            C = coof[2];
            D = coof[3];
            Interval = interval;
        }
        public float A { get; }
        public float B { get; }
        public float C { get; }
        public float D { get; }
        public IntervalWithExtreme Interval { get; }    


        public float FindMin()
        {
            float min;
            float discriminant = (float)Math.Pow(2 * C, 2) - 4 * 3 * D * B;
            if (discriminant < 0)
                throw new Exception("полином не имеет минимума");
            else if(discriminant == 0)
                min = (-2 * C) / (6 * D);
            else
            {
                float mixFirst = (-2*C + (float)Math.Sqrt(discriminant)) / (6*D);
                float mixSecond = (-2*C - (float)Math.Sqrt(discriminant)) / (6*D);
                if(mixFirst < mixSecond)
                    min = mixFirst;
                else
                    min = mixSecond;    
            }
            return min;

        }
        public List<PointF> SolvePoint(int countPoints)
        {
            List<PointF> points = new List<PointF>(countPoints);
            float step = (Interval.EndPoint - Interval.StartPoint) / (countPoints-1);
            for (int i = 0; i < countPoints; i++) 
            {
                float X = Interval.StartPoint + step*i;
                float Y = A + B * X + C * X * X + D * X * X * X;
                points.Add(new PointF(X,Y));   
            }
            return points;

        }
        public List<PointF> SolvePointVersionTwo(int countPoints)
        {
            List<PointF> points = new List<PointF>(countPoints);

            float X = (Interval.EndPoint - Interval.StartPoint) / 2;
            float Y = A + B * X + C * X * X + D * X * X * X;
            points.Add(new PointF(X, Y));

            
            return points;
        }
        private float[,] RecordMatrix(IntervalWithExtreme interval, float valueFunInFirstArgument, float valueFunInSecondArgument, float valueDerFunInFirstArgument, float valueDerFunInSecondArgument)
        {
            float[,] matrix = new float[4, 5];

            matrix[0, 0] = 1; 
            matrix[0, 1] = interval.StartPoint;
            matrix[0, 2] = interval.StartPoint * interval.StartPoint;
            matrix[0, 3] = interval.StartPoint * interval.StartPoint * interval.StartPoint;
            matrix[0, 4] = valueFunInFirstArgument;

            matrix[1, 0] = 1;
            matrix[1, 1] = interval.EndPoint;
            matrix[1, 2] = interval.EndPoint * interval.EndPoint;
            matrix[1, 3] = interval.EndPoint * interval.EndPoint * interval.EndPoint;
            matrix[1, 4] = valueFunInSecondArgument;

            matrix[2, 0] = 0;
            matrix[2, 1] = 1;
            matrix[2, 2] = 2 * interval.StartPoint;
            matrix[2, 3] = 3 * interval.StartPoint * interval.StartPoint;
            matrix[2, 4] = valueDerFunInFirstArgument;

            matrix[3, 0] = 0;
            matrix[3, 1] = 1;
            matrix[3, 2] = 2 * interval.EndPoint;
            matrix[3, 3] = 3 * interval.EndPoint * interval.EndPoint;
            matrix[3, 4] = valueDerFunInSecondArgument;

            return matrix;
        }
        private float[] SolveCoof(float[,] matrix)
        {
            float[] answer = new float[4];
            int j1 = 0;
            float p = 0;
            for (int i = 0; i < 4; i++)// зануление эл. под гл. диогональю
            {
                float max = float.MinValue;
                for (int j = i; j < 4; j++)//ищем главный эл. в столбце
                {
                    if (max <= matrix[j, i])
                    {
                        max = matrix[i, j];
                        j1 = j;
                    }
                }
                if (j1 < 4)
                    for (int q = 4; q >= i; q--)
                    {
                        p = matrix[j1, q];
                        matrix[j1, q] = matrix[i, q];//меняем уровнения местами
                        matrix[i, q] = p;
                    }
                float[,] testM = matrix;
                
                for (int q = 4; q >= i; q--)
                {
                    if(matrix[i, i] != 0)
                    matrix[i, q] /= matrix[i, i];//нормируем уровнение
                    if (Single.IsNaN(matrix[0, 0]))
                        Console.WriteLine("Ошибка");
                }
                    
                for (int w = i + 1; w < 4; w++)//зануление элементов
                    for (int t = 4; t >= i; t--)
                        matrix[w, t] -= matrix[i, t] * matrix[w, i];
            }
            answer[0] = matrix[4 - 1, 4];
            float sum = 0;
            for (int i = 4 - 1; i > 0; i--)//решение системы обратным ходом
            {
                for (int j = 0; j < 4 - i; j++)
                    sum += matrix[i - 1, 4 - j - 1] * answer[j];
                answer[4 - i] = matrix[i - 1, 4] - sum;
                sum = 0;
            }
            if (Single.IsNaN(answer[0]))
                Console.WriteLine("Ошибка");
            Array.Reverse(answer);
            
            return answer;
        }
    }
}
