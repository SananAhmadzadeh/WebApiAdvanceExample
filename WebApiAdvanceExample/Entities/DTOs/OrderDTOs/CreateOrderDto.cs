namespace WebApiAdvanceExample.Entities.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
