namespace Bank.Models;

// ჩვენს ფაილთან საკომუნიკაციო კლასი
public class JsonModel
{
    // ის შეიცავს მომხმარებლებს და ანგარიშებს
    public List<Account> Accounts { get; set; } = new List<Account>();
    public List<Wallet> Wallets { get; set; } = new List<Wallet>();

    // ეს მეთოდი ამატებს მოდელს ლისტში 
    // არის ჯენერიქი რომ მარტივად მოხდეს სხვადასხვა ტიპის
    // მოდელების დამატება
    public void AddItem<T>(T item)
    {
        // ვიღებთ მოდელის ტიპს
        var property = this.GetType().GetProperty(typeof(T).Name + "s");

        // და ვამატებთ კონკრეტულ მოდელს კონკრეტულ ლისტში
        if (property != null)
        {
            var list = property.GetValue(this) as IList<T>;
            list?.Add(item);
        }
        else
        {
            // თუ მოდელის ტიპი არ ემთხვევა გავდივართ ერორზე
            throw new InvalidOperationException("No matching collection found for type " + typeof(T).Name);
        }
    }
}

