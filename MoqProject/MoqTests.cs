using FluentAssertions;
using Moq;
using Project;

namespace MoqProject
{
    public class MoqTests
    {
        [Fact]
        public void Moq_Example_1()
        {
            var moqCalculator = new Mock<ICalculator>();
            moqCalculator.Setup(calc => calc.Add(1, 2)).Returns(3);

            var calculator = moqCalculator.Object;
            calculator.Add(1, 2).Should().Be(3);
        }

        [Fact]
        public void Moq_Example_2()
        {
            var moqCalculator = new Mock<ICalculator>();
            moqCalculator.Setup(calc => calc.Add(It.IsAny<int>(), It.IsAny<int>()));

            var computer = new Computer(moqCalculator.Object);
            computer.CPU("Intel", 3, 6);

            moqCalculator.Verify(calc => calc.Add(3, 6));
        }

        [Fact]
        public void Moq_Example_3()
        {
            var counter = 0;

            var moqCalculator = new Mock<ICalculator>();
            moqCalculator.Setup(calc => calc.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Callback(() => counter++);

            var computer = new Computer(moqCalculator.Object);
            computer.CPU("Intel", 3, 6);

            counter.Should().Be(1);
        }

        [Fact]
        public void Moq_Example_4()
        {
            var moqCalculator = new Mock<ICalculator>();
            moqCalculator.Setup(calc => calc.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception("This is the message"));
            
            Action act = () => moqCalculator.Object.Add(1, 2);
            act.Should().Throw<Exception>()
                .WithMessage("This is the message");
        }

        [Fact]
        public void Moq_Example_5()
        {
            var moqCalculator = new Mock<ICalculator>(MockBehavior.Strict);
            var mockSequence = new MockSequence();
            
            moqCalculator.InSequence(mockSequence)
                .Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(10);
            moqCalculator.InSequence(mockSequence)
                .Setup(x => x.Multiply(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(20);

            moqCalculator.Object.Add(1, 2);
            moqCalculator.Object.Multiply(3, 3);
            moqCalculator.VerifyAll();
        }
    }
}