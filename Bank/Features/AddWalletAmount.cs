using Bank.Interfaces;
using Bank.Repositories;

namespace Bank.Features;

// ანგარიშზე თანხის შესატანი კლასი
public class AddWalletAmount
{
    // ვიძახებთ ანგარიშის სერვისს
    private readonly IWalletRepository _walletRepository;

    // მომხმარებელს
    private readonly Guid _userId;

    // თანხის რაოდენობას
    private readonly decimal _amount;

    // ანგარიშის სახელს
    private readonly string _walletName;

    // კონსტრუქტორით ხდება ინიციალიზაცია
    public AddWalletAmount(Guid userId, decimal amount, string walletName)
    {
        _userId = userId;
        _amount = amount;
        _walletName = walletName;
        _walletRepository = new WalletRepository();
    }

    // გამშვები მეთოდი
    public async Task Execute() 
    {
        // შეგვაქვს თანხა
        await _walletRepository.AddAmountWallet(_userId, _walletName, _amount);
    }
}
