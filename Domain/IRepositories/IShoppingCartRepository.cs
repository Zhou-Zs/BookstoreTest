using Bookstore.Domain.Entities;
using System;

namespace Bookstore.Domain.IRepositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCartItem> AddCartItemAsync(ShoppingCartItem cartItem);
        Task<ShoppingCartItem?> GetCartItemsByIdAsync(int id);
        Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserAsync(string userId);
        Task UpdateCartItemAsync(ShoppingCartItem cartItem);
        Task<ShoppingCartItem?> GetCartItemsByUserAndUserIdAsync(string userId, int BookId);
    }
}
