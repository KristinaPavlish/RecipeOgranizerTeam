using System;
using System.Collections.Generic;

namespace RecipeOgranizer.Dal.Models;

public partial class Recipe
{
    public int Recipesid { get; set; }

    public int Bookid { get; set; }

    public string? Recipename { get; set; }

    public string? Ingredients { get; set; }

    public string? Process { get; set; }

    public virtual Cookerybook Book { get; set; } = null!;
}
