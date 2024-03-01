namespace Bank.Models;

// მომხმარებლის კლასი
public class Account
{
    // უნიკალურობის აღმნიშვნელი
    public Guid Id { get; private set; }
    // სახელი
    public string FirstName { get; private set; } = null!;
    // გვარი
    public string LastName { get; private set; } = null!;
    //პაროლი
    public string Password { get; private set; } = null!;
    // მომხმარებლის ანგარიშები

    private readonly IList<Wallet> _wallets = new List<Wallet>();
    public IList<Wallet> Wallets => _wallets;

    // კონსტრუქტორი
    public Account(Guid id, string firstName, string lastName, string password) 
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
    }
}
