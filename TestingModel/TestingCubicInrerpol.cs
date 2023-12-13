using Model_MathOperation;

namespace TestingModel
{
    public class TestingCubicInrerpol
    {
        [Fact]
        public void TestLastValueNumberOne()
        {
            CubicInterpolation method = new CubicInterpolation("2x^2+16/x", "4x-16/x^2", 1, 1, 0.01);

            PointDouble actual = method.FindAbsoluteMin().Last(); 

            PointDouble expected = new PointDouble(1.588,15.119);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);
        }
        [Fact]
        public void TestLastValueNumberTwo()
        {
            /*CubicInterpolation method = new CubicInterpolation("2x^2+16/x", "4x-16/x^2", 1, 1, 0.01);

            PointDouble actual = method.FindAbsoluteMin().Last();

            PointDouble expected = new PointDouble(1.588, 15.119);

            Assert.Equal(actual.X, expected.X, 0.0001);
            Assert.Equal(actual.Y, expected.Y, 0.0001);*/
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
