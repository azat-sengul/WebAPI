using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {

        }

        public DbSet<Book> Books { get; set; }    //Sqlite’da oluşacak tablo adı
        public DbSet<Author> Authors { get; set; }    //Sqlite’da oluşacak tablo adı
        public DbSet<Publisher> Publishers { get; set; }    //Sqlite’da oluşacak tablo adı

    }
}