using System;
using System.Collections.Generic;

namespace Danish_Project_Dot_net.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public int? Status { get; set; }
}
