using System.Text;

namespace Math_Model.MathExpression
{
    public class Calculator
    {
        public Calculator(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                expression = RemoveSpaces(expression);
                CheckingCorrectMathExpression.IsCorrectExpression(expression);
                Expression = expression;
                ExpressionWithOpeningBrackets = OpeningBrackets(expression);
            }
            else throw new ArgumentException("входное выражение было пустым");
        }
        public string Expression { get; }
        private string ExpressionWithOpeningBrackets { get; }
        public float? _result = null;
        public float Result
        {
            get
            {
                if (_result == null) _result = StepCalculate(ExpressionWithOpeningBrackets);
                return (float)_result;
            }
        }
        readonly string _format = "#0.####################";
        private static float StepCalculate(string expression)
        {
            while (true)
            {
                int i0 = expression.LastIndexOf("+");
                if (i0 >= 0) return StepCalculate(expression.Substring(0, i0)) + StepCalculate(expression.Substring(i0 + 1));

                int i1 = expression.LastIndexOf("-");
                if (i1 > 0 && !CheckingCorrectMathExpression.IsOperation(expression[i1 - 1])) return StepCalculate(expression.Substring(0, i1)) - StepCalculate(expression.Substring(i1 + 1));

                int i2 = expression.LastIndexOf("*");
                if (i2 >= 0) return StepCalculate(expression.Substring(0, i2)) * StepCalculate(expression.Substring(i2 + 1));

                int i3 = expression.LastIndexOf("/");
                if (i3 >= 0) 
                {
                    float del = StepCalculate(expression.Substring(i3 + 1));
                    if (del != 0)
                        return StepCalculate(expression.Substring(0, i3)) / del;
                    else
                        throw new Exception("деление на ноль");
                }

                int i4 = expression.LastIndexOf("^");
                if (i4 >= 0) return  (float)Math.Pow(StepCalculate(expression.Substring(0, i4)), StepCalculate(expression.Substring(i4 + 1)));

                int i5 = expression.LastIndexOf("√");
                if (i5 >= 0) return (float)Math.Sqrt(StepCalculate(expression.Substring(i5 + 1)));


                float number;
                if (float.TryParse(expression, out number))
                    return number;
                else throw new Exception("не удалось преобразовать строку в число");
            }
        }

        private static string OpeningBrackets(string expression)
        {
            while (CheckExistenceBrackets(expression))
            {
                int indexOpeningBracket = expression.LastIndexOf("(");
                int indexCloseingBracket = FindIndexCloseingBracket(indexOpeningBracket, expression);

                string leftPartOutsideBracket = expression.Substring(0, indexOpeningBracket);
                string rigthPartOutsideBracket = expression.Substring(indexCloseingBracket + 1);

                double valueInBracket = StepCalculate(expression.Substring(indexOpeningBracket + 1, indexCloseingBracket - indexOpeningBracket - 1));
                expression = leftPartOutsideBracket + valueInBracket + rigthPartOutsideBracket;
            }
            return expression;
        }
        private static int FindIndexCloseingBracket(int indexOpeningBracket, string expression)
        {
            int indexCloseingBracket;
            if (indexOpeningBracket > expression.IndexOf(")"))
                indexCloseingBracket = expression.LastIndexOf(")");
            else
                indexCloseingBracket = expression.IndexOf(")");
            return indexCloseingBracket;
        }
        private static bool CheckExistenceBrackets(string expression)
        {
            if (expression.IndexOf("(") == -1)
                return false;
            else return true;
        }
        private static string RemoveSpaces(string expression)
        {
            expression = expression.Trim();
            string[] GrypExpression = expression.Split(' ');
            var sb = new StringBuilder();
            sb.Append(string.Join(string.Empty, GrypExpression));
            return sb.ToString();
        }
        private static bool IsOperation(char operation)
        {
            switch (operation)
            {
                case '+': return true;
                case '-': return true;
                case '/': return true;
                case '*': return true;
                case '^': return true;
                case '√': return true;
                default: return false;
            }
        }
    }
}