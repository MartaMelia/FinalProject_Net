using Bank.Interfaces;
using Bank.Repositories;

namespace Bank.Features;

// ანგარიშიდან თანხის გამოტანის კლასი
public class TakeWalletAmount
{
    // ანგარიშის სერვისი
    private readonly IWalletRepository _walletRepository;
    // მომხმარებელი
    private readonly Guid _userId;
    // თანხა
    private readonly decimal _amount;
    // ანგარიშის სახელი
    private readonly string _walletName;

    // კონსტრუქტორი ინიციალიზაციისთვის
    public TakeWalletAmount(Guid userId, decimal amount, string walletName)
    {
        _userId = userId;
        _amount = amount;
        _walletName = walletName;
        _walletRepository = new WalletRepository();
    }

    // გამშვები მეთოდი
    public async Task Execute()
    {
        // თანხის გამოტანა
        await _walletRepository.TakeAmount(_userId, _walletName, _amount);
    }
}
