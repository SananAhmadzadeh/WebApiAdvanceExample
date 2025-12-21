using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiAdvanceExample.Entities.Common;
using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Entities
{
    public class Product : BaseEntity
    {
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Currency { get; set; } = "AZN";
        public ProductStatus Status { get; set; }
    }
}
