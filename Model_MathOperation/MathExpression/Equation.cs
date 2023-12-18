using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Math_Model.MathExpression
{
    public class Equation
    {
        public Equation(string formula)
        {
            if (!string.IsNullOrEmpty(formula))
            {
                CheckingCorrectMathExpression.IsCorrectFormula(formula);
                Formula = formula;
            }
            else throw new ArgumentException("входное выражение было пустым");
            Formula = formula;
        }
        public string Formula { get; }

        public float Solve(float argument)
        {
            string argumentStr = ArgumemtToString(argument);
            string expression = ReplaceArgument(argumentStr);
            Calculator calculator = new Calculator(expression);
            return calculator.Result;
        }
        public string ReplaceArgument(string argumentStr)
        {
            string[] partsFormula = SplitBySubEquatoin();
            for (int i = 0; i < partsFormula.Length - 1; i++)
            {
                int indexEndPartForlula = partsFormula[i].Length - 1;
                if (char.IsDigit(partsFormula[i][indexEndPartForlula]))
                    partsFormula[i] += '*' + argumentStr;
                else
                    partsFormula[i] += argumentStr;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Join(string.Empty, partsFormula));
            string expression = sb.ToString();

            return expression;
        }
        private string ArgumemtToString(float argument)
        {
            var format = "#############0.####################";
            string argumentStr;
            if (argument < 0)
                argumentStr = '(' + argument.ToString(format) + ')';
            else
                argumentStr = argument.ToString();
            return argumentStr;
        }
        private string[] SplitBySubEquatoin()
        {
            string[] partsFormula = Formula.Split('x');
            if (partsFormula.Length == 1)
                throw new ArgumentException("функция не имеет extr");
            else
                return partsFormula;
        }
    }
}
