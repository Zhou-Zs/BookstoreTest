
namespace Bookstore.Application.DTO
{
    public record ShoppingCartItemDto(int Id, BookDto? Book, int Quantity);
}
