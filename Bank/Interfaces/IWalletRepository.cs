using Bank.Models;

namespace Bank.Interfaces;

// ეს არის ანგარიშების ინტერფეისი რომელიც შეიცავს
// კონკრეტულად ანგარიშებთან კომუნიკაციისთვის საჭირო მეთოდებს
// BaseRepository ში ხდება ტიპის განსაზღვრა რომ ავტომატურად მოხდეს დამეპვა
public interface IWalletRepository : IBaseRepository<Wallet>
{
    // მეთოდი რომელიც აბრუნებს მომხმარებლის ანგარიშებს
    Task<List<Wallet>> GetUserWallets(Guid id);

    // მეთოდი რომელსაც თანხა შეაქვს ანგარიშზე
    Task<bool> AddAmountWallet(Guid userId, string walletName, decimal amount);

    // მეთოდი რომელსაც თანხა გამოაქვს ანგარიშიდან
    Task<bool> TakeAmount(Guid userId, string walletName, decimal amount);
}
