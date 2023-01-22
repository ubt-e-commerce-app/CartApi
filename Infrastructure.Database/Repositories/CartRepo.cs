using Application.Interfaces;
using CartApi;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class CartRepo : ICartRepo
{
    private readonly CartApiDbContext _cartApiDbContext;

    public CartRepo(CartApiDbContext cartApiDbContext)
    {
        _cartApiDbContext = cartApiDbContext;
    }

    public async Task<Cart> GetCustomerCartByCustomerId(int customerId)
    {
        try
        {
            return await _cartApiDbContext.Carts.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return new Cart();
        }
    }

    public async Task<bool> InsertOrder(Cart cart)
    {
        try
        {
            await _cartApiDbContext.Carts.AddAsync(cart);

            return await _cartApiDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
