namespace MISA.WebFresher042023.Demo.UnitTests
{
    [TestFixture]
    public class CaculatorTests
    {
       

        [TestCase(4,5,9)]
        [TestCase(1,1,2)]
        [TestCase(int.MaxValue,1,(long)int.MaxValue + 1)]
        public void Add_ValidInput_Success(int a, int b, long expectedResult)
        {
        
            // Act
            var actualResult = new Caculator().Add(a, b);
            // Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }


        [TestCase(3, 2, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(int.MaxValue, int.MinValue, (long)2 * int.MaxValue + 1)]
        public void Sub_ValidInput_Success(int a, int b, long expectedResult)
        {

            // Act
            var actualResult = new Caculator().Sub(a, b);
            // Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }


        [TestCase(3, 2, 6)]
        [TestCase(1, 1, 1)]
        [TestCase(int.MaxValue, int.MinValue, (long)int.MinValue * int.MaxValue)]
        public void Mul_ValidInput_Success(int a, int b, long expectedResult)
        {

            // Act
            var actualResult = new Caculator().Mul(a, b);
            // Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [TestCase(3, 2, (double)3/2)]
        [TestCase(4, 5, (double)4/5)]
        [TestCase(5, 3, (double)5 / 3)]

        public void Div_ValidInput_Success(int a, int b, double expectedResult)
        {

            // Act
            var actualResult = new Caculator().Div(a, b);
            // Assert
            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }



        public void Div_InValidInput_Exception()
        {
            // 
            var a = 5;
            var b = 0;
            var expectedMessage = "Không ???c chia cho 0";

            var handler = () => new Caculator().Div(a, b);

            
            // Act
          var exception = Assert.Throws<Exception>(() => handler());
            // Assert
            Assert.That(expectedMessage, Is.EqualTo(exception.Message));
        }
    }
}