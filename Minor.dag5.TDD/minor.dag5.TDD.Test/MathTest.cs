using Xunit;

namespace minor.dag5.TDD.Test
{
    public class MathTest
    {
        [Fact]
        public void Fact0IsExpectInvalidOperation()
        {
            //arange
            var target = new Math();

            //act
            System.Action action = () => target.Fact(0);

            //assert
            Assert.Throws<System.InvalidOperationException>(action);
        }
        [Fact]
        public void Fact1Is1()
        {
            //arange
            var target = new Math();

            //act
            int result = target.Fact(1);

            //assert
            Assert.Equal(1, result);
        }
        [Fact]
        public void Fact2Is2()
        {
            //arange
            var target = new Math();

            //act
            int result = target.Fact(2);

            //assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Fact3Is6()
        {
            //arange
            var target = new Math();

            //act
            int result = target.Fact(3);

            //assert
            Assert.Equal(6, result);
        }

    }
}
