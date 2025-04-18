using Bookstore.Application.DTO;
using AutoMapper;
using Bookstore.Domain.Entities;

namespace Bookstore.Application.Mapping
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<CreateBookDto, Book>().ConstructUsing(d => new Book(d.Title, d.Author, d.Price, d.Category));
            CreateMap<Book, BookDto>();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>() .ForMember(d => d.Book, opt => opt.MapFrom(s => s.Book));
        }
    }
}
