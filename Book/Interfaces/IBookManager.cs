namespace Book.Interfaces;

// წიგნის მენეჯერის ინტერფეისი
// შექმნილია საჭირო მეთოდები
public interface IBookManager
{
    // წიგნები
    void Books();
    // ფილტრი
    void Filter(string title);
    // დამატება
    void Add(Models.Book book);
}
