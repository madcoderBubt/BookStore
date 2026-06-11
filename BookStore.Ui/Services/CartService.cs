namespace BookStore.Ui.Services
{
    public interface ICartService
    {
        Task<string> GetCartId();
    }

    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private const string CartIdKey = "cartId";

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetCartId()
        {
            var cartId = await _localStorage.GetItemAsync<string>(CartIdKey);
            if (string.IsNullOrEmpty(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                await _localStorage.SetItemAsync(CartIdKey, cartId);
            }
            return cartId;
        }
    }
}
