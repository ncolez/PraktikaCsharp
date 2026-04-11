using Microsoft.AspNetCore.Mvc;
using BookLibrary.Services;
using BookLibrary.ViewModels;

namespace BookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: /Books
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetBooksAsync();
            return View(books);
        }

        // GET: /Books/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var books = await _bookService.GetBooksAsync();
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: /Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(book);

                TempData["SuccessMessage"] = $"Книга \"{book.Title}\" успешно добавлена!";

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
    }
}