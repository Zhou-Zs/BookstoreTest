using Bookstore.Domain.IRepositories;
using Bookstore.Domain.Entities;

namespace Domain.Services
{
    public class ShoppingCartService(IShoppingCartRepository shoppingCartRepository,IBookRepository bookRepository)
    {
        private readonly IShoppingCartRepository _shoppingCartRepository = shoppingCartRepository;
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<ShoppingCartItem> AddOrUpdateCartItemAsync(ShoppingCartItem cartItem)
        {
            var book = await _bookRepository.GetBookByIdAsync(cartItem.BookId);
            if (book == null)
            {
                throw new ArgumentException($"当前bookId={cartItem.BookId}书籍不存在", nameof(cartItem.BookId));
            }

            var existItem = await _shoppingCartRepository.GetCartItemsByUserAndUserIdAsync(cartItem.UserId, cartItem.BookId);
            if (existItem == null)
            {
                await _shoppingCartRepository.AddCartItemAsync(cartItem);
                return await _shoppingCartRepository.GetCartItemsByIdAsync(cartItem.Id);
            }
            else
            {
                existItem.AddQuantity(cartItem.Quantity);
                await _shoppingCartRepository.UpdateCartItemAsync(existItem);
                return existItem;
            }
        }

    }

}
