using System.Numerics;

namespace Calculator.Models;

// ამ მოდელს ვიყენებთ ისტორიის შესანახად
public class OperationModel
{
    // კლასის უნიკალურობის აღმნიშვნელი
    public Guid Id { get; private set; }

    // ოპერაციის სახელი
    public string Name { get; private set; } = null!;

    // მომხმარებლის მიერ შეყვანილი პირველი რიცხი
    public Complex FirstNumber { get; private set; }

    // მომხმარებლის მიერ შეყვანილი მეორე რიცხვი
    public Complex SecondNumber { get; private set; }

    // მიღებული შედეგი
    public Complex Result { get; private set; }

    // როდის მოხდა ოპერაცია
    public DateTimeOffset CreatedAt { get; private set; }

    // ვალიდური იყო თუ არა ოპერაცია
    public bool IsValid { get; private set; }

    // ვქმნით კონსტრუქტორს რომ ინიციალიზაცია გავუკეთოთ მოდელს
    public OperationModel(
        string name,
        Complex firstNumber,
        Complex secondNumber,
        Complex result,
        bool isValid) 
    {
        Id = Guid.NewGuid();
        Name = name;
        FirstNumber = firstNumber;
        SecondNumber = secondNumber;
        Result = result;
        CreatedAt = DateTimeOffset.Now;
        IsValid = isValid;
    }
}
