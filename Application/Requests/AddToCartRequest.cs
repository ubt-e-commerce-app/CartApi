namespace Application.Requests;

public class AddToCartRequest
{
    public int CustmerId { get; set; }
    public int ProductId { get; set; }
    public decimal ProductPrice { get; set; }
}
