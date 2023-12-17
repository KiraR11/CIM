using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_MathOperation.MathExpression
{
    public class ResultCIM
    {
        public ResultCIM(PointF firstPoint, PointF secondPoint, PointF optimal, List<PointF> polinomInterval)
        {
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            Optimal = optimal;
            PolinomInterval = polinomInterval;
        }
        public ResultCIM(PointF optimal)
        {
            Optimal = optimal;
        }

        public PointF? FirstPoint { get; }
        public PointF? SecondPoint { get; }
        public PointF Optimal { get; }
        public List<PointF> PolinomInterval { get; } = new List<PointF>();
    }
}
