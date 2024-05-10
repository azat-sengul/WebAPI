using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook()
        {
            var books= await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Select(b => new BookDTO {
                BookId = b.BookId,
                BookName = b.BookName,
                AuthorName = b.Author.AuthorName,
                PublisherName=b.Publisher.PublisherName,
            }).ToListAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int? id)
        {
            if (id == null)
            {
                return NotFound(); // 404 durum kodu, kaynak bulunamadı
            }
            var book= await _context.Books.FirstOrDefaultAsync(i => i.BookId == id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(Book entity)
        {
            _context.Books.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBook", new {id = entity.BookId}, entity); // Veri eklenir ve 201 durum kodu gönderilir. Veri eklendikten sonra eklenen verinin kodu geri döndürülür.
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(int id, Book entity)
        {
            if(id != entity.BookId)
            {
                return BadRequest(); //400lü hata, kullanıca tarafndan gönderilen hatalı talep
            }
            var book = await _context.Books.FirstOrDefaultAsync(i => i.BookId == id);
            if (book == null)
            {
                return NotFound();
            }           
            book.Author = entity.Author;
            book.BookName = entity.BookName;
            book.Publisher = entity.Publisher;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return NoContent(); // 204 nolu durum kodu, herşeyin normal olduğunu gösterir
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            if(id == null)
            {
                return NotFound(); 
            }

            var book = await _context.Books.FirstOrDefaultAsync(i => i.BookId==id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}