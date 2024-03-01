using Bank.Interfaces;
using Bank.Models;
using Bank.Services;

namespace Bank.Features;

// მომხმარებლის რეგისტრაციის კლასი
public class AccountRegistration
{
    // ვიძახებთ მომხმარებლის სერვისს
    private readonly IAccountRepository _accountRepository;
    // მომხმარებლის ექაუნთი
    private readonly Account _account;

    // კონსტრუქტორით ხდება ინიციალიზაცია
    public AccountRegistration(Account account) 
    {
        _account = account;
        _accountRepository = new AccountRepository();
    }

    public async Task Execute() 
    {
        // ვარეგისტრირებთ მომხმარებელს
        await _accountRepository.AddItemAsync(_account);
    }
}
