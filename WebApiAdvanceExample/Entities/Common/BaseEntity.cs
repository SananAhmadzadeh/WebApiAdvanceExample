namespace WebApiAdvanceExample.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        //GetById zamanı istifadə etmək üçün
        public int Code { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
