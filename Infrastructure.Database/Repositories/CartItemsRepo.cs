using Application.Interfaces;
using Domain.Entites;
using Infrastructure.Database.DbContexts;
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

    public async Task<CartItem?> GetCartItemByCartIdAndProductId(int cartId, int productId)
    {
        try
        {
            return await _cartApiDbContext.CartItems.Where(x => x.CartId == cartId && x.ProductId == productId).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return new CartItem();
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

    public async Task<bool> RemoveCartItem(CartItem carItem)
    {
        try
        {
            _cartApiDbContext.CartItems.Remove(carItem);

            return await _cartApiDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
