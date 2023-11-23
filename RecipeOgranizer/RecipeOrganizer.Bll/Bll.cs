using System.Text.RegularExpressions;
using RecipeOgranizer.Dal.Context;
using RecipeOgranizer.Dal.Models;

namespace RecipeOrganizer.Bll;
public class Bll
{
    private RecipeOrganizerContext _context;

    public Bll(RecipeOrganizerContext context)
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

    public bool IsValidEmail(string email)
    {
        string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        return Regex.IsMatch(email, emailPattern);
    }

    public bool IsValidUsername(string username)
    {
        string usernamePattern = @"^[a-zA-Z0-9_]+$";
        return Regex.IsMatch(username, usernamePattern);
    }

    public bool IsValidPassword(string password)
    {
        string passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$";
        return Regex.IsMatch(password, passwordPattern);
    }

    public bool RegisterUser(string email, string username, string password, ref string message)
    {
        if (UserExists(email))
        {
            message = "Email must be unique!";
            return false;
        }

        if (!IsValidEmail(email))
        {
            message = "Email must be correct!";
            return false;
        }

        if (!IsValidUsername(username))
        {
            message = "Username must be correct!";
            return false;
        }
        if (!IsValidPassword(password))
        {
            message = "Password must be correct!";
            return false;
        }
        var newUser = new User
        {
            Email = email,
            Username = username,
            Userpassword = password
        };
        _context.Set<User>().Add(newUser);
        _context.SaveChanges();

        return true;
    }

    private bool UserExists(string email)
    {
        return _context.Set<User>().Any(u => u.Email == email);
    }
    public List<Cookerybook> GetUserCookerybooks(string email)
    {
        var user = GetUserByEmail(email);

        if (user != null)
        {
            var userCookerybooks = _context.Set<Cookerybook>().Where(c => c.Userid == user.Userid).ToList();
            return userCookerybooks;
        }

        return new List<Cookerybook>();
    }

    public Cookerybook GetBookById(int bookid)
    {
        return _context.Set<Cookerybook>().First(b => b.Bookid == bookid);
    }

    public List<Recipe> GetRecipes(int bookid)
    {
        var book = GetBookById(bookid);

        if (book != null)
        {
            var recipes = _context.Set<Recipe>().Where(r => r.Bookid == book.Bookid).ToList();
            return recipes;
        }

        return new List<Recipe>();
    }

    public void addBook(string name, string description)
    {
        Cookerybook newBook = new Cookerybook
        {
            Bookname = name,
            Description = description
        };

        _context.Cookerybooks.Add(newBook);

        _context.SaveChanges();
    }

    public void deleteBook(int bookid)
    {
        Cookerybook bookToDelete = _context.Cookerybooks.Find(bookid);

        if (bookToDelete != null)
        {
            _context.Cookerybooks.Remove(bookToDelete);
            _context.SaveChanges();
        }
    }

    public void addRecipe(string name, string ingredients, string process)
    {
        Recipe newRecipe = new Recipe
        {
            Recipename = name,
            Ingredients = ingredients,
            Process = process
        };

        _context.Recipes.Add(newRecipe);

        _context.SaveChanges();
    }

    public void deleteRecipe(int recipeid)
    {
        Recipe recipeToDelete = _context.Recipes.Find(recipeid);

        if (recipeToDelete != null)
        {
            _context.Recipes.Remove(recipeToDelete);
            _context.SaveChanges();
        }
    }
}