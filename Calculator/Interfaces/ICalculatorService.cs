namespace Calculator.Interfaces;

// კალკულატორის სერვისების ინტერფეისი
public interface ICalculatorService : ICalculatorRoot
{
    // კალკულატორის გათიშვა
    void StopCalculator();
    // კალკულატორის ჩართვა
    void StartCalculator();
    // კალკულატორის მთავარი ლოგიკა
    void Input();
    // კალკულატორის სტატუსის შემოწმება
    bool IsON();
    // ისტორიის წაკითხვა
    void ReadHistory();
}

