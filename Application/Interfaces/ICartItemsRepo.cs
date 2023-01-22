using CartApi;

namespace Application.Interfaces;

public interface ICartItemsRepo
{
    Task<List<CartItem>> GetAllCartItemsByCartId(int cartId);
    Task<bool> InsertCartItems(List<CartItem> carItems);
}