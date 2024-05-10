using WebAPI.Models;

namespace WebAPI.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? AuthorName { get; set; }
        public string? PublisherName { get; set; }
       
    }
}