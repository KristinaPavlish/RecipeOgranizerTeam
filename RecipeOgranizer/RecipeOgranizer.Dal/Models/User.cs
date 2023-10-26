using System;
using System.Collections.Generic;

namespace RecipeOgranizer.Dal.Models;

public partial class User
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Userpassword { get; set; }

    public virtual ICollection<Cookerybook> Cookerybooks { get; set; } = new List<Cookerybook>();
}
