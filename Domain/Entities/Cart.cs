using System;
using System.Collections.Generic;

namespace CartApi;

public partial class Cart
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreateDateTime { get; set; }
}
