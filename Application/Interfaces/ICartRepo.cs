using CartApi;

namespace Application.Interfaces;

public interface ICartRepo
{
    Task<Cart> GetCustomerCartByCustomerId(int customerId);
    Task<bool> InsertOrder(Cart cart);
}