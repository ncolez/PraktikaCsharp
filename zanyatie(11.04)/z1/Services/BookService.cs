using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;
using BookLibrary.ViewModels;

namespace BookLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookViewModel>> GetBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                Genre = b.Genre,
                Year = b.Year
            }).ToList();
        }

        public async Task AddBookAsync(BookViewModel book)
        {
            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Genre = book.Genre,
                Year = book.Year
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
        }
    }
}