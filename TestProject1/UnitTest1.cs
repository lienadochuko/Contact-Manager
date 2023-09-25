namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            MyMath math = new MyMath();
            int input1 = 1; int input2 = 2;
            int expected = 3;

            //act
            int actual = math.Add(input1, input2);

            //assert(compare)
            Assert.Equal(expected, actual);

        }
    }
}