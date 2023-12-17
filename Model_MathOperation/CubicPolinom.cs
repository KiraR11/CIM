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
        public CubicPolinom(IntervalWithExtreme interval, float valueFunInFirstArgument, float valueDerFunInFirstArgument,float valueDerFunInSecondArgument, float z) 
        {
            A = valueFunInFirstArgument;

            B = valueDerFunInFirstArgument;

            C = -(valueDerFunInFirstArgument + z) / (interval.EndPoint);

            D = (valueDerFunInFirstArgument + valueDerFunInSecondArgument + 2 * z) / (3 * interval.EndPoint * interval.EndPoint);
        }

        public float A { get; }
        public float B { get; }
        public float C { get; }
        public float D { get; }

        public List<PointF> SolvePoint(IntervalWithExtreme interval,int countPoints)
        {
            List<PointF> points = new List<PointF>(countPoints);
            float step = (interval.EndPoint - interval.StartPoint) / (countPoints-1);
            for (int i = 0; i < countPoints; i++) 
            {
                float X = interval.StartPoint + step*i;
                float Y = A + B * X + C * X * X + D * X * X * X;
                points.Add(new PointF(X,Y));   
            }
            return points;
        }
    }
}
