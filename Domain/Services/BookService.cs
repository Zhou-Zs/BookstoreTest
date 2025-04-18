using Bookstore.Domain.IRepositories;
using Bookstore.Domain.Entities;

namespace Domain.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<Book> CreateBookAsync(Book book)
        {
            var isExistbook = await _bookRepository.GetBookByTitleAsync(book.Title);
            if (isExistbook != null)
            {
                throw new ArgumentException($"当前{book.Title}书籍已存在", nameof(book.Title));
            }

            return await _bookRepository.AddBookAsync(book);
        }

    }

}
