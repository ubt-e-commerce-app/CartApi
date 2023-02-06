namespace Application.Requests;

public class RemoveFromCartRequest
{
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
}
