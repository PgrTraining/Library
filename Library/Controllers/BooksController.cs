using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class BooksController : Controller
    {
        readonly LibraryDataContext Context;

        public BooksController(LibraryDataContext context)
        {
            this.Context = context;
        }

        [HttpPut("books/{bookId:int}/numberofpages")]
        public async Task<ActionResult> ChangeNumberOfPages(int bookId, [FromBody] int numberOfPages)
        {
            var book = await Context.Books
                .Where(b => b.Id == bookId && b.InStock)
                .SingleOrDefaultAsync();

            if(book != null)
            {
                book.NumberOfPages = numberOfPages;
                await Context.SaveChangesAsync();
                return NoContent();
            } else
            {
                return NotFound();
            }
        }

        [HttpDelete("books/{bookId:int}")]
        public async Task<ActionResult> RemoveABook(int bookId)
        {
            var bookToRemove = await Context.Books
                .Where(b => b.InStock && b.Id == bookId)
                .SingleOrDefaultAsync();

            if (bookToRemove != null)
            {
                bookToRemove.InStock = false;
                await Context.SaveChangesAsync();
            }

            return NoContent(); // send back a "Fine"
        }

        [HttpPost("books")]
        public async Task<ActionResult> AddABook([FromBody] AddABookModel bookToAdd)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book {
                Title = bookToAdd.Title,
                Author = bookToAdd.Author,
                Genre = bookToAdd.Genre,
                NumberOfPages = bookToAdd.NumberOfPages,
                InStock = true
            };

            Context.Books.Add(book);
            await Context.SaveChangesAsync();

            var response = new GetABookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                NumberOfPages = book.NumberOfPages
            };

            return CreatedAtRoute("books#getabook", new { bookId = response.Id }, response);
        }

        /// <summary>
        /// Retrieves one of the books from our stockpile
        /// </summary>
        /// <param name="bookId">The ID of the book you want to retrieve</param>
        /// <returns>A book</returns>
        [HttpGet("books/{bookId:int}", Name = "books#getabook")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetABook(int bookId)
        {
            var book = await Context.Books
                .Where(b => b.InStock && b.Id == bookId)
                .Select(b => new GetABookResponse
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    NumberOfPages = b.NumberOfPages
                }).SingleOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        [HttpGet("books")]
        public async Task<ActionResult<GetBooksResponse>> GetAllBooks([FromQuery] string genre)
        {
            var books = Context.Books
                .Where(b => b.InStock)
                .Select(b => new GetBooksResponseItem
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    NumberOfPages = b.NumberOfPages
                });

            if(genre != null)
            {
                books = books.Where(b => b.Genre == genre);
            }
            var booksList = await books.ToListAsync();

            var response = new GetBooksResponse
            {
                Books = booksList,
                GenreFilrer = genre,
                NumberOfBooks = booksList.Count
            };
            return Ok(response);
        }
    }
}
