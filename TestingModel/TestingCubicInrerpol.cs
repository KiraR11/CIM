﻿using Model_MathOperation;
using Model_MathOperation.MathExpression;
using System.Drawing;
using System.Linq.Expressions;

namespace TestingModel
{
    public class TestingCubicInrerpol
    {
        [Fact]
        public void SolveFromNegativPoint()
        {
            CubicInterpolation method = new CubicInterpolation(new Equation("2x^2+16/x"), new Equation("4x-16/x^2"), -1, 1, 0.01f, 10);

            Action action = () => { ResultCIM actual = method.FindAbsoluteMin().Last(); };

            Assert.Throws<Exception>(action);
        }
        [Fact]
        public void TestLastValueNumberOne()
        {
            CubicInterpolation method = new CubicInterpolation(new Equation("2x^2+16/x"), new Equation("4x-16/x^2"), 1, 1, 0.01f,10);

            PointF actual = method.FindAbsoluteMin().Last().Optimal; 

            PointF expected = new PointF(1.588f,15.119f);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);
        }
        [Fact]
        public void TestLastValueNumberTwo()
        {
            CubicInterpolation method = new CubicInterpolation(new Equation("2x^2+16/x"), new Equation("4x-16/x^2"), 1, 1, 0.000001f, 10);

            PointF actual = method.FindAbsoluteMin().Last().Optimal;

            PointF expected = new PointF(-0.8559f, -17.2275f);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);
        }
        [Fact]
        public void TestLastValueNumberThree()
        {
            /*CubicInterpolation method = new CubicInterpolation("2x^2+16/x", "4x-16/x^2", 1, 1, 0.01);

            PointDouble actual = method.FindAbsoluteMin().Last();

            PointDouble expected = new PointDouble(1.588, 15.119);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);*/
        }
        [Fact]
        public void TestLastValueNumberFour()
        {
            /*CubicInterpolation method = new CubicInterpolation("2x^2+16/x", "4x-16/x^2", 1, 1, 0.01);

            PointDouble actual = method.FindAbsoluteMin().Last();

            PointDouble expected = new PointDouble(1.588, 15.119);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);*/
        }
        [Fact]
        public void TestLastValueNumberFive()
        {
            /*CubicInterpolation method = new CubicInterpolation("2x^2+16/x", "4x-16/x^2", 1, 1, 0.01);

            PointDouble actual = method.FindAbsoluteMin().Last();

            PointDouble expected = new PointDouble(1.588, 15.119);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);*/
        }
        [Fact]
        public void TestCountPointsInSolution()
        {
            /*CubicInterpolation method = new CubicInterpolation("2x^2+16/x", "4x-16/x^2", 1, 1, 0.01);

            PointDouble actual = method.FindAbsoluteMin().Last();

            PointDouble expected = new PointDouble(1.588, 15.119);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);*/
        }
    }
}
