using FluentAssertions;
using NSubstitute;
using Project;

namespace NSubstituteProject
{
    public class NSubstituteTests
    {
        [Fact]
        public void NSubstitute_Example_1()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);

            calculator.Add(1, 2).Should().Be(3);
        }

        [Fact]
        public void NSubstitute_Example_2()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(Arg.Any<int>(), Arg.Any<int>());

            var computer = new Computer(calculator);
            computer.CPU("Intel", 3, 6);

            calculator.Received().Add(3, 6);
        }

        [Fact]
        public void NSubstitute_Example_3()
        {
            var counter = 0;

            var calculator = Substitute.For<ICalculator>();
            calculator.When(x => x.Add(Arg.Any<int>(), Arg.Any<int>()))
                .Do(x => counter++);

            var computer = new Computer(calculator);
            computer.CPU("Intel", 3, 6);

            counter.Should().Be(1);

        }

        [Fact]
        public void NSubstitute_Example_4()
        {
            var calculator = Substitute.For<ICalculator>();
            
            calculator.Add(Arg.Any<int>(), Arg.Any<int>())
                .Returns(x => { throw new Exception("This is the message");});
            
            Action act = () => calculator.Add(1, 2);
            act.Should().Throw<Exception>()
                .WithMessage("This is the message");
        }

        [Fact]
        public void NSubstitute_Example_5()
        {
            var calculator = Substitute.For<ICalculator>();
            
            calculator.Add(Arg.Any<int>(), Arg.Any<int>())
                .Returns(10);
            calculator.Multiply(Arg.Any<int>(), Arg.Any<int>())
                .Returns(20);

            calculator.Add(5, 5);
            calculator.Multiply(2, 5);
            
            Received.InOrder(() =>
            {
                calculator.Add(Arg.Any<int>(), Arg.Any<int>());
                calculator.Multiply(Arg.Any<int>(), Arg.Any<int>());
            });
        }
    }
}