using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_Model.MathExpression
{
    internal static class CheckingCorrectMathExpression
    {
        public static bool IsCorrectExpression(string expression)
        {
            if (CheckByCorrectCharsExpression(expression) && СheckСlosenessBrackets(expression) && CheckByAbsenceDoubleSign(expression))
                return true;
            else
                return false;
        }
        public static bool IsCorrectFormula(string formula)
        {
            if (CheckByCorrectCharsFormula(formula) && СheckСlosenessBrackets(formula) && CheckByAbsenceDoubleSign(formula))
                return true;
            else
                return false;
        }
        public static bool IsOperation(char operation)
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
        private static bool IsBrackets(char operation)
        {
            switch (operation)
            {
                case '(': return true;
                case ')': return true;
                default: return false;
            }
        }
        private static bool IsSpecialSymbol(char operation)
        {
            switch (operation)
            {
                case 'x': return true;
                case ' ': return true;
                default: return false;
            }
        }
        private static bool IsSignSeparation(char operation)
        {
            switch (operation)
            {
                case ',': return true;
                default: return false;
            }
        }
        private static bool IsCorrectSymbol(char symbol)
        {
            return char.IsDigit(symbol) || IsSignSeparation(symbol) || IsBrackets(symbol) || IsOperation(symbol);
        }

        private static bool CheckByCorrectCharsExpression(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (!IsCorrectSymbol(expression[i]))
                    throw new Exception("В выражении присутствуют недопустиный символ");
            }
            return true;
        }
        private static bool CheckByCorrectCharsFormula(string expression)
        {
            for (int i = 0; i < expression.Length; i++)
            {
                if (!IsCorrectSymbol(expression[i]) && !IsSpecialSymbol(expression[i]))
                    throw new Exception("В выражении присутствуют недопустиный символ");
            }
            return true;
        }

        private static bool CheckByAbsenceDoubleSign(string expression)
        {
            char lastSymbol = expression[0];
            for (int i = 1; i < expression.Length - 1; i++)
            {
                if (IsOperation(lastSymbol) && IsOperation(expression[i]) && expression[i] != '√')
                    throw new Exception("В выражении присутствуют два символа операции подрят");
                else if (expression[i] != ' ')
                    lastSymbol = expression[i];
            }
            return true;
        }

        private static bool СheckСlosenessBrackets(string expression)
        {
            int countOpeningBrackets = 0;
            int countClosingBrackest = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                    countOpeningBrackets++;
                else if (expression[i] == ')')
                    countClosingBrackest++;
            }
            if (countOpeningBrackets == countClosingBrackest)
                return true;
            else throw new Exception("В выражении присутствуют не закрытые скобки");
        }
    }
}
