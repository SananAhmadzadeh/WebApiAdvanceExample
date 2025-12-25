using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.OrderItemDTOs;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly WebApiAdvanceExampleDbContext _context;
        private readonly IMapper _mapper;
        public OrderItemsController(WebApiAdvanceExampleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderItemDto>>> GetAllOrderItems()
        {
            var orderItems = await _context.OrderItems
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<List<GetOrderItemDto>>(orderItems));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderItemDto>> GetOrderItemById(Guid id)
        {
            var orderItem = await _context.OrderItems
                .AsNoTracking()
                .FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItem == null)
                return NotFound($"{id} - nömrəli sifariş maddəsi tapılmadı!");

            return Ok(_mapper.Map<GetOrderItemDto>(orderItem));
        }

        [HttpPost]
        public async Task<ActionResult<GetOrderItemDto>> CreateOrderItem(CreateOrderItemDto dto)
        {
            var orderItem = _mapper.Map<OrderItem>(dto);
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetOrderItemDto>(orderItem);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(Guid id, UpdateOrderItemDto dto)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == id);
            if (orderItem == null)
                return NotFound($"{id} - nömrəli sifariş maddəsi tapılmadı!");

            _mapper.Map(dto, orderItem);
         
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == id);
            if (orderItem == null)
                return NotFound($"{id} - nömrəli sifariş maddəsi tapılmadı!");
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
