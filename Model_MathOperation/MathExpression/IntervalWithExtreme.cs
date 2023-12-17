using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_MathOperation.MathExpression
{
    internal class IntervalWithExtreme
    {
        public IntervalWithExtreme(float firstArgument, float secondArgument) 
        {
            if (!firstArgument.Equals(secondArgument))
            {
                StartPoint = firstArgument;
                EndPoint = secondArgument;
            }
            else throw new ArgumentException("начало и конец интервала не должны совпадать");
        }
        public float StartPoint { get; }
        public float EndPoint { get; }

    }
}
