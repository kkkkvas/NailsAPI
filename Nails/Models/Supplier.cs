using System;
using System.Collections.Generic;

namespace Nails.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
