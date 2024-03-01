using Bank.Interfaces;
using Bank.Models;

namespace Bank.Repositories;

// ანგარიშის სერვისი რომელიც შვილია BaseRepository-ის
// სადაც განვსაზღვრავთ ანგარიშის ტიპს და ასევე ვაიმპლიმენტირებთ
// ინტერფეისს ანგარიშის სერვისისთვის
public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
{
    // მომხმარებლის ანგარიშების მეთოდი
    public async Task<List<Wallet>> GetUserWallets(Guid id) 
    {
        // ვიღებთ ყველა მონაცემს ფაილიდან
        var queries = await GetQuarable();

        // ვფილტრავთ მომხმარებლის ID ის მეშვეობით
        return queries.Wallets.Where(x => x.AccountId == id).ToList();
    }

    // ანგარიშში თანხის დამატების ემთოდი
    public async Task<bool> AddAmountWallet(Guid userId, string walledName, decimal amount) 
    {
        // ვიღებთ ყველა მონაცემს ფაილიდან
        var queries = await GetQuarable();

        // ვფილტრავთ არსებულ ანგარიშებს
        var wallet = queries.Wallets.Where(x => x.AccountId == userId && x.Name == walledName).FirstOrDefault();

        // თუ ანგარიში არ არსებობს
        if (wallet is null) return false;

        // თუ ანგარიში არსებობს ვიგებთ ინდექსს
        var index = queries.Wallets.IndexOf(wallet);

        // ვანახლებთ ანგარიშში თანხას
        wallet.In(amount);

        // ვანახლებთ ლისტში ანგარიშს
        queries.Wallets[index] = wallet;

        // ვინახავთ ცვლილებებს ფაილში
        await SaveJsonModelAsync(queries);

        return true;
    }

    // ანგარიშიდან თანხის გამოტანის მეთოდი
    public async Task<bool> TakeAmount(Guid userId, string walledName, decimal amount)
    {
        // ვიყებთ ყველა მონაცემს ფაილიდან
        var queries = await GetQuarable();

        // ვფილტრავთ ანგარიშებს
        var wallet = queries.Wallets.Where(x => x.AccountId == userId && x.Name == walledName).FirstOrDefault();

        // თუ ანგარიში არ არსებობს
        if (wallet is null) return false;

        // ვპოულობთ ინდექსს
        var index = queries.Wallets.IndexOf(wallet);

        // გამოგვაქვს თანხა
        wallet.Out(amount);

        // ვანახლებთ ანგარიშს სიაში
        queries.Wallets[index] = wallet;

        // ვანახლებთ ფაილს
        await SaveJsonModelAsync(queries);

        return true;
    }
}
