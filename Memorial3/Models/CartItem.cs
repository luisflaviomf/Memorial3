namespace Memorial3.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Memorial Memorial { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; } 
    }
}
