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

        public double Solve(double argument)
        {
            string expression = ReplaceArgument(argument);
            Calculator calculator = new Calculator(expression);
            return calculator.Result;
        }
        public string ReplaceArgument(double argument)
        {
            string[] partsFormula = Formula.Split('x');
            for (int i = 0; i < partsFormula.Length - 1; i++)
            {
                int indexEndPartForlula = partsFormula[i].Length - 1;
                if (char.IsDigit(partsFormula[i][indexEndPartForlula]))
                    partsFormula[i] += '*' + argument.ToString();
                else
                    partsFormula[i] += argument.ToString();
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Join(string.Empty, partsFormula));
            string expression = sb.ToString();

            return expression;
        }
    }
}
