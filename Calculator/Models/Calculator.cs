using Calculator.Interfaces;

namespace Calculator.Models;

// კალკულატორის კლასი რომელის შვილია CalculatorRoot კლასის 
public class Calculator : CalculatorRoot, ICalculatorService
{
    // კალკულატორის ჩამრთველი
    private bool _isON = false;

    // კონსტრუქტორი
    public Calculator() : base() 
    {
    }

    // ისტორიის წამკითხავი მეთოდი რომელსაც ისტორიის ლისტი
    // გადაყავს ჩვენთვის მისაღებ სტრინგში
    public void ReadHistory() 
    {
        // აქ ვიყენებთ სტრინგის ინტერპოლაციას
        string result = string.Join("\n", History
            .Select(x => $"Operation: {x.Name} , " +
            $"First Number: {x.FirstNumber} , " +
            $"Second Number: {x.SecondNumber} " +
            $", Result: {x.Result} ," +
            $" CreatedAt: {x.CreatedAt.ToString("dd.MM.yyyy")}," +
            $" IsValid: {x.IsValid}"));

        Console.WriteLine();
        Console.WriteLine("Calculator History");
        Console.WriteLine(result);
    }

    // კალკულატორის სტატუსის შესამოწმებლად
    public bool IsON() => _isON;

    // მთავარი ლოგიკის მეთოდი
    public void Input()
    {
        // პირველი რიცხვის შეყვანა
        Console.WriteLine("Enter the expression:");

        Console.Write("First number: ");
        string firstNumber = Console.ReadLine()?? string.Empty;

        // თუ Stop დაწერა მაშინ ვთიშავთ კალკულატორს
        if (firstNumber!.ToLower() == "stop")
        {
            StopCalculator();
            return;
        }

        // ვითხოვთ ოპერაციას
        Console.Write("Operation (+, -, *, /): ");
        string operation = Console.ReadLine() ?? string.Empty;

        // თუ Stop დაწერა მაშინ ვთიშავთ კალკულატორს
        if (operation!.ToLower() == "stop")
        {
            StopCalculator();
            return;
        }

        // ვითხოვთ მეორე რიცხვს
        Console.Write("Second number: ");
        string secondNumber = Console.ReadLine() ?? string.Empty;

        // თუ Stop დაწერა მაშინ ვთიშავთ კალკულატორს
        if (secondNumber!.ToLower() == "stop")
        {
            StopCalculator();
            return;
        }

        // მიღებული სტრინგების ვალიდაცია
        var (isValid, errorMessage) =  Validate(firstNumber, secondNumber, operation);

        // თუ არაა ვალიდური მაშინ თავიდან ვთხოვთ მომხმარებელს რომ შეიყვანოს მონაცემები
        if (!isValid)
        {
            Console.WriteLine($"Invalid input for {errorMessage}. Please try again.\n");
            Input();
        }
        else
        {
            // თუ ვალიდურია
            Console.WriteLine("Input is valid. Performing calculation...\n");

            // სტრინგები გადაგვყავს კომპლლექსურ რიცხვში
            var a = ComplexParse(firstNumber!);
            var b = ComplexParse(secondNumber!);

            // სწორი ოპერატორისთვის ვარჩევთ კონკრეტულ მეთოდს
            var method = GetOperationMethod(operation!);

            // მეთოდის და რიცხვების გამოყენებით ვიღებთ შედეგს
            var result = method(a!.Value, b!.Value);

            // კონკრეტულ მოქმედებას ვამატებთ ისტორიაში
            AddHistory(new OperationModel(operation, a!.Value, b!.Value, result, true));

            Console.WriteLine($"Result of the calculation: {result.Real}\n");
        }
    }

    // კალკულატორის ჩამრთველი მეთოდი
    public void StartCalculator() 
    {
        _isON = !_isON;
        Console.WriteLine("Hello Calculator started. \nTo stop the calculator write (stop) command");
    }

    // კალკულატორის გამომრთავი მეთოდი
    public void StopCalculator() 
    {
        _isON = !_isON;
        Console.WriteLine("Calculator stoped.");
    }

    // ვალიდაციის მეთდი
    private (bool, string) Validate(string? firstNumber, string? secondNumber, string? operation)
    {
        // თუ ცარიელია არა ვალიდურია
        if (string.IsNullOrEmpty(firstNumber)) return (false, nameof(firstNumber));
        if (string.IsNullOrEmpty(secondNumber)) return (false, nameof(secondNumber));
        if (string.IsNullOrEmpty(operation)) return (false, nameof(operation));

        // ყველა შესაძლო ოპერაცია 
        var operations = new string[] { "+", "-", "*", "/" };

        // ვცდილობთ გადაყვანას
        var a = ComplexParse(firstNumber);

        // თუ არ გამოვიდა არა ვალიდურია
        if (a is null) return (false, nameof(firstNumber));

        // ვცდილობთ გადაყვანას
        var b = ComplexParse(secondNumber);

        // თუ არ გამოვიდა არა ვალიდურია
        if (b is null) return (false, nameof(secondNumber));

        // თუ არა ვალიდური ოპერატორია ვაბრუნებთ ერორს
        if (!operations.Contains(operation.Trim())) return (false, nameof(operation));

        // თუ ვალიდურია 
        return (true, string.Empty);
    }
}
