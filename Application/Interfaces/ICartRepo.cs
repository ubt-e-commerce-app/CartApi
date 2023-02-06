using Domain.Entites;

namespace Application.Interfaces;

public interface ICartRepo
{
    Task<Cart> GetCustomerCartByCustomerId(int customerId);
    Task<bool> CreateCart(Cart cart);
}