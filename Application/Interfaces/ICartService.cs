using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCustomerCart(int customerId);
    }
}