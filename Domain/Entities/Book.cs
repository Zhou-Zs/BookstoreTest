

namespace Bookstore.Domain.Entities
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public string Category { get; private set; }

        private Book() { }

        public Book(string title, string author, decimal price, string category)
        {
            if (price <= 0)
            {
                throw new ArgumentException("价格要大于0");
            }
            Title = title;
            Author = author;
            Price = price;
            Category = category;
        }
    }
}
