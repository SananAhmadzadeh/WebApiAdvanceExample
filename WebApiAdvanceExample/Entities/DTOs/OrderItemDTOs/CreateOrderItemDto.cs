namespace WebApiAdvanceExample.Entities.DTOs.OrderItemDTOs
{
    public class CreateOrderItemDto
    {
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
