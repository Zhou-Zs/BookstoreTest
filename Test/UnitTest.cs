using Bookstore.Domain.Entities;
using Bookstore.Domain.IRepositories;
using Domain.Services;
using Bookstore.Infrastructure;
using Bookstore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class UnitTest
    {
        public class BookstoreUnitTest : IDisposable
        {
            private readonly BookstoreDdContext _context;
            private readonly IBookRepository _bookRepository;
            private readonly IShoppingCartRepository _cartRepository;
            private readonly ShoppingCartService _shoppingCartService;

            public BookstoreUnitTest()
            {
                var options = new DbContextOptionsBuilder<BookstoreDdContext>()
                    .UseInMemoryDatabase("testDB")
                    .Options;
                _context = new BookstoreDdContext(options);

                _bookRepository = new BookRepository(_context);
                _cartRepository = new ShoppingCartRepository(_context);
                _shoppingCartService = new ShoppingCartService(_cartRepository, _bookRepository);
            }

            [Fact]
            public async Task CreateBookAsyncTest()
            {
                var book = new Book("大话西游", "张三", 30m, "玄幻");

                var addBook = await _bookRepository.AddBookAsync(book);

                Assert.NotNull(addBook);
                Assert.True(addBook.Id > 0);
                Assert.Equal("大话西游", addBook.Title);

                var book1 = await _bookRepository.GetBookByIdAsync(addBook.Id);
                Assert.NotNull(book1);
                Assert.Equal("张三", book1!.Author);
                Assert.Equal("大话西游", book1!.Title);
                Assert.Equal("玄幻", book1!.Category);
                Assert.Equal(30m, book1!.Price);   
            }

            [Fact]
            public async Task GetBookTest()
            {
                var book1 = new Book("如来神掌", "如来", 33m, "神话");
                var book2 = new Book("如来神掌1", "如来1", 22m, "神话4");

                await _bookRepository.AddBookAsync(book1);
                await _bookRepository.AddBookAsync(book2);

                var books = await _bookRepository.GetAllBooksAsync();

                Assert.NotNull(books);
                Assert.Equal(2, books.Count()); // 书籍添加个数比对
            }

            [Fact]
            public async Task AddToCartTerst()
            {
                var book = new Book("大话西游", "周新新", 555m, "神花");
                var addBook = await _bookRepository.AddBookAsync(book);
                var userId = "123";

                var cartItem = new ShoppingCartItem(userId, addBook.Id, 1);
                var addCartItem = await _shoppingCartService.AddOrUpdateCartItemAsync(cartItem);
                Assert.NotNull(addCartItem); 
                Assert.Equal(1, addCartItem.Quantity); // 书本数量比对

                var additionalCartItem = new ShoppingCartItem(userId, addBook.Id, 2);
                var updateCartItem = await _shoppingCartService.AddOrUpdateCartItemAsync(additionalCartItem);
                Assert.NotNull(updateCartItem);
                Assert.Equal(3, updateCartItem.Quantity); // 书本增量比对

                var subTotal = updateCartItem.GetSubTotal();
                Assert.Equal(555m * 3, subTotal);  // 金额汇总比对
            }

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
}