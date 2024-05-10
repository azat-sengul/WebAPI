namespace WebAPI.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }
}