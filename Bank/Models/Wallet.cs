namespace Bank.Models;

// საფულის კლასი
public class Wallet
{
    // უნიკალურობის აღმნიშვნელი
    public Guid Id { get; private set; }

    // მომხმარებლის აღმნიშვნელი
    public Guid AccountId { get; private set; }

    // საფულის სახელი
    public string Name { get; private set; }

    // ანგარიშის ნომერი
    public string AccountNumber { get; private set; } = null!;

    // ბალანსი
    public decimal Balance { get; private set; }

    // შექმნის დრო
    public DateTimeOffset CreatedAt { get; private set; }

    // განახლების დრო
    public DateTimeOffset? UpdatedAt { get; private set; }

    // წაშლის დრო
    public DateTimeOffset? DeletedAt { get; private set; }

    // კავშირი მომხმარებელთან
    public Account Account { get; private set; } = null!;

    // კონსტრუქტორი და ინიციალიზაცია
    public Wallet(Guid id, Guid accountId, string name, decimal balance) 
    {
        Id = id;
        Name = name;
        AccountId = accountId;
        Balance = balance;
        CreatedAt = DateTimeOffset.Now;
        AccountNumber = GenerateAccountNumber();
    }

    // ბალანსის დამატება
    public void In(decimal amount) 
    {
        Balance += amount;
    }


    // ბალანსის გამოტანა
    public void Out(decimal amount)
    {
        // თუ ბალანსი საკმარისია
        if (Balance >= amount)
        {
            // გამოგვაქვს
            Balance -= amount;
        }
        else 
        {
            // თუ არადა შეცდომაა
            throw new Exception($"You do not have {amount} on your balance.");
        }
    }


    // ანგარიშის ნომერის გენერაცია
    private string GenerateAccountNumber() 
    {
        // უნდა იწყებოდეს 32 -ით
        string startWith = "32";

        // ვიძაღებთ რენდომს
        Random generator = new Random();

        // ვიღებთ რენდომ რიცხვს და გადაგვყავს D6 ფორმატში
        string r = generator.Next(0, 999999).ToString("D6");

        // ვაძლევთ საბოლოო სახეს
        string accounNumber = startWith + r;

        return accounNumber;
    }
}
