using Bank.Interfaces;
using Bank.Models;
using Bank.Repositories;

namespace Bank.Services;

// მომხმარებლების სერვისი რომელიც შვილია BaseRepository-ის
// სადაც განვსაზღვრავთ ანგარიშის ტიპს და ასევე ვაიმპლიმენტირებთ
// ინტერფეისს მომხმარებლების სერვისისთვის
public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    // მომხმარებლის პოვნა სახელით
    public async Task<Account?> GetAccountByFirstName(string firstName) 
    {
        // ვიღებთ ყველა მონაცემს ფაილიდან
        var accounts = await GetQuarable();

        // ვპოულობთ მომხმარებელს
        var account = accounts.Accounts.Where(x => x.FirstName == firstName.Trim()).FirstOrDefault();

        return account;
    }
}
