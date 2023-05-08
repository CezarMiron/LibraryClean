using LibraryClean.Core;

namespace LibraryClean.Application;

public interface ILibraryService
{
    public List<Book> CreateSomeBooks();
    List<Book> GetAllBooksFromLibrary(List<Book> listOfBooks);
    Book? GetBookByISBN(string isbn, List<Book> listOfBooks);
    double GetThePriceOfRentingABook(Book book, int numberOfDays);
    int GetNumberOfCopiesOfBook(string isbn, List<Book> listOfBooks);
    void AddBook(Book newBook, List<Book> listOfBooks);
    void DecreaseNumberOfCopiesOfBook(Book? book, List<Book> listOfBooks);
    void IncreaseNumberOfCopiesOfBook(Book? book, List<Book> listOfBooks);

    List<RentedBooks> CreateSomeRentedBooks();
    List<RentedBooks> GetAllRentedBooks(List<RentedBooks> rentedBooks, List<Book> listOfBooks);
    void AddRentBook(Book book, DateTime startDate, List<RentedBooks> rentedBooks);
    void UpdateRentBook(Book book, List<RentedBooks> rentedBooks, int rentedDays);
    double GetRentPriceOfBook(string isbn, List<Book> listOfBooks);
}