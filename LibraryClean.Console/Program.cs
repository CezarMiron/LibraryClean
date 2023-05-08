using LibraryClean.Application;
using LibraryClean.Core;
using LibraryClean.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryClean;


internal class Program
{
    static void Main(string[] args)
    {
        //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<ILibraryService, LibraryService>()
            .AddSingleton<ILibraryRepository, LibraryRepository>()
            .BuildServiceProvider();        
        
        var service = serviceProvider.GetRequiredService<ILibraryService>();

        // Create a library
        var books = service.CreateSomeBooks();
        var rentedBooks = service.CreateSomeRentedBooks();
        
        // Add a book to the library
        AddANewBook(service, books);

        // Get the list of books from the library
        GetListOfBooksFromLibrary(books, service);
        
        // Get the list of rented books from the library
        GetListOfRentedBooksFromLibrary(rentedBooks, books, service);
        
        // Get number of copies of a book
        GetNumberCopiesOfABook(service, books);

        // Rent a book
        RentABook(books, rentedBooks, service);
        ReturnABook(books, rentedBooks, service);
        // Dispose services
        DisposeServices(serviceProvider);

    }

    private static void AddANewBook(ILibraryService service, List<Book> books)
    {
        Book newBook = new();
        Console.WriteLine("It is your turn to create a book :)");
        Console.WriteLine("Enter Title:");
        newBook.Title = Console.ReadLine();
        Console.WriteLine("Enter ISBN:");
        newBook.ISBN = Console.ReadLine();
        Console.WriteLine("Enter Number of Copies:");
        newBook.NumberOfCopies = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Rent Price:");
        newBook.RentPrice = Convert.ToDouble(Console.ReadLine());
        service.AddBook(newBook, books);
    }

    private static void GetNumberCopiesOfABook(ILibraryService service, List<Book> books)
    {
        Console.WriteLine("Enter ISBN:");
        var isbn = Console.ReadLine();

        if (isbn != null)
            Console.WriteLine(service.GetNumberOfCopiesOfBook(isbn.Trim(), books) + " books found with ISBN " + isbn);
    }

    private static void GetListOfBooksFromLibrary(List<Book> listOfBooks, ILibraryService service)
    {
        Console.WriteLine("List of books in the library:");
        foreach (var book in service.GetAllBooksFromLibrary(listOfBooks))
        {
            Console.WriteLine(book.Title + " " + book.ISBN + " " + book.NumberOfCopies + " " + book.RentPrice);
        }
        Console.WriteLine("--------------------");
    }
    
    private static void GetListOfRentedBooksFromLibrary(List<RentedBooks> listOfRentedBooks, List<Book> listOfBooks, ILibraryService service)
    {
        Console.WriteLine("List of rented books in the library:");
        foreach (var rentedBook in service.GetAllRentedBooks(listOfRentedBooks, listOfBooks))
        {
            Console.WriteLine(rentedBook.Book?.Title + " rented from : " + rentedBook.StartRentDate.Date + " to: " + rentedBook.EndRentDate.Date);
        }
        Console.WriteLine("--------------------");
    }

    private static void ReturnABook(List<Book> listOfBooks, List<RentedBooks> rentedBooksList, ILibraryService service)
    {
        Console.WriteLine("You will return a book. Enter ISBN:");
        var isbnToReturn = Console.ReadLine();
        if (isbnToReturn != null)
        {
            var bookToReturn = service.GetBookByISBN(isbnToReturn.Trim(), listOfBooks);
            Console.WriteLine("After how many days do you want to return the book?");
            var daysToRent = Convert.ToInt32(Console.ReadLine());
            if (bookToReturn != null)
            {
                var priceToPay = service.GetThePriceOfRentingABook(bookToReturn, daysToRent);
                Console.WriteLine("You will have to pay : " + priceToPay + " euros.");
                service.UpdateRentBook(bookToReturn, rentedBooksList, daysToRent);
                service.IncreaseNumberOfCopiesOfBook(bookToReturn, listOfBooks);
            }
            else
            {
                Console.WriteLine("Book not found. Try again.");
            }
        }

    }
    private static void RentABook(List<Book> listOfBooks, List<RentedBooks> rentedBooksList, ILibraryService service)
    {
        Console.WriteLine("You will borrow a book. Enter ISBN:");
        var isbnToRent = Console.ReadLine();
        if (isbnToRent == null) return;
        var bookToRent = service.GetBookByISBN(isbnToRent.Trim(), listOfBooks);
        if (bookToRent != null && bookToRent.NumberOfCopies > 0)
        {
            Console.WriteLine("You will rent the book " + bookToRent.Title + " with ISBN " + bookToRent.ISBN + " for " + bookToRent.RentPrice + " euros / day.");
            service.AddRentBook(bookToRent, DateTime.Now.Date, rentedBooksList);
            service.DecreaseNumberOfCopiesOfBook(bookToRent, listOfBooks);
        }
        else
        {
            Console.WriteLine("Book not found. Try again.");
        }
    }

    private static void DisposeServices(IServiceProvider serviceProvider) {
        if (serviceProvider is IDisposable sp) {
            sp.Dispose();
        }
    }
}