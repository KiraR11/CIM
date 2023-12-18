using Model_MathOperation.MathExpression;

namespace TestingModel
{
    public class TestingEquation
    {
        [Fact]
        public void Replace_argument_in_Equation_with_sigh_multiplication()
        {
            Equation equation = new Equation("2*x + 5");


            string expression = equation.ReplaceArgument("3");

            string expected = "2*3 + 5";

            Assert.Equal(expected, expression);

        }
        [Fact]
        public void Replace_argument_in_Equation_without_sigh_multiplication()
        {
            Equation equation = new Equation("2x + 5");

            string expression = equation.ReplaceArgument("3");

            string expected = "2*3 + 5";

            Assert.Equal(expected, expression);


        }
        [Fact]
        public void Replace_argument_in_Equation_without_arguments()
        {
            Equation equation = new Equation("2 + 5");

            Action action = () => { equation.Solve(3f); };

            Assert.Throws<ArgumentException>(action);

        }
        [Fact]
        public void Replace_argument_in_PolinomEquation_first_point()
        {
            Equation equationPolinom = new Equation("48 - 52*x + 26*x^2 -4*x^3");
            Equation equation = new Equation("2*x^2+16/x");

            float expressionPolinom = equationPolinom.Solve(1);

            float expression = equation.Solve(1);

            Assert.Equal(expression, expressionPolinom);
        }
        [Fact]
        public void Replace_argument_in_PolinomEquation_second_point()
        {
            Equation equationPolinom = new Equation("48 - 52*x + 26*x^2 -4*x^3");
            Equation equation = new Equation("2*x^2+16/x");

            float expressionPolinom = equationPolinom.Solve(2);

            float expression = equation.Solve(2);

            Assert.Equal(expression, expressionPolinom);
        }
    }
}
