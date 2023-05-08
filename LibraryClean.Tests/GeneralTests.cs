using LibraryClean.Application;
using LibraryClean.Core;
using Moq;

namespace LibraryClean.Tests;

public class Tests
{
    private List<Book> _listOfBooks;
    private ILibraryService _libraryService;
    private Mock<ILibraryRepository> _mockRepo;
    
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<ILibraryRepository>();
        _libraryService = new LibraryService(_mockRepo.Object);
        _listOfBooks = new List<Book>();
    }

    [Test]
    public void GetRentPriceOfABook_ReturnsSameNumber()
    {
        _listOfBooks.Add(new Book(4, "The Mobbit", "978-0261134523", 2, numberOfCopies: 5));
        var price = _libraryService.GetRentPriceOfBook("978-0261134523", _listOfBooks);
        Assert.That(price, Is.EqualTo(2));
    }
    
    [Test]
    public void CalculateRentPriceOfABook_ReturnsSameNumber()
    {
        var book = new Book(5, "The Zobbit", "978-0261234523", 2, numberOfCopies: 5);
        var price = _libraryService.GetThePriceOfRentingABook(book, 15);
        Assert.That(price, Is.EqualTo(30.02));
    }
    
    [Test]
    public void GetNumberOfCopiesOfABook_ReturnsSameNumber()
    {
        _listOfBooks.Add(new Book(4, "The Mobbit", "978-0261134523", 2, numberOfCopies: 5));
        var numberOfCopies = _libraryService.GetNumberOfCopiesOfBook("978-0261134523", _listOfBooks);
        Assert.That(numberOfCopies, Is.EqualTo(5));
    }
    
    [Test]
    public void GetBookByISBN_ReturnsSameBook()
    {
        _listOfBooks.Add(new Book(4, "The Mobbit", "978-0261134523", 2, numberOfCopies: 5));
        var book = _libraryService.GetBookByISBN("978-0261134523", _listOfBooks);
        Assert.That(book, Is.EqualTo(_listOfBooks[0]));
    }
}