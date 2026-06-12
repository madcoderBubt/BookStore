namespace BookStore.Shared.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Details { get; set; }
        public string ImgUrl { get; set; }
        public bool Active { get; set; }
        public int Price { get; set; }
        public int PriceOffer { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
