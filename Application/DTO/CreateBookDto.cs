namespace Bookstore.Application.DTO
{
    public record CreateBookDto(string Title, string Author, decimal Price, string Category);
}
