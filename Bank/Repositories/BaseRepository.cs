using Bank.Interfaces;
using Bank.Models;
using Bank.Settings;
using System.Text.Json;

namespace Bank.Repositories;

// ეს კლასი არის მშობელი ყველა იმ სერვისისთვის
// რომელიც კითხულობს ან ცვლილება შეაქვს
// ჩვენს ფაილში. აქ არის ასახული საბაზისო მეთოდები
// რომლებიც გვჭირდება ყველა სერვისში
public class BaseRepository<T> : IBaseRepository<T>
{
    // ყველა მონაცემის ამოღება
    public async Task<JsonModel> GetQuarable()
    {
        // ვკითხულობთ ფაილს
        string json = await File.ReadAllTextAsync(DbSettings.Path);

        // ვუკეთებს დესერილიზაციას
        var data = JsonSerializer.Deserialize<JsonModel>(json) ?? new JsonModel();

        return data;
    }

    // მონაცემის დამატება სიაში
    public async Task AddItemAsync(T item)
    {
        // ვიღებთ ყველა მოცანემს
        var data = await GetQuarable();

        // ვამატებთ კონკრეტულ ლისტში
        data.AddItem(item);

        // ავსახავთ ცვლილებებს
        await SaveJsonModelAsync(data);
    }

    // ვინახავთ ცვლილებეს ფაილში
    public async Task SaveJsonModelAsync(JsonModel jsonModel)
    {
        // ვუკეთებდ არსებულ მოდელს სერილიზაციას
        string json = JsonSerializer.Serialize(jsonModel);

        // არსებულ ფაილს ვაწერთ თავზე
        await File.WriteAllTextAsync(DbSettings.Path, json);
    }
}
