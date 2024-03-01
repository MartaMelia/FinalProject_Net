using Bank.Interfaces;
using Bank.Models;
using Bank.Repositories;

namespace Bank.Features;

// ანგარიშის შექმნის კლასი
public class CreateWallet
{
    // ვიძახებთ ანგარიშის სერვისს
    private readonly IWalletRepository _walletRepository;

    // ანგარიშს
    private readonly Wallet _wallet;

    // კონსტრუქტორით ხდება ინიციალიზაცია
    public CreateWallet(Wallet wallet)
    {
        _wallet = wallet;
        _walletRepository = new WalletRepository();
    }

    // გამშვები მეთოდი
    public async Task Execute() 
    {
        // ვამატებთ ანგარიშს
        await _walletRepository.AddItemAsync(_wallet);
    }
}
