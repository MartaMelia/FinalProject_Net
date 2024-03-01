using Calculator.Interfaces;

namespace Calculator;

public class Program
{
    private static ICalculatorService _calculatorService;

    static Program()
    {
        // სტატიკური კონსტრუქტორი რომ ინიციალიზება გავუკეთოთ ჩვენს კალკულატორს
        _calculatorService = new Models.Calculator();
    }

    public static void Main(string[] args)
    {
        // კალკულატორის გამშვები მეთოდი
        _calculatorService.StartCalculator();

        // სანამ ჩართულია იმუშავებს
        while (_calculatorService.IsON()) 
        {
            // მთავარი ლოგიკის გამშვები
            _calculatorService.Input();
        }

        // კალკულატორის ისტორიის ამსახველი მეთდი
        _calculatorService.ReadHistory();
    }
}