using Math_Model.MathExpression;
using Model_MathOperation;
using System.Drawing;

namespace Math_Model
{
    public class CubicInterpolation
    {
        public CubicInterpolation(string function, string derivativeFunction,double startPoint, double step, double accuracy)
        {
            Function = new Equation(function);
            DerivativeFunction = new Equation(derivativeFunction);
            StartPoint = startPoint;
            Step = step;
            Accuracy = accuracy;
        }
        public Equation Function { get; }
        public Equation DerivativeFunction { get; }
        private double StartPoint { get; }
        private double Step { get; }
        private double Accuracy { get; }
        public int Test()
        {
            int signIteretion = FindSignIteretion(StartPoint);
            int powPoint = FindPowArguments(signIteretion);
            double firstArguments = FindArgumentInBinaryRow(powPoint - 1, signIteretion);
            double secondArguments = FindArgumentInBinaryRow(powPoint, signIteretion);


            return powPoint;
        }

        public List<PointDouble> FindAbsoluteMin()
        {
            List<PointDouble> result = new List<PointDouble>{new PointDouble(StartPoint, SolveValueFun(StartPoint))};

            int signIteretion = FindSignIteretion(StartPoint); // шаг 3
            int powPoint = FindPowArguments(signIteretion);

            double firstArgument = FindArgumentInBinaryRow(powPoint - 1, signIteretion); // шаг 4
            double secondArgument = FindArgumentInBinaryRow(powPoint, signIteretion);
            double optimalArgument = FindOptimalArgument(firstArgument, secondArgument); // шаг 5-6
            result.Add(new PointDouble(optimalArgument, SolveValueFun(optimalArgument)));
            result.AddRange(LoopingSearchOptimum(firstArgument,secondArgument,optimalArgument));

            return result;
        }
        private List<PointDouble> LoopingSearchOptimum(double firstArgument, double secondArgument, double optimalArgument)
        {
            List<PointDouble> result = new List<PointDouble>();

            while (Math.Abs(SolveValueDerivativeFun(optimalArgument)) > Accuracy)
            {
                if (SolveValueDerivativeFun(optimalArgument) * SolveValueDerivativeFun(firstArgument) < 0)
                {
                    secondArgument = firstArgument;
                    firstArgument = optimalArgument;
                }
                else if (SolveValueDerivativeFun(optimalArgument) * SolveValueDerivativeFun(secondArgument) < 0)
                {
                    firstArgument = optimalArgument;
                }
                else throw new Exception("хуйня");

                optimalArgument = FindOptimalArgument(firstArgument, secondArgument);
                result.Add(new PointDouble(optimalArgument, SolveValueFun(optimalArgument)));
            }

            return result;
        }

        private double FindOptimalArgument(double firstArgument, double secondArgument)//разбить на методы
        {
            double valueFunInFirstArgument = SolveValueFun(firstArgument);
            double valueFunInSecondArgument = SolveValueFun(secondArgument);

            double valueDerFunInFirstArgument = SolveValueDerivativeFun(firstArgument);
            double valueDerFunInSecondArgument = SolveValueDerivativeFun(secondArgument);

            double z = 3 * (valueFunInFirstArgument - valueFunInSecondArgument) / (secondArgument - firstArgument) + valueDerFunInFirstArgument + valueDerFunInSecondArgument;

            int signOperationForSolveW;

            if (firstArgument < secondArgument) signOperationForSolveW = 1;
            else if (firstArgument > secondArgument) signOperationForSolveW = -1;
            else throw new Exception("хуйня");

            double w;
            if (z * z >= valueDerFunInFirstArgument * valueDerFunInSecondArgument)
                w = signOperationForSolveW * Math.Pow((z * z - valueDerFunInFirstArgument * valueDerFunInSecondArgument), 0.5);
            else
                throw new Exception("корень из отрицательного числа");

            double u = (valueDerFunInSecondArgument + w - z) / (valueDerFunInSecondArgument - valueDerFunInFirstArgument + 2 * w);

            double optimalArgument;

            if (u < 0)
                optimalArgument = secondArgument;
            else if (u > 1)
                optimalArgument = firstArgument;
            else optimalArgument = secondArgument - u * (secondArgument - firstArgument);

            optimalArgument = FulfillDescendingCondition(optimalArgument, firstArgument); // шаг 6

            return optimalArgument;
        }

        private double FulfillDescendingCondition(double optimalArgument, double firstArgument)
        {
            double valueFunInFirstArgument = SolveValueFun(firstArgument);
            while (SolveValueFun(optimalArgument) >= valueFunInFirstArgument)
                optimalArgument -= 0.5 * (optimalArgument - firstArgument);
            return optimalArgument;
        }
        private int FindPowArguments(int signIteretion)
        {
            int pow = 0;
            double popPoint = StartPoint;
            double previousPoint;
            double nextPoint;
            do
            {
                pow++;
                previousPoint = popPoint;
                nextPoint = FindArgumentInBinaryRow(pow, signIteretion);
                popPoint = nextPoint;

            } while (SolveValueDerivativeFun(previousPoint) * SolveValueDerivativeFun(nextPoint) > 0);
                return pow;
        }
        private double FindArgumentInBinaryRow(int pow,int signIteretion)
        {
            double pointNumberM = StartPoint;
            for (int i = 1; i <= pow; i++)
            {
                pointNumberM += Math.Pow(2, i-1) * signIteretion * Step;
            }
            return pointNumberM;
        }

        private int FindSignIteretion(double startPoint)
        {
            int signIteretion;
            double valueDerivativeFunInStartPoint = SolveValueDerivativeFun(startPoint);
            if (valueDerivativeFunInStartPoint > 0)
            {
                signIteretion = -1;
            }
            else if (valueDerivativeFunInStartPoint < 0)
            {
                signIteretion = 1;
            }
            else
            {
                throw new ArgumentException("Производная в начальной точке равна нулю");
            }
            return signIteretion;
        }
        private double SolveValueFun(double point)
        {
            return Function.Solve(point);
        }
        private double SolveValueDerivativeFun(double point)
        {
            return DerivativeFunction.Solve(point);
        }

    }
}
