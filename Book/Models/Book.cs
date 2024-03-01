namespace Book.Models;

// წიგნის კლასი
public class Book
{
    // სათაური
    public string Title { get; private set; } = null!;
    // ავტორი
    public string Author { get; private set; } = null!;
    // გამოშვების წელი
    public int PublishYear { get; private set; }

    // კონსტრუქტორი და ინიციალიზაცია
    public Book(string title, string author, int publishYear)
    {
        Title = title;
        Author = author;
        PublishYear = publishYear;
    }
}
