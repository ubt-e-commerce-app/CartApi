using Application.DTOs;
using Application.Interfaces;
using Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepo _cartRepo;
    private readonly ICartItemsRepo _cartItemsRepo;

    public CartService(ICartRepo cartRepo, ICartItemsRepo cartItemsRepo)
    {
        _cartRepo = cartRepo;
        _cartItemsRepo = cartItemsRepo;
    }

    public async Task<CartDto> GetCustomerCart(int customerId)
    {
        if (customerId == 0)
            return new CartDto();

        var cart = await this._cartRepo.GetCustomerCartByCustomerId(customerId);

        if (cart == null)
            return new CartDto();

        var cartItems = await this._cartItemsRepo.GetAllCartItemsByCartId(cart.Id);

        if (cartItems.Count == 0)
            return new CartDto();

        return new CartDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
            CartItems = cartItems.Select(x => new CartItemsDto { Price = x.Price, ProductId = x.ProductId }).ToList()
        };
    }

    public async Task AddToCart(AddToCartRequest request)
    {
        
    }

}
