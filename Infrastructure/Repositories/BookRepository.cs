using Bookstore.Domain.IRepositories;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class BookRepository(BookstoreDdContext context) : IBookRepository
    {
        private readonly BookstoreDdContext _context = context;

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Where(c=> !c.IsDeleted).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<Book?> GetBookByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(c => c.Title == title && !c.IsDeleted);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
