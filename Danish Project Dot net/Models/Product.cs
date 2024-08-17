using System;
using System.Collections.Generic;

namespace Danish_Project_Dot_net.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public int? CatId { get; set; }

    public virtual Category? Cat { get; set; }
}
