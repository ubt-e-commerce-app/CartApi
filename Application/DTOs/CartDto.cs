namespace Application.DTOs;

public class CartDto : Base
{
    public int CustomerId { get; set; }
    public List<CartItemsDto> CartItems { get; set; }
}
