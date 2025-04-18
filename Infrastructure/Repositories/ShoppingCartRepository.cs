using Bookstore.Domain.IRepositories;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class ShoppingCartRepository(BookstoreDdContext context) : IShoppingCartRepository
    {
        private readonly BookstoreDdContext _context = context;

        public async Task<ShoppingCartItem> AddCartItemAsync(ShoppingCartItem cartItem)
        {
            _context.ShoppingCartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<ShoppingCartItem?> GetCartItemsByIdAsync(int id)
        {
            return await _context.ShoppingCartItems.Include(c => c.Book)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ShoppingCartItem?> GetCartItemsByUserAndUserIdAsync(string userId, int bookId)
        {
            return await _context.ShoppingCartItems.Include(c => c.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == bookId);
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserAsync(string userId)
        {
            return await _context.ShoppingCartItems.Include(c => c.Book)
                                .Where(c => c.UserId == userId)
                                .ToListAsync();
        }

        public async Task UpdateCartItemAsync(ShoppingCartItem cartItem)
        {
            _context.ShoppingCartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
