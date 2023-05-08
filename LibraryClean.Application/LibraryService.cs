using LibraryClean.Core;

namespace LibraryClean.Application;

public class LibraryService : ILibraryService
{
    private readonly ILibraryRepository _libraryRepository;
    const int TwoWeeks = 14;

    public LibraryService(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    public List<Book> CreateSomeBooks()
    {
        Console.WriteLine("I will create some books for you");
        return _libraryRepository.CreateBooks();
    }

    public List<RentedBooks> CreateSomeRentedBooks()
    {
        Console.WriteLine("I will create some rented books for you");
        return _libraryRepository.CreateRentedBooks();
    }

    public List<RentedBooks> GetAllRentedBooks(List<RentedBooks> rentedBooks, List<Book> listOfBooks)
    {
        foreach (var rentedBook in rentedBooks)
            if (listOfBooks != null)
                rentedBook.Book = listOfBooks.FirstOrDefault(x => rentedBook.BookId == x.BookId);
        
        return rentedBooks;
    }


    public List<Book> GetAllBooksFromLibrary(List<Book> listOfBooks)
    {
        // in theory we should call the repository here in order to get all books from the library
        // but for the sake of simplicity we just return the list of books that we already have
        return listOfBooks;
    }

    public double GetThePriceOfRentingABook(Book book, int numberOfDays)
    {
        var totalPrice = book.RentPrice * numberOfDays;
        if (numberOfDays > TwoWeeks) 
            totalPrice = totalPrice + (numberOfDays - TwoWeeks) * book.RentPrice * 0.01;

        return totalPrice;
    }

    public Book? GetBookByISBN(string isbn, List<Book> listOfBooks)
    {
        return listOfBooks.FirstOrDefault(x => x?.ISBN == isbn);
    }

    public double GetRentPriceOfBook(string isbn, List<Book> listOfBooks)
    {
        var book = listOfBooks.FirstOrDefault(x => x.ISBN == isbn);
        return book?.RentPrice ?? 0;
    }

    public int GetNumberOfCopiesOfBook(string isbn, List<Book> listOfBooks)
    {
        var book = listOfBooks.FirstOrDefault(x => x.ISBN == isbn);
        return book?.NumberOfCopies ?? 0;
    }

    public void AddBook(Book newBook, List<Book> listOfBooks)
    {
        var existingbook = listOfBooks.FirstOrDefault(x => x.ISBN == newBook.ISBN);
        if (existingbook != null)
        {
            listOfBooks[listOfBooks.IndexOf(existingbook)].NumberOfCopies += newBook.NumberOfCopies;
        }
        else
        {
            if(listOfBooks.Count == 0)
                newBook.BookId = 1;
            else
                newBook.BookId = listOfBooks.Last().BookId + 1;
            _libraryRepository.AddBook(newBook, listOfBooks);
        }
    }
    
    public void AddRentBook(Book book, DateTime startDate, List<RentedBooks> rentedBooks)
    {
        var rentedBook = new RentedBooks(rentedBooks.Last().Id + 1, book.BookId, startDate, startDate.AddDays(14));
        rentedBooks.Add(rentedBook);
    }

    public void UpdateRentBook(Book book, List<RentedBooks> rentedBooks, int rentedDays)
    {
        var rentedBook = rentedBooks.FirstOrDefault(x => x.BookId == book.BookId);
        if (rentedBook != null)
        {
            rentedBook.EndRentDate = rentedBook.StartRentDate.AddDays(rentedDays);
        }
    }

    public void DecreaseNumberOfCopiesOfBook(Book? book, List<Book> listOfBooks)
    {
        if (book != null) listOfBooks[listOfBooks.IndexOf(book)]!.NumberOfCopies -= 1;
    }

    public void IncreaseNumberOfCopiesOfBook(Book? book, List<Book> listOfBooks)
    {
        if (book != null) listOfBooks[listOfBooks.IndexOf(book)]!.NumberOfCopies += 1;
    }
}