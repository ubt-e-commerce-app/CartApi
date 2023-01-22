using System;
using System.Collections.Generic;

namespace CartApi;

public partial class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public DateTime CreateDateTime { get; set; }
}
