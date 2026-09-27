namespace Backend.Models.Entities
{
    public class PizzaOrderEntity
    {
        public int Id { get; set; }
        public string? Size { get; set; }
        public string? Options { get; set; }
        public string? Thickness { get; set; }
    }
}
