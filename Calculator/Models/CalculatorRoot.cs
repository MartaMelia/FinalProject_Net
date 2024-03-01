using Calculator.Interfaces;
using System.Numerics;

namespace Calculator.Models;

// კალკულატორის მშობელი კლასი სადაც გაწერილი არის 
// მთავარი სერვისები რაც კალკულატორს უნდა გააჩნდეს
public class CalculatorRoot : ICalculatorRoot
{
    // ისტორიის აღმრიცხველი ლისტი
    private IList<OperationModel> _history = new List<OperationModel>();

    // ისტორიის წამკითხველი field
    public IList<OperationModel> History => _history;

    // კონსტრუქტორი
    public CalculatorRoot()
    {
    }

    // ისტორიის დამატება
    public void AddHistory(OperationModel model) => _history.Add(model);

    // დამატების ოპერაცია
    public Complex Add(Complex a, Complex b) => Complex.Add(a, b);

    // გაყოფის ოპერაცია
    public Complex Divide(Complex a, Complex b) => Complex.Divide(a, b);

    // გამრავლების ოპერაცია
    public Complex Multiply(Complex a, Complex b) => Complex.Multiply(a, b);

    // გამოკლების ოპერაცია
    public Complex Subtract(Complex a, Complex b) => Complex.Subtract(a, b);

    // ოპერაციის მეთოდის გამოძახების მეთოდი რომელი აბრუნებს ზედა 4 მეთოდიდან ერთს
    public Func<Complex, Complex, Complex> GetOperationMethod(string operation)
    {
        // ვქმნით ბიბლიოთეკას
        Dictionary<string, Func<Complex, Complex, Complex>> operations = new Dictionary<string, Func<Complex, Complex, Complex>>
            {
                {"+", Add},
                {"-", Subtract},
                {"*", Multiply},
                {"/", Divide}
            };

        // თუ მომხმარებელმა სწორად მოითხოვა ოპერაცია მაშინ ვაბრუნებთ მეთოდს
        if (operations.TryGetValue(operation.Trim(), out var method))
        {
            return method;
        }

        // თუ არასწორად ვისვრით ერორს
        throw new ArgumentException("Invalid operation");
    }

    // სტრინგის კომპლექსურ რიცხვში გადაყვანა
    public Complex? ComplexParse(string text)
    {
        try
        {
            // თუ ცარიელია მაშინ შეცდომაა
            if (string.IsNullOrEmpty(text)) return null;

            if (text.Any(x => char.IsLetter(x))) return null;

            // რადგანაც კომპლექსური რიცხვი a + bi არის ამიტომ გამოვყობთ 2 დეციმალს
            var numbers = text.Trim().Split(" ");

            // ნამდვილი რიცხვი
            double.TryParse(numbers[0], out double real);
            
            if (numbers.Length > 1 && numbers.Length < 3) 
            {
                // წარმოსახვითი რიცხვი
                double.TryParse(numbers[1], out double imagine);

                // გადავკასტოთ კომპლექსურ ტიპში
                return new Complex(real, imagine);
            }

            // გადავკასტოთ კომპლექსურ ტიპში
            return new Complex(real, 0);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

