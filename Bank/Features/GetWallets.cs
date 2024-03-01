using Bank.Interfaces;
using Bank.Repositories;

namespace Bank.Features;

// ანგარიშებს წამოღების კლასი
public class GetWallets
{
    // მომხმარებელი
    private readonly Guid _userId;

    // ანგარიშების სერვისი
    private readonly IWalletRepository _walletRepository;

    // კონსტრუქტორი ინიციალიზაციისთვის
    public GetWallets(Guid userId) 
    {
        _userId = userId;
        _walletRepository = new WalletRepository();
    }

    // გამშვები მეთოდი
    public async Task<string> Execute() 
    {
        // ვიღებთ მომხმარებლის ანგარიშებს
        var wallets = await _walletRepository.GetUserWallets(_userId);

        // თუ არ აქვს ანგარიში
        if (wallets is null || wallets.Count() == 0) return "There is no wallet for this account";

        // თუ აქვს ვუბრუნებთ ანგარიშებს სტრინგის ინტერპოლაციის მეშვეობით
        return string.Join("\n", wallets.Select(x => $"Account Name: {x.Name} | Balance: {x.Balance} | Account Number: {x.AccountNumber} | Created At: {x.CreatedAt.ToString("dd.MM.yyyy")}"));
    }
}
