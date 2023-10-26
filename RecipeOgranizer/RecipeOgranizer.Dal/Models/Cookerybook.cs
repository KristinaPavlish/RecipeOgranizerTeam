using System;
using System.Collections.Generic;

namespace RecipeOgranizer.Dal.Models;

public partial class Cookerybook
{
    public int Bookid { get; set; }

    public int Userid { get; set; }

    public string? Bookname { get; set; }

    public string? Description { get; set; }

    public byte[]? Photo { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual User User { get; set; } = null!;
}
