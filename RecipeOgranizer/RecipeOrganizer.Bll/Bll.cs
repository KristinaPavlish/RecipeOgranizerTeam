﻿// <copyright file="Bll.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RecipeOrganizer.Bll
{
    using System.Net;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using RecipeOgranizer.Dal.Context;
    using RecipeOgranizer.Dal.Models;
    using Serilog;

    public class BllLogger
    {
        private static readonly ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("C:\\RecipeOgranizerTeam\\RecipeOgranizer\\RecipeOrganizer.Bll\\Documents\\RecipeOrganizerLog.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        public static void LogMessage(string message)
        {
            logger.Information(message);
        }

        public static void LogException(Exception exception)
        {
            logger.Error(exception, "Exception occurred");
        }
    }

    /// <summary>
    /// This is the business logic layer class.
    /// </summary>
    public class Bll
    {
        private readonly RecipeOrganizerContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bll"/> class.
        /// </summary>
        /// <param name="context">TEXT.</param>
        public Bll(RecipeOrganizerContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Checks if the provided email is valid.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>True if the email is valid, false otherwise.</returns>
        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        /// Checks if the provided username is valid.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns>True if the username is valid, false otherwise.</returns>
        public static bool IsValidUsername(string username)
        {
            string usernamePattern = @"^[a-zA-Z0-9_]+$";
            return Regex.IsMatch(username, usernamePattern);
        }

        /// <summary>
        /// Checks if the provided password is valid.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password is valid, false otherwise.</returns>
        public static bool IsValidPassword(string password)
        {
            string passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user if found, null otherwise.</returns>
        public User? GetUserByEmail(string email)
        {
            var result = this.context.Users.FirstOrDefault(u => u.Email == email);

            if (result == null)
            {
                BllLogger.LogMessage($"Attempt to find \"{email}\". Provided email is unique!");
            }
            else
            {
                BllLogger.LogMessage($"Attempt to find \"{email}\". Provided email exists in database!");
            }

            return result;
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>True if the user is authenticated, false otherwise.</returns>
        public bool AuthenticateUser(string email, string password)
        {
            var user = this.GetUserByEmail(email);

            if (user != null && user.Userpassword == password)
            {
                BllLogger.LogMessage("User login successful for email: " + email);
                return true;
            }

            BllLogger.LogMessage("User login failed for email: " + email);
            return false;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="message">A message indicating the result of the registration.</param>
        /// <returns>True if the user is registered successfully, false otherwise.</returns>
        public bool RegisterUser(string email, string username, string password, ref string message)
        {
            if (this.UserExists(email))
            {
                message = "Email must be unique!";
                BllLogger.LogMessage($"User registration failed for email: {email}. {message}");
                return false;
            }

            if (!IsValidEmail(email))
            {
                message = "Email must be correct!";
                BllLogger.LogMessage($"User registration failed for email: {email}. {message}");
                return false;
            }

            if (!IsValidUsername(username))
            {
                message = "Username must be correct!";
                BllLogger.LogMessage($"User registration failed for email: {email}. {message}");
                return false;
            }

            if (!IsValidPassword(password))
            {
                message = "Password must be correct!";
                BllLogger.LogMessage($"User registration failed for email: {email}. {message}");
                return false;
            }

            var newUser = new User
            {
                Email = email,
                Username = username,
                Userpassword = password,
            };
            this.context.Set<User>().Add(newUser);
            this.context.SaveChanges();
            BllLogger.LogMessage($"User registration successful for email: {email}.");
            return true;
        }

        /// <summary>
        /// Gets the cookery books of a user.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>A list of cookery books of the user.</returns>
        public List<Cookerybook> GetUserCookerybooks(string email)
        {
            var user = this.GetUserByEmail(email);

            if (user != null)
            {
                var userCookerybooks = this.context.Set<Cookerybook>().Where(c => c.Userid == user.Userid).ToList();
                BllLogger.LogMessage($"Cookery books for \"{email}\" are found.");
                return userCookerybooks;
            }

            return new List<Cookerybook>();
        }

        /// <summary>
        /// Gets a cookery book by its ID.
        /// </summary>
        /// <param name="bookid">The ID of the cookery book.</param>
        /// <returns>The cookery book if found, null otherwise.</returns>
        public Cookerybook GetBookById(int bookid)
        {
            BllLogger.LogMessage($"Get information about book with id: {bookid}");
            return this.context.Set<Cookerybook>().First(b => b.Bookid == bookid);
        }

        /// <summary>
        /// Gets the recipes of a cookery book.
        /// </summary>
        /// <param name="bookid">The ID of the cookery book.</param>
        /// <returns>A list of recipes of the cookery book.</returns>
        public List<Recipe> GetRecipes(int bookid)
        {
            var book = this.GetBookById(bookid);

            if (book != null)
            {
                var recipes = this.context.Set<Recipe>().Where(r => r.Bookid == book.Bookid).ToList();
                BllLogger.LogMessage($"Get information about recipes in the book with id: {bookid}.");
                return recipes;
            }

            return new List<Recipe>();
        }

        /// <summary>
        /// Adds a new cookery book.
        /// </summary>
        /// <param name="name">The name of the cookery book.</param>
        /// <param name="description">The description of the cookery book.</param>
        public void AddBook(string name, string description)
        {
            Cookerybook newBook = new Cookerybook
            {
                Bookname = name,
                Description = description,
            };
            BllLogger.LogMessage($"New book is added.");
            this.context.Cookerybooks.Add(newBook);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes a cookery book.
        /// </summary>
        /// <param name="bookid">The ID of the cookery book.</param>
        public void DeleteBook(int bookid)
        {
            Cookerybook? bookToDelete = this.context.Cookerybooks.Find(bookid);
            BllLogger.LogMessage($"Attemp to find a book with id: {bookid}.");

            if (bookToDelete != null)
            {
                BllLogger.LogMessage($"Book with id: {bookid} is found and deleted.");
                this.context.Cookerybooks.Remove(bookToDelete);
                this.context.SaveChanges();
            }
            else
            {
                BllLogger.LogMessage($"Attemp to find a book with id: {bookid} is failed.");
            }
        }

        /// <summary>
        /// Adds a new recipe.
        /// </summary>
        /// <param name="name">The name of the recipe.</param>
        /// <param name="ingredients">The ingredients of the recipe.</param>
        /// <param name="process">The process of the recipe.</param>
        public void AddRecipe(string name, string ingredients, string process)
        {
            Recipe newRecipe = new Recipe
            {
                Recipename = name,
                Ingredients = ingredients,
                Process = process,
            };

            this.context.Recipes.Add(newRecipe);

            this.context.SaveChanges();
            BllLogger.LogMessage($"New recipe is added");
        }

        /// <summary>
        /// Deletes a recipe.
        /// </summary>
        /// <param name="recipeid">The ID of the recipe.</param>
        public void DeleteRecipe(int recipeid)
        {
            Recipe? recipeToDelete = this.context.Recipes.Find(recipeid);
            BllLogger.LogMessage($"Attemp to find a recipe with id: {recipeid}");

            if (recipeToDelete != null)
            {
                this.context.Recipes.Remove(recipeToDelete);
                this.context.SaveChanges();
                BllLogger.LogMessage($"Recipe with id: {recipeid} is found and deleted.");
            }
            else
            {
                BllLogger.LogMessage($"Attemp to find a recipe with id: {recipeid} is failed.");
            }
        }

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        private bool UserExists(string email)
        {
            return this.context.Set<User>().Any(u => u.Email == email);
        }
    }
}