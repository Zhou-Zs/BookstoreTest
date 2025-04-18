
namespace Bookstore.Domain.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public int BookId { get; private set; }
        public int Quantity { get; private set; }
        public Book? Book { get; private set; }

        private ShoppingCartItem() { }

        public ShoppingCartItem(string userId, int bookId, int quantity)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("UserId不要为空", nameof(userId));
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("数量要大于0", nameof(quantity));
            }  

            UserId = userId;
            BookId = bookId;
            Quantity = quantity;
        }

        public void AddQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("增加数量要大于0", nameof(quantity));
            }
            Quantity += quantity;
        }

        public decimal GetSubTotal()
        {
            if (Book == null)
            {
                return 0;
            }
            return Book.Price * Quantity;
        }
    }
}
