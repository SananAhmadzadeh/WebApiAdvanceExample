using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebApiAdvanceExample.DAL.EFCore;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.OrderDTOs;

namespace WebApiAdvanceExample.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly WebApiAdvanceExampleDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(WebApiAdvanceExampleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrderDto>>> GetAllOrders()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .ToListAsync();

            return Ok(_mapper.Map<List<GetOrderDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDto>> GetOrderById(Guid id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null)
                return NotFound($"{id} - nömrəli sifariş tapılmadı!");

            return Ok(_mapper.Map<GetOrderDto>(order));
        }

        [HttpPost]
        public async Task<ActionResult<GetOrderDto>> CreateOrder(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<GetOrderDto>(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderDto dto)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound($"{id} - nömrəli sifariş tapılmadı!");
            _mapper.Map(dto, order);

            await _context.SaveChangesAsync();
       
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return NotFound($"{id} - nömrəli sifariş tapılmadı!");
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
