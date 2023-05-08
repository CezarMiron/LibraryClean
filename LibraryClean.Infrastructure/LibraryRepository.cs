using LibraryClean.Application;
using LibraryClean.Core;

namespace LibraryClean.Infrastructure;

public class LibraryRepository : ILibraryRepository
{
    public void AddBook(Book book, List<Book> listOfBooks)
    {
        listOfBooks.Add(book);
    }
    
    public void RemoveBook(Book book, List<Book> listOfBooks)
    {
        listOfBooks.Remove(book);
    }
    
    public List<Book> CreateBooks()
    {
        // Get the data
        List<Book> books = new();
        books.Add(new Book(1, "The Fellowship of the Ring", "978-0261102354", 10.99, numberOfCopies: 2));
        books.Add(new Book(2, "The Two Towers", "978-0261102361", 10.99, numberOfCopies: 4));
        books.Add(new Book(3, "The Return of the King", "978-0261102378", 10.99, numberOfCopies: 7));
        books.Add(new Book(4, "The Hobbit", "978-0261103283", 2, numberOfCopies: 5));
        books.Add(new Book(5, "The Silmarillion", "978-0261102736", 10.99, numberOfCopies: 2));

        return books;
    }
    public List<RentedBooks> CreateRentedBooks()
    {
        // Get the Data
        List<RentedBooks> rentedBooks = new();
        rentedBooks.Add(new RentedBooks(1, 1, DateTime.Now.AddDays(-14), DateTime.Now.AddDays(3)));
        rentedBooks.Add(new RentedBooks(2, 2, DateTime.Now.AddDays(-3), DateTime.Now.AddDays(5)));
        rentedBooks.Add(new RentedBooks(3, 2, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(10)));

        return rentedBooks;
    }
}