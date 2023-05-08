namespace LibraryClean.Core;

public class RentedBooks
{
    public RentedBooks(int id, int bookId, DateTime startRentDate, DateTime endRentDate)
    {
        Id = id;
        BookId = bookId;
        StartRentDate = startRentDate;
        EndRentDate = endRentDate;
    }
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime StartRentDate { get; set; }
    public DateTime EndRentDate { get; set; }
    
    public virtual Book? Book { get; set; }
}

