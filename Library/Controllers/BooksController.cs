using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryApi.Controllers
{
    public class BooksController: Controller
    {
        readonly LibraryDataContext Context;

        public BooksController(LibraryDataContext context)
        {
            this.Context = context;
        }

        [HttpGet("books")]
        public ActionResult GetAllBooks()
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
                })
                .ToList();

            var response = new GetBooksResponse
            {
                Books = books,
                GenreFilrer = null,
                NumberOfBooks = books.Count
            };
            return Ok(response);
        }
    }
}
