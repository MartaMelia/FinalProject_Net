using Bank.Models;

namespace Bank.Interfaces;

// ეს არის მომხმარებლების ინტერფეისი რომელიც შეიცავს
// კონკრეტულად ანგარიშებთან კომუნიკაციისთვის საჭირო მეთოდებს
// BaseRepository ში ხდება ტიპის განსაზღვრა რომ ავტომატურად მოხდეს დამეპვა
public interface IAccountRepository : IBaseRepository<Account>
{
    // მომხმარებლის პოვნა სახელის მეშვეობით
    Task<Account?> GetAccountByFirstName(string firstName);
}
