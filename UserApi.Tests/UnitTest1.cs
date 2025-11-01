namespace UserApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SimpleTest_ReturnsTrue()
        {
            bool myTestValue = true;
            Assert.True(myTestValue);
        }

        [Fact]
        public void AdditionTest_ReturnsCorrectSum()
        {
            int a = 5;
            int b = 10;
            int expected = 15;

            int actual = a + b;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MultiplicationTest_ReturnsCorrectSum()
        {
            int a = 5;
            int b = 10;
            int expected = 50;

            int actual = a * b;

            Assert.Equal(expected, actual);
        }
    }
}