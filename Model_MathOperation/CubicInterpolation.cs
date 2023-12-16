using Math_Model.MathExpression;
using Model_MathOperation;
using Model_MathOperation.MathExpression;
using System.Drawing;

namespace Math_Model
{
    public class CubicInterpolation
    {
        public CubicInterpolation(Equation function, Equation derivativeFunction,double startPoint, double step, double accuracy)
        {
            Function = function;
            DerivativeFunction = derivativeFunction;
            StartPoint = startPoint;
            Step = step;
            Accuracy = accuracy;
        }
        public Equation Function { get; }
        public Equation DerivativeFunction { get; }
        private double StartPoint { get; }
        private double Step { get; }
        private double Accuracy { get; }
        public List<PointDouble> FindAbsoluteMin()
        {
            List<PointDouble> result = new List<PointDouble>{new PointDouble(StartPoint, SolveValueFun(StartPoint))};
            IntervalWithExtreme intervalWithExtreme = SolveFirstInterval();

            double optimalArgument = FindOptimalArgument(intervalWithExtreme); // шаг 5-6
            result.Add(new PointDouble(optimalArgument, SolveValueFun(optimalArgument)));
            result.AddRange(LoopingSearchOptimum(intervalWithExtreme,optimalArgument));

            return result;
        }

        private IntervalWithExtreme SolveFirstInterval()
        {
            int signIteretion = FindSignIteretion(StartPoint); // шаг 3
            int powPoint = FindPowArguments(signIteretion);

            double firstArgument = FindArgumentInBinaryRow(powPoint - 1, signIteretion); // шаг 4
            double secondArgument = FindArgumentInBinaryRow(powPoint, signIteretion);

            IntervalWithExtreme intervalWithExtreme = new IntervalWithExtreme(firstArgument, secondArgument);

            return intervalWithExtreme;
        }
        private List<PointDouble> LoopingSearchOptimum(IntervalWithExtreme interval, double optimalArgument)
        {
            List<PointDouble> result = new List<PointDouble>();
            IntervalWithExtreme newInterval = new IntervalWithExtreme(interval.StartPoint, interval.EndPoint);

            while (Math.Abs(SolveValueDerivativeFun(optimalArgument)) > Accuracy)
            {
                if (SolveValueDerivativeFun(optimalArgument) * SolveValueDerivativeFun(newInterval.StartPoint) < 0)
                {
                    double secondArgument = newInterval.StartPoint;
                    double firstArgument = optimalArgument;
                    newInterval = new IntervalWithExtreme(firstArgument, secondArgument);
                }
                else if (SolveValueDerivativeFun(optimalArgument) * SolveValueDerivativeFun(newInterval.EndPoint) < 0)
                {
                    double firstArgument = optimalArgument;
                    newInterval = new IntervalWithExtreme(firstArgument, newInterval.EndPoint);
                }
                else throw new Exception("хуйня");

                optimalArgument = FindOptimalArgument(newInterval);
                result.Add(new PointDouble(optimalArgument, SolveValueFun(optimalArgument)));
            }

            return result;
        }

        private double FindOptimalArgument(IntervalWithExtreme interval)
        {
            double valueFunInFirstArgument = SolveValueFun(interval.StartPoint);
            double valueFunInSecondArgument = SolveValueFun(interval.EndPoint);

            double valueDerFunInFirstArgument = SolveValueDerivativeFun(interval.StartPoint);
            double valueDerFunInSecondArgument = SolveValueDerivativeFun(interval.EndPoint);

            double z = 3 * (valueFunInFirstArgument - valueFunInSecondArgument) / (interval.EndPoint - interval.StartPoint) + valueDerFunInFirstArgument + valueDerFunInSecondArgument;

            double w = SolveWArg(interval.StartPoint,interval.EndPoint,valueDerFunInFirstArgument,valueDerFunInSecondArgument,z);

            double u = (valueDerFunInSecondArgument + w - z) / (valueDerFunInSecondArgument - valueDerFunInFirstArgument + 2 * w);

            double optimalArgument = SolveOptimalArgument(interval,u);

            return optimalArgument;
        }
        private double SolveOptimalArgument(IntervalWithExtreme interval,double u)
        {
            double optimalArgument;

            if (u < 0)
                optimalArgument = interval.EndPoint;
            else if (u > 1)
                optimalArgument = interval.StartPoint;
            else optimalArgument = interval.EndPoint - u * (interval.EndPoint - interval.StartPoint);

            optimalArgument = FulfillDescendingCondition(optimalArgument, interval.StartPoint); // шаг 6

            return optimalArgument;
        }
        private double SolveWArg(double firstArgument, double secondArgument,double valueDerFunInFirstArgument, double valueDerFunInSecondArgument, double z)
        {
            double w;
            int signOperationForSolveW;

            if (firstArgument < secondArgument) 
                signOperationForSolveW = 1;
            else if (firstArgument > secondArgument) 
                signOperationForSolveW = -1;
            else 
                throw new Exception("точка вместо интервала, очень интерестно");
            if (z * z >= valueDerFunInFirstArgument * valueDerFunInSecondArgument)
                w = signOperationForSolveW * Math.Pow((z * z - valueDerFunInFirstArgument * valueDerFunInSecondArgument), 0.5);
            else
                throw new Exception("корень из отрицательного числа");
            return w;
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
