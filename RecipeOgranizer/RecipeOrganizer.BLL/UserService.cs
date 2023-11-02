namespace RecipeOrganizer.BLL;


using RecipeOgranizer.Dal.Context;
using RecipeOgranizer.Dal.Models;
using System.Collections.Generic;
using System.Linq;

public class UserService
{
    private RecipeOrganizerContext _context;

    public UserService(RecipeOrganizerContext context)
    {
        _context = context;
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public bool AuthenticateUser(string email, string password)
    {
        var user = GetUserByEmail(email);

        if (user != null && user.Userpassword == password)
        {
            return true;
        }

        return false;
    }
}