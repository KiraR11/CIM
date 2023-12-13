namespace TestingModel
{
    public class TestingErrorsInExpression
    {
        [Fact]
        public void Errors_double_operation_with_inсorrect_multiplication_by_a_negative_number()
        {
            string expression = "3 * -1";

            Action action = () => { new Calculator(expression); };

            Assert.Throws<Exception>(action);
        }
        [Fact]
        public void Errors_double_operation_with_two_signs_addition()
        {
            string expression = "3 ++ 1";

            Action action = () => { new Calculator(expression); };

            Assert.Throws<Exception>(action);
        }
    }
}
