using Book.Interfaces;

namespace Book.Managers;

// წიგნების მენერეჯი რომელიც იმპლემენტაციას უკეთებს
// წიგნების მენეჯერის ინტერფეისს
public class BookManager : IBookManager
{
    // ლისტი წიგნების შესანახად
    private readonly IList<Models.Book> _books = new List<Models.Book>();

    // კონსტრუქტორი
    public BookManager() { }

    // წიგნების დამატების მეთოდი
    public void Add(Models.Book book) 
    {
        _books.Add(book);
    }

    // წიგნების ამოღების მეთოდი
    public void Books()
    {
        // გამოყენებულია სტრინგის ინტერპოლაცია
        Console.WriteLine(string.Join("\n", _books.Select(x => $"Title: {x.Title} | Author: {x.Author} | Publish Year: {x.PublishYear}")));
    }

    // გაფილტვრის მეთოდი
    public void Filter(string title) 
    {
        // წიგნების სიას ვფილტრავთ და ვაბრუნებთ ტექსტს
        Console.WriteLine(string.Join("\n", _books.Where(x => x.Title.Contains(title)).Select(x => $"Title: {x.Title} | Author: {x.Author} | Publish Year: {x.PublishYear}")));
    }
}
