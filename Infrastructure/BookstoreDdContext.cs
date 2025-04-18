using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure
{
    public class BookstoreDdContext(DbContextOptions<BookstoreDdContext> options) : IdentityDbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
