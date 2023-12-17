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

            float expression = equation.Solve(3f);

            float expected = 7;

            Assert.Equal(expected, expression);

        }
    }
}
