using Math_Model.MathExpression;
using Model_MathOperation;
using Model_MathOperation.MathExpression;
using System.Drawing;

namespace Math_Model
{
    public class CubicInterpolation
    {
        public CubicInterpolation(Equation function, Equation derivativeFunction, float startPoint, float step, float accuracy)
        {
            if (step <= 0)
                throw new Exception("шаг должен быть больше нуля");
            if (accuracy <= 0)
                throw new Exception("точность должена быть больше нуля");
            else
            {
                Function = function;
                DerivativeFunction = derivativeFunction;
                StartPoint = startPoint;
                Step = step;
                Accuracy = accuracy;
            }
        }
        public Equation Function { get; }
        public Equation DerivativeFunction { get; }
        private float StartPoint { get; }
        private float Step { get; }
        private float Accuracy { get; }
        public List<PointF> FindAbsoluteMin()
        {
            List<PointF> result = new List<PointF>{new PointF(StartPoint, SolveValueFun(StartPoint))};
            IntervalWithExtreme intervalWithExtreme = SolveFirstInterval();

            float optimalArgument = FindOptimalArgument(intervalWithExtreme); // шаг 5-6
            result.Add(new PointF(optimalArgument, SolveValueFun(optimalArgument)));
            result.AddRange(LoopingSearchOptimum(intervalWithExtreme,optimalArgument));

            return result;
        }

        private IntervalWithExtreme SolveFirstInterval()
        {
            int signIteretion = FindSignIteretion(StartPoint); // шаг 3
            int powPoint = FindPowArguments(signIteretion);

            float firstArgument = FindArgumentInBinaryRow(powPoint - 1, signIteretion); // шаг 4
            float secondArgument = FindArgumentInBinaryRow(powPoint, signIteretion);

            IntervalWithExtreme intervalWithExtreme = new IntervalWithExtreme(firstArgument, secondArgument);

            return intervalWithExtreme;
        }
        private List<PointF> LoopingSearchOptimum(IntervalWithExtreme interval, float optimalArgument)
        {
            List<PointF> result = new List<PointF>();
            IntervalWithExtreme newInterval = new IntervalWithExtreme(interval.StartPoint, interval.EndPoint);

            while (Math.Abs(SolveValueDerivativeFun(optimalArgument)) > Accuracy)
            {
                if (SolveValueDerivativeFun(optimalArgument) * SolveValueDerivativeFun(newInterval.StartPoint) < 0)
                {
                    float secondArgument = newInterval.StartPoint;
                    float firstArgument = optimalArgument;
                    newInterval = new IntervalWithExtreme(firstArgument, secondArgument);
                }
                else if (SolveValueDerivativeFun(optimalArgument) * SolveValueDerivativeFun(newInterval.EndPoint) < 0)
                {
                    float firstArgument = optimalArgument;
                    newInterval = new IntervalWithExtreme(firstArgument, newInterval.EndPoint);
                }
                else throw new Exception("хуйня");

                optimalArgument = FindOptimalArgument(newInterval);
                result.Add(new PointF(optimalArgument, SolveValueFun(optimalArgument)));
            }

            return result;
        }

        private float FindOptimalArgument(IntervalWithExtreme interval)
        {
            float valueFunInFirstArgument = SolveValueFun(interval.StartPoint);
            float valueFunInSecondArgument = SolveValueFun(interval.EndPoint);

            float valueDerFunInFirstArgument = SolveValueDerivativeFun(interval.StartPoint);
            float valueDerFunInSecondArgument = SolveValueDerivativeFun(interval.EndPoint);

            float z = 3 * (valueFunInFirstArgument - valueFunInSecondArgument) / (interval.EndPoint - interval.StartPoint) + valueDerFunInFirstArgument + valueDerFunInSecondArgument;

            float w = SolveWArg(interval.StartPoint,interval.EndPoint,valueDerFunInFirstArgument,valueDerFunInSecondArgument,z);

            float u = (valueDerFunInSecondArgument + w - z) / (valueDerFunInSecondArgument - valueDerFunInFirstArgument + 2 * w);

            float optimalArgument = SolveOptimalArgument(interval,u);

            return optimalArgument;
        }
        private float SolveOptimalArgument(IntervalWithExtreme interval, float u)
        {
            float optimalArgument;

            if (u < 0)
                optimalArgument = interval.EndPoint;
            else if (u > 1)
                optimalArgument = interval.StartPoint;
            else optimalArgument = interval.EndPoint - u * (interval.EndPoint - interval.StartPoint);

            optimalArgument = FulfillDescendingCondition(optimalArgument, interval.StartPoint); // шаг 6

            return optimalArgument;
        }
        private float SolveWArg(float firstArgument, float secondArgument, float valueDerFunInFirstArgument, float valueDerFunInSecondArgument, float z)
        {
            float w;
            int signOperationForSolveW;

            if (firstArgument < secondArgument) 
                signOperationForSolveW = 1;
            else if (firstArgument > secondArgument) 
                signOperationForSolveW = -1;
            else 
                throw new Exception("точка вместо интервала, очень интерестно");
            if (z * z >= valueDerFunInFirstArgument * valueDerFunInSecondArgument)
                w = signOperationForSolveW * (float)Math.Pow((z * z - valueDerFunInFirstArgument * valueDerFunInSecondArgument), 0.5);
            else
                throw new Exception("корень из отрицательного числа");
            return w;
        }

        private float FulfillDescendingCondition(float optimalArgument, float firstArgument)
        {
            double valueFunInFirstArgument = SolveValueFun(firstArgument);
            while (SolveValueFun(optimalArgument) >= valueFunInFirstArgument)
                optimalArgument -= (float)0.5 * (optimalArgument - firstArgument);
            return optimalArgument;
        }
        private int FindPowArguments(int signIteretion)
        {
            int pow = 0;
            float popPoint = StartPoint;
            float previousPoint;
            float nextPoint;
            do
            {
                pow++;
                previousPoint = popPoint;
                nextPoint = FindArgumentInBinaryRow(pow, signIteretion);
                popPoint = nextPoint;

            } while (SolveValueDerivativeFun(previousPoint) * SolveValueDerivativeFun(nextPoint) > 0);
                return pow;
        }
        private float FindArgumentInBinaryRow(int pow,int signIteretion)
        {
            float pointNumberM = StartPoint;
            for (int i = 1; i <= pow; i++)
            {
                pointNumberM += (float)Math.Pow(2, i-1) * signIteretion * Step;
            }
            return pointNumberM;
        }

        private int FindSignIteretion(float startPoint)
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
        private float SolveValueFun(float point)
        {
            return Function.Solve(point);
        }
        private float SolveValueDerivativeFun(float point)
        {
            return DerivativeFunction.Solve(point);
        }

    }
}
