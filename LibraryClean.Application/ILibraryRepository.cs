using LibraryClean.Core;

namespace LibraryClean.Application;

public interface ILibraryRepository
{
    List<Book> CreateBooks();
    List<RentedBooks> CreateRentedBooks();
    void AddBook(Book book, List<Book> listOfBooks);
    void RemoveBook(Book book, List<Book> listOfBooks);
}