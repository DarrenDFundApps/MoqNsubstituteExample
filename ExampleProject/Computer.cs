namespace Project;

public class Computer
{
    private readonly ICalculator _calculator;

    public Computer(ICalculator calculator) {
        _calculator = calculator;
    }

    public double CPU(string name, int speed, int  cores)
    {
        if (string.IsNullOrWhiteSpace(name)) {
            throw new ArgumentException("name can not be empty or null.");
        }
        
        _ = _calculator.Add(speed, cores);
        return 4.55;
    }
}
