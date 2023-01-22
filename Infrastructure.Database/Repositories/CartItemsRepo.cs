using Application.Interfaces;
using CartApi;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class CartItemsRepo : ICartItemsRepo
{
    private readonly CartApiDbContext _cartApiDbContext;

    public CartItemsRepo(CartApiDbContext cartApiDbContext)
    {
        _cartApiDbContext = cartApiDbContext;
    }

    public async Task<List<CartItem>> GetAllCartItemsByCartId(int cartId)
    {
        try
        {
            return await _cartApiDbContext.CartItems.Where(x => x.CartId == cartId).ToListAsync();
        }
        catch (Exception)
        {
            return new List<CartItem>();
        }
    }

    public async Task<bool> InsertCartItems(List<CartItem> carItems)
    {
        try
        {
            await _cartApiDbContext.CartItems.AddRangeAsync(carItems);

            return await _cartApiDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
