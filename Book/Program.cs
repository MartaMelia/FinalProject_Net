using Book.Interfaces;
using Book.Managers;
using System.Security.Cryptography;

namespace Book;

public class Program 
{
    private static readonly IBookManager _manager = new BookManager();

    public Program() 
    {
    }

    public static void Main(string[] args)
    {
        // მთავარი გამშვები ლოგიკა
        while (true) 
        {
            Console.WriteLine("Welcome to our library");
            Console.WriteLine("This is our Services:");
            Console.WriteLine("(1) Create Book");
            Console.WriteLine("(2) Filter Books");
            Console.WriteLine("(3) Books");
            Console.WriteLine("Choose the service: 1 | 2 | 3:");
            // სერვისის არჩევა
            string input = Console.ReadLine()?? string.Empty;

            // თუ დაემთხვა სერვისი
            if (input == "1")
            {
                // შექმნა
                Create();
            }
            else if (input == "2")
            {
                // გაფილტვრა
                Filter();
            }
            else if (input == "3")
            {
                // წიგნები
                Books();
            }
            else 
            {
                // სერვისი არ არსებობს
                Console.WriteLine("We do not have that service;");
            }
        }
    }

    // გაფილტვრის მეთოდი
    private static void Filter() 
    {
        // წიგნის სათაურის მოთხოვნა
        Console.WriteLine("To Filter books enter book title");
        string title = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrEmpty(title)) 
        {
            Console.WriteLine("Invalid Input");
            Filter();
        }

        // ფილტრის გამოძახება
        _manager.Filter(title);
    }

    // წიგნების ამოღების მეთოდი
    private static void Books() 
    {
        // სერვისის გამოძახება
        _manager.Books();
    }

    // წიგნის შექმნის მეთოდი
    private static void Create() 
    {
        Console.WriteLine("To Create a book fill the form:");
        Console.WriteLine("Enter book title:");

        // სათაური
        string title = Console.ReadLine()?? string.Empty;

        Console.WriteLine("Enter book author:");
        // ავტორი
        string author = Console.ReadLine()?? string.Empty;

        Console.WriteLine("Enter publish year");
        // გამოშვების წელი
        string year = Console.ReadLine()?? string.Empty;

        // ვალიდაცია
        if (!int.TryParse(year, out int publishyear) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || string.IsNullOrEmpty(year))
        {
            Console.WriteLine("Invalid Inputs");
            Create();
        }
        else 
        {
            // წიგნის მოდელი
            Models.Book book = new(title, author, publishyear);

            // წიგნის მოდელის დამატება
            _manager.Add(book);

            Console.WriteLine("Book added successfully.");
        }
    }
}