namespace Memorial3.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public int MemorialId { get; set; }
        public Order Order { get; set; }
        public Memorial Memorial { get; set; }
    }
}
