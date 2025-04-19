using Bookstore.Domain.Entities;
using System;

namespace Bookstore.Domain.IRepositories
{
    public interface IShoppingCartRepository
    {
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        Task<ShoppingCartItem> AddCartItemAsync(ShoppingCartItem cartItem);

        /// <summary>
        /// 根据购物车ID获取购物车
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ShoppingCartItem?> GetCartItemsByIdAsync(int id);

        /// <summary>
        /// 根据用户ID获取购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<ShoppingCartItem>> GetCartItemsByUserAsync(string userId);

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        Task UpdateCartItemAsync(ShoppingCartItem cartItem);

        /// <summary>
        /// 根据用户ID和书籍ID获取购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="BookId"></param>
        /// <returns></returns>
        Task<ShoppingCartItem?> GetCartItemsByUserAndUserIdAsync(string userId, int BookId);
    }
}
