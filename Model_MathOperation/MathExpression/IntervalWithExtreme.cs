using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_MathOperation.MathExpression
{
    internal class IntervalWithExtreme
    {
        public IntervalWithExtreme(double firstArgument, double secondArgument) 
        {
            if (!firstArgument.Equals(secondArgument))
            {
                StartPoint = firstArgument;
                EndPoint = secondArgument;
            }
            else throw new ArgumentException("начало и конец интервала не должны совпадать");
        }
        public double StartPoint { get; }
        public double EndPoint { get; }

    }
}
