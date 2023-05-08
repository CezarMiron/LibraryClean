namespace LibraryClean.Core;

public class Book
{
    public Book(int bookId, string? title, string iSBN, double rentPrice, int numberOfCopies, int numberOfCopiesRented = 0)
    {
        BookId = bookId;
        Title = title;
        ISBN = iSBN;
        RentPrice = rentPrice;
        NumberOfCopies = numberOfCopies;
        NumberOfCopiesRented = numberOfCopiesRented;
    }

    public Book()
    {
        
    }
    public int BookId { get; set; }
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public double RentPrice { get; set; }
    public int NumberOfCopies { get; set; }
    public int NumberOfCopiesRented { get; set; }
} 