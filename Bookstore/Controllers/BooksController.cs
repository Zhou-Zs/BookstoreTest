using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bookstore.Domain.IRepositories;
using AutoMapper;
using Bookstore.Application;
using FluentValidation;
using Bookstore.Application.DTO;
using Bookstore.Domain.Entities;        

namespace Bookstore.API.Controllers
{
    /// <summary>
    /// 图书
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IBookRepository bookRepository, IMapper mapper, IValidator<CreateBookDto> createBookValidator) : ControllerBase
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBookDto> _createBookValidator = createBookValidator;

        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="createDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createDto)
        {
            var result = await _createBookValidator.ValidateAsync(createDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var book = _mapper.Map<Book>(createDto);
            var createdBook = await _bookRepository.AddBookAsync(book);
            var bookDto = _mapper.Map<BookDto>(createdBook);
            return CreatedAtAction(nameof(GetBook), new { id = bookDto.Id }, bookDto); 
        }

       /// <summary>
       /// 获取图书
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
    }
}
