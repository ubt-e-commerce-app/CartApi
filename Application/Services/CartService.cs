using Application.DTOs;
using Application.Interfaces;
using Application.Requests;
using Domain.Entites;
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
        if (request.CustomerId != 0 && request.ProductId != 0 && request.ProductPrice > 0)
        {
            var currentCustomerCart = await _cartRepo.GetCustomerCartByCustomerId(request.CustomerId);

            if (currentCustomerCart == null)
            {
                var newCart = new Cart
                {
                    CustomerId = request.CustomerId,
                    CreateDateTime = DateTime.Now
                };

                var created = await _cartRepo.CreateCart(newCart);

                if (created)
                    currentCustomerCart = await _cartRepo.GetCustomerCartByCustomerId(request.CustomerId);
            }

            var cartItem = new CartItem
            {
                CartId = currentCustomerCart!.Id,
                ProductId = request.ProductId,
                Price = request.ProductPrice,
                CreateDateTime = DateTime.Now
            };

            await _cartItemsRepo.InsertCartItems(new List<CartItem> { cartItem });
        }
    }

    public async Task RemoveFromCart(RemoveFromCartRequest request)
    {
        if (request.CustomerId != 0 && request.ProductId != 0)
        {
            var custmerCart = await _cartRepo.GetCustomerCartByCustomerId(request.CustomerId);

            if (custmerCart != null)
            {
                var cartId = custmerCart.Id;

                var cartItemDoBeRemoved = await _cartItemsRepo.GetCartItemByCartIdAndProductId(cartId, request.ProductId);

                if (cartItemDoBeRemoved != null)
                    await _cartItemsRepo.RemoveCartItem(cartItemDoBeRemoved);                        
            }
        }
    }
}
