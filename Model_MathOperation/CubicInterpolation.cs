using Math_Model.MathExpression;
using Model_MathOperation;
using Model_MathOperation.MathExpression;
using System.Drawing;

namespace Math_Model
{
    public class CubicInterpolation
    {
        public CubicInterpolation(Equation function, Equation derivativeFunction, float startPoint, float step, float accuracy,int countIterution)
        {
            if (step <= 0)
                throw new Exception("шаг должен быть больше нуля");
            if (accuracy <= 0)
                throw new Exception("точность должена быть больше нуля");
            if (countIterution <= 1 || countIterution > 50)
                throw new Exception("количество итераций должно быть от 2 до 50");
            else
            {
                Function = function;
                DerivativeFunction = derivativeFunction;
                StartPoint = startPoint;
                Step = step;
                Accuracy = accuracy;
                _countIterution = countIterution;
            }
        }
        public Equation Function { get; }
        public Equation DerivativeFunction { get; }
        private float StartPoint { get; }
        private float Step { get; }
        private float Accuracy { get; }
        private int _countIterution;
        public List<ResultCIM> FindAbsoluteMin()
        {
            List<ResultCIM> result = new List<ResultCIM>() {new ResultCIM(new PointF(StartPoint,SolveValueFun(StartPoint))) };
            IntervalWithExtreme intervalWithExtreme = SolveFirstInterval();

            List<ResultCIM> resultCIMs = LoopingSearchOptimum(intervalWithExtreme);
            result.AddRange(resultCIMs);

            return result;
        }
        private List<ResultCIM> LoopingSearchOptimum(IntervalWithExtreme interval)
        {
            List<ResultCIM> result = new List<ResultCIM>();
            ResultCIM firstResult = FindOptimalArgument(interval); // шаг 5-6
            result.Add(firstResult);

            float optimalArgument = firstResult.Optimal.X;

            float ValueDerFun = SolveValueDerivativeFun(optimalArgument);
            while (Math.Abs(ValueDerFun) > Accuracy && result.Count < _countIterution)
            {
                if (ValueDerFun * SolveValueDerivativeFun(interval.StartPoint) < 0)
                {
                    float secondArgument = interval.StartPoint;
                    float firstArgument = optimalArgument;//localResult.Optimal.X
                    interval = new IntervalWithExtreme(firstArgument, secondArgument);
                }
                else if (ValueDerFun * SolveValueDerivativeFun(interval.EndPoint) < 0)
                {
                    float firstArgument = optimalArgument;
                    interval = new IntervalWithExtreme(firstArgument, interval.EndPoint);
                }
                else return result;
                ResultCIM localResult = FindOptimalArgument(interval);
                optimalArgument = localResult.Optimal.X;    
                result.Add(localResult);
                ValueDerFun = SolveValueDerivativeFun(optimalArgument);
            }

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


        private ResultCIM FindOptimalArgument(IntervalWithExtreme interval)
        {
            float valueFunInFirstArgument = SolveValueFun(interval.StartPoint);
            float valueFunInSecondArgument = SolveValueFun(interval.EndPoint);

            float valueDerFunInFirstArgument = SolveValueDerivativeFun(interval.StartPoint);
            float valueDerFunInSecondArgument = SolveValueDerivativeFun(interval.EndPoint);

            CubicPolinom cubicPolinom = new CubicPolinom(interval, valueFunInFirstArgument, valueFunInSecondArgument, valueDerFunInFirstArgument, valueDerFunInSecondArgument);

            float optimalArgument = cubicPolinom.FindMin();

            ResultCIM result = new ResultCIM
                (
                new PointF(interval.StartPoint, valueFunInFirstArgument),
                new PointF(interval.EndPoint, valueFunInSecondArgument),
                new PointF(optimalArgument, SolveValueFun(optimalArgument)),
                cubicPolinom.SolvePoint(20)
                );


            return result;
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
