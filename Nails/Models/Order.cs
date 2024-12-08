using System;
using System.Collections.Generic;

namespace Nails.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int? ClientId { get; set; }

    public int? ServiceId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Service? Service { get; set; }
}
