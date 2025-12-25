using WebApiAdvanceExample.Entities.Common;

namespace WebApiAdvanceExample.Entities
{
    public class Order : BaseEntity 
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }    
    }
}
