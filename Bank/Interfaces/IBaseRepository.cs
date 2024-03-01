using Bank.Models;
namespace Bank.Interfaces;

// ეს არის ჯენერიქ ინტერფეისი
// რომელიც შეიცავს ელემენტარულ მეთოდებს
// ფაილთან საკომუნიკაციოდ
public interface IBaseRepository<T>
{
    // ფაილიდან ყველა მონაცემის ამოღება
    Task<JsonModel> GetQuarable();
    // ფაილში ცვლილებების შენახვა
    Task SaveJsonModelAsync(JsonModel jsonModel);

    // ფაილში მონაცემის დამატება
    Task AddItemAsync(T item);
}
