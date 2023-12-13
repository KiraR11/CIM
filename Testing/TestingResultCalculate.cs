using MathExpression;

namespace Testing
{
    public class TestingResultCalculate
    {
        [Fact]
        public void Simple_adding_two_numbers()
        {
            string expression = "3 + 2";

            double result = new Calculator(expression).Result;

            Assert.Equal(5, result);
        }
        [Fact]
        public void Simple_subtraction_two_numbers()
        {
            string expression = "3 - 2";

            double result = new Calculator(expression).Result;

            Assert.Equal(1, result);
        }
        [Fact]
        public void Simple_multiplication_two_numbers()
        {
            string expression = "3 * 2";

            double result = new Calculator(expression).Result;

            Assert.Equal(6, result);
        }
        [Fact]
        public void Simple_division_two_numbers()
        {
            string expression = "3 / 2";

            double result = new Calculator(expression).Result;

            Assert.Equal(3 / 2.0, result);
        }
        [Fact]
        public void Adding_two_numbers_is_double()
        {
            string expression = "3,0 + 2,0";

            double result = new Calculator(expression).Result;

            Assert.Equal(5.0, result);
        }
        [Fact]
        public void Multiplication_by_the_result_of_addition_in_parentheses()
        {
            string expression = "3 * (4 + 5)";

            double result = new Calculator(expression).Result;

            double expected = 3.0 * (4 + 5);

            Assert.Equal(expected, result);
        }
        [Fact]
        public void Multiplication_of_the_results_of_additions_in_two_brackets()
        {
            string expression = "(1 + 2) * (4 + 5)";

            double result = new Calculator(expression).Result;

            double expected = (1 + 2) * (4 + 5);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Сorrect_multiplication_by_a_negative_number()
        {
            string expression = "3 * (-1)";

            double result = new Calculator(expression).Result;

            double expected = 3 * (-1);

            Assert.Equal(expected, result);
        }
        [Fact]
        public void Сorrect_multiplication_by_a_negative_subtraction()
        {
            string expression = "3 * (2 - 3)";

            double result = new Calculator(expression).Result;

            double expected = 3 * (2 - 3);

            Assert.Equal(expected, result);
        }
        [Fact]
        public void Prioritizing_multiplication_over_addition()
        {
            string expression = "3,1 + 2 * 4";

            double result = new Calculator(expression).Result;

            double expected = 3.1 + 2 * 4;

            Assert.Equal(expected, result);
        }
        [Fact]
        public void CorrectResultOfCalculatingTheComplexDegeneracy()
        {
            string expression = "100,21 * √(21/(-15 + 5))";

            double result = new Calculator(expression).Result;

            double expected = 100.21 * Math.Sqrt(21 / (-15 + 5));

            Assert.Equal(expected, result);
        }
    }
}