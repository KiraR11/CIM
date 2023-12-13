using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_Model.MathExpression;

namespace Testing
{
    public class TestingEquation
    {
        [Fact]
        public void Replace_argument_in_Equation_with_sigh_multiplication()
        {
            Equation equation = new Equation("2*x + 5");

            string expression = equation.ReplaceArgument(3);

            string expected = "2*3 + 5";

            Assert.Equal(expected, expression);

        }
        [Fact]
        public void Replace_argument_in_Equation_without_sigh_multiplication()
        {
            Equation equation = new Equation("2x + 5");

            string expression = equation.ReplaceArgument(3);

            string expected = "2*3 + 5";

            Assert.Equal(expected, expression);

        }
    }
}
