using Bank.Interfaces;
using Bank.Services;

namespace Bank.Features;

// ავტორიზაციის კლასი
public class AccountAuthorization
{
    // ვიძახებთ მომხმარებლების სერვისს
    private readonly IAccountRepository _accountRepository;
    // მომხმარებლის სახელს
    private readonly string _firstName;

    // მომხმარებლის პაროლს
    private readonly string _password;

    // კონსტრუქტორით ხდება ინიციალიზაცია
    public AccountAuthorization(string firstName, string password) 
    {
        _firstName = firstName;
        _password = password;
        _accountRepository = new AccountRepository();
    }

    // გამშვები მეთოდი
    public async Task<Guid?> Execute() 
    {
        // ვიპოვოთ მომხმარებლის ექაუნთი სახელით
        var account = await _accountRepository.GetAccountByFirstName(_firstName);

        // თუ არ არსებობს ვაბრუნებთ შეტყობინებას
        if (account is null) 
        {
            Console.WriteLine("Username or Password is incorrect");
            return null;
        }

        // თუ არსებობს და პაროლი სწორია ვუშვებთ სისტემაში
        if (account.Password == _password.Trim()) 
        {
            Console.WriteLine("Authorization ended successfully.");
            return account.Id;
        }

        // თუ არ არის სწორი ვაბრუნებთ შეტყობინებას
        Console.WriteLine("Username or Password is incorrect");

        return null;
    }
}
