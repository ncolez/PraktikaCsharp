using BookLibrary.ViewModels;

namespace BookLibrary.Services
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetBooksAsync();
        Task AddBookAsync(BookViewModel book);
    }
}