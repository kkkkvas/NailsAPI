using System;
using System.Collections.Generic;

namespace Nails.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public int? SupplierId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Supplier? Supplier { get; set; }
}
