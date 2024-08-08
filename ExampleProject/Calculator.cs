namespace Project
{
    public class Calculator : ICalculator
    {
        public int Add(int value1, int value2) =>
            value1 + value2;

        public int Multiply(int value1, int value2) =>
            value1 * value2;
    }
}
