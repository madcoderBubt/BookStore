namespace BookStore.Shared.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string ImgUrl { get; set; }
    }

    public class ShoppingCartDto
    {
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public int Total { get; set; }
    }
}
