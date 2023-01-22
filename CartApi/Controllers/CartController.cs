using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace CartApi.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    [Route("api/cart/{customerId}")]
    public async Task<CartDto> GetCustomerCart(int customerId)
    {
        return await this._cartService.GetCustomerCart(customerId);
    }
}
