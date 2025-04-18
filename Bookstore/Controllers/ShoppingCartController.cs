using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Bookstore.Application;
using Bookstore.Domain.Entities;
using Bookstore.Domain.IRepositories;
using AutoMapper;
using Domain.Services;
using Bookstore.Application.DTO;

namespace Bookstore.API.Controllers
{
   /// <summary>
   /// ���ﳵ
   /// </summary>
   /// <param name="shoppingCartRepository"></param>
   /// <param name="shoppingCartService"></param>
   /// <param name="mapper"></param>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
        ShoppingCartService shoppingCartService,
        IMapper mapper) : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository = shoppingCartRepository;
        private readonly ShoppingCartService _shoppingCartService = shoppingCartService;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// �����ﳵ����鼮
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int bookId, int Quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var cartItem = new ShoppingCartItem(userId, bookId, Quantity);

            // ���ͼ���������ͼ������
            var result = await _shoppingCartService.AddOrUpdateCartItemAsync(cartItem);
            var cartItemDto = _mapper.Map<ShoppingCartItemDto>(result);
            return Ok(cartItemDto);
        }

        /// <summary>
        /// ��ȡ���ﳵ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var cartItems = await _shoppingCartRepository.GetCartItemsByUserAsync(userId);
            var cartItemsDto = _mapper.Map<ShoppingCartItemDto[]>(cartItems);
            return Ok(cartItemsDto);
        }

        /// <summary>
        /// ���㹺�ﳵ���
        /// </summary>
        /// <returns></returns>
        [HttpGet("settlement")]
        public async Task<IActionResult> Settlement()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var settlement = await _shoppingCartRepository.GetCartItemsByUserAsync(userId);
            decimal total = settlement.Sum(item => item.GetSubTotal());
            var detais= await _shoppingCartRepository.GetCartItemsByUserAsync(userId);
            var detaislDto = _mapper.Map<ShoppingCartItemDto[]>(detais);

            return Ok(new { TotalPrice = total, ShoppingCartDetais = detaislDto });
        }
    }
}
