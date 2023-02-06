using Domain.Entites;

namespace Application.Interfaces;

public interface ICartItemsRepo
{
    Task<List<CartItem>> GetAllCartItemsByCartId(int cartId);
    Task<CartItem?> GetCartItemByCartIdAndProductId(int cartId, int productId);
    Task<bool> InsertCartItems(List<CartItem> carItems);
    Task<bool> RemoveCartItem(CartItem carItem);
}