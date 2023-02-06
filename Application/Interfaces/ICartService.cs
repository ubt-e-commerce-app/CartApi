using Application.DTOs;
using Application.Requests;

namespace Application.Interfaces;

public interface ICartService
{
    Task<CartDto> GetCustomerCart(int customerId);
    Task AddToCart(AddToCartRequest request);
    Task RemoveFromCart(RemoveFromCartRequest request);
}