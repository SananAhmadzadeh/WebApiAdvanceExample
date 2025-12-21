using System.ComponentModel.DataAnnotations;
using WebApiAdvanceExample.Entities.Common;
using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Entities
{
    public class Category : BaseEntity  
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryStatus Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
