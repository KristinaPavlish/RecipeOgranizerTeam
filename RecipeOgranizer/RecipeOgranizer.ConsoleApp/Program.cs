using Npgsql;
using System;
using NpgsqlTypes;

class Program
{
    static string GenerateUniqueUsername(NpgsqlConnection connection)
    {
        Random random = new Random();
        string baseUsername = "user_" + random.Next(1, 100);
        string username = baseUsername;
        int suffix = 1;

        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            while (true)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", username);
                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count == 0)
                {
                    return username;
                }

                suffix++;
                username = baseUsername + suffix;
            }
        }

    }

    static byte[] ReadImageBytes(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        return imageBytes;
    }

    static string GenerateUniqueEmail()
    {

        Random random = new Random();
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int emailLength = 5;

        string uniqueEmail = new string(Enumerable.Repeat(allowedChars, emailLength)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());

        return $"{uniqueEmail}@gmail.com";
    }

    static string GenerateRandomCookeryBookName(NpgsqlConnection connection)
    {
        return GenerateRandomCookeryBookNameRecursive(connection, 0);
    }

    static string GenerateRandomCookeryBookNameRecursive(NpgsqlConnection connection, int count)
    {
        Random random = new Random();
        string[] englishBookNames =
        {
            "Delicious Recipes", "Culinary Masterpieces", "Family Kitchen", "Vegetarian Dishes", "Sweets for Kids",
            "Specialty Dishes", "Desserts and Treats", "Café Recipes", "Flavorful Breakfasts", "Dinners for Two"
        };

        int randomIndex = random.Next(englishBookNames.Length);
        string randomName = englishBookNames[randomIndex];

        string testName = randomName;
        if (count > 0)
        {
            testName = $"{randomName} {count}";
        }

        string query = "SELECT COUNT(*) FROM CookeryBooks WHERE BookName = @BookName";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@BookName", testName);
            int resultCount = Convert.ToInt32(command.ExecuteScalar());

            if (resultCount == 0)
            {
                return testName;
            }
        }

        return GenerateRandomCookeryBookNameRecursive(connection, count + 1);
    }

    static string GenerateRandomCookeryBookDescription(NpgsqlConnection connection)
    {
        return GenerateRandomCookeryBookDescriptionRecursive(connection, 0);
    }

    static string GenerateRandomCookeryBookDescriptionRecursive(NpgsqlConnection connection, int count)
    {
        Random random = new Random();
        string[] sampleDescriptions =
        {
            "A collection of delicious recipes for all occasions.",
            "Explore the art of cooking with these culinary masterpieces.",
            "Discover family-friendly recipes that everyone will love.",
            "Healthy and flavorful vegetarian dishes for every meal.",
            "Indulge in sweet treats and desserts that will satisfy your cravings.",
            "Specialty dishes for those who appreciate gourmet cuisine.",
            "Dive into a world of delightful desserts and treats.",
            "Café-style recipes to enjoy at home.", "Start your day with these flavorful breakfast recipes.",
            "Romantic dinners designed for two."
        };

        int randomIndex = random.Next(sampleDescriptions.Length);
        string randomDescription = sampleDescriptions[randomIndex];

        string testDescription = randomDescription;
        if (count > 0)
        {
            testDescription = $"{randomDescription} {count}";
        }

        string query = "SELECT COUNT(*) FROM CookeryBooks WHERE Description = @Description";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Description", testDescription);
            int resultCount = Convert.ToInt32(command.ExecuteScalar());

            if (resultCount == 0)
            {
                return testDescription;
            }
        }

        return GenerateRandomCookeryBookDescriptionRecursive(connection, count + 1);
    }

    static string GenerateRandomRecipeName(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] sampleRecipeNames =
        {
            "Spaghetti Carbonara", "Chicken Alfredo", "Vegetable Stir-Fry", "Chocolate Cake", "Homemade Pizza",
            "Mango Salsa", "Garlic Shrimp Scampi", "Beef Tacos", "Caprese Salad", "Lemon Butter Chicken"
        };

        int randomIndex = random.Next(sampleRecipeNames.Length);
        string randomName = sampleRecipeNames[randomIndex];

        string query = "SELECT COUNT(*) FROM Recipes WHERE RecipeName = @RecipeName";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@RecipeName", randomName);
            int count = Convert.ToInt32(command.ExecuteScalar());

            if (count == 0)
            {
                return randomName;
            }
        }

        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int emailLength = 3;

        string randomNumb = new string(Enumerable.Repeat(allowedChars, emailLength)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());

        return $"{randomName} - {randomNumb}";
    }

    static string GenerateRandomIngredients(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] sampleIngredients =
        {
            "Tomatoes", "Onions", "Garlic", "Olive Oil", "Salt",
            "Pepper", "Basil", "Pasta", "Chicken", "Cheese",
            "Mushrooms", "Lemon", "Broccoli", "Cauliflower", "Carrots",
            "Cumin", "Coriander", "Paprika", "Thyme", "Soy Sauce",
            "Ginger", "Cilantro", "Parsley", "Rosemary", "Chili Powder",
            "Sesame Oil", "Honey", "Brown Sugar", "Lime", "Cayenne Pepper"
        };

        int ingredientCount = random.Next(3, 8);
        List<string> selectedIngredients = new List<string>();

        while (selectedIngredients.Count < ingredientCount)
        {
            int randomIndex = random.Next(sampleIngredients.Length);
            string ingredient = sampleIngredients[randomIndex];

            if (!selectedIngredients.Contains(ingredient))
            {
                selectedIngredients.Add(ingredient);
            }
        }

        string ingredientList = string.Join(", ", selectedIngredients);
        int count = 0;
        while (true)
        {
            string testIngredientList = ingredientList;
            if (count > 0)
            {
                testIngredientList = $"{ingredientList} ({count})";
            }

            string query = "SELECT COUNT(*) FROM Recipes WHERE Ingredients = @Ingredients";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Ingredients", testIngredientList);
                count = Convert.ToInt32(command.ExecuteScalar());
            }

            if (count == 0)
            {
                return testIngredientList;
            }

            count++;
        }
    }

    static string GenerateRandomProcess(NpgsqlConnection connection)
    {
        Random random = new Random();
        string[] sampleProcesses =
        {
            "Preheat the oven to 350°F.", "Chop the onions and garlic finely.",
            "Sauté the vegetables in olive oil until tender.",
            "Stir in the spices and cook for another minute.", "Add the meat and cook until browned.",
            "Stir in the chopped tomatoes and simmer.",
            "Cook the pasta according to package instructions.", "Mix the cooked pasta with the sauce.",
            "Sprinkle with cheese and bake until bubbly.",
            "Serve hot with a side of salad."
        };

        int randomIndex = random.Next(sampleProcesses.Length);
        string randomProcess = sampleProcesses[randomIndex];

        // Перевіряємо, чи опис процесу вже використовується в базі даних
        string query = "SELECT Process FROM Recipes WHERE Process = @Process";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Process", randomProcess);
            object existingProcess = command.ExecuteScalar();

            if (existingProcess == null)
            {
                return randomProcess;
            }
        }

        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int emailLength = 6;

        string randomNumb = new string(Enumerable.Repeat(allowedChars, emailLength)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());

        return $"{randomNumb}{randomProcess}";
    }
    static bool UserExists(int userID, NpgsqlConnection connection)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@UserID", userID);
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }

    static string GenerateRandomPassword()
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static void InsertUser(NpgsqlConnection connection)
    {
        string username = GenerateUniqueUsername(connection);
        string email = GenerateUniqueEmail();
        string password = GenerateRandomPassword();


        string query = "INSERT INTO Users (Username, Email, UserPassword) VALUES (@Username, @Email, @UserPassword)";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@UserPassword", password);
            command.ExecuteNonQuery();
        }
    }

    static void InsertCookeryBook(int userID, byte[] photo, NpgsqlConnection connection)
    {
        string bookName = GenerateRandomCookeryBookName(connection);
        string description = GenerateRandomCookeryBookDescription(connection);

        string query =
            "INSERT INTO CookeryBooks (UserID, BookName, Description, Photo) VALUES (@UserID, @BookName, @Description, @Photo)";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@UserID", userID);
            command.Parameters.AddWithValue("@BookName", bookName);
            command.Parameters.AddWithValue("@Description", description);

            NpgsqlParameter photoParam = new NpgsqlParameter("@Photo", NpgsqlDbType.Bytea);
            photoParam.Value = photo;
            command.Parameters.Add(photoParam);

            command.ExecuteNonQuery();
        }
    }

    static void InsertRecipe(int bookID,
        NpgsqlConnection connection)
    {
        string recipeName = GenerateRandomRecipeName(connection);
        string ingredients = GenerateRandomIngredients(connection);
        string process = GenerateRandomProcess(connection);

        // Додавання рецепта
        string query =
            "INSERT INTO Recipes (BookID, RecipeName, Ingredients, Process) VALUES (@BookID, @RecipeName, @Ingredients, @Process)";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@BookID", bookID);
            command.Parameters.AddWithValue("@RecipeName", recipeName);
            command.Parameters.AddWithValue("@Ingredients", ingredients);
            command.Parameters.AddWithValue("@Process", process);
            command.ExecuteNonQuery();
        }
    }

    static void DisplayUsers(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM Users";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Users:");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string username = reader.GetString(1);
                    string email = reader.GetString(2);
                    Console.WriteLine($"ID: {id}, Username: {username}, Email: {email}");
                }
            }
        }
    }

    static void DisplayCookeryBooks(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM CookeryBooks";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("CookeryBooks:");
                while (reader.Read())
                {
                    int bookId = reader.GetInt32(0);
                    int userId = reader.GetInt32(1);
                    string bookName = reader.GetString(2);
                    string description = reader.GetString(3);
                    Console.WriteLine(
                        $"BookID: {bookId}, UserID: {userId}, BookName: {bookName}, Description: {description}");
                }
            }
        }
    }

    static void DisplayRecipes(NpgsqlConnection connection)
    {
        string query = "SELECT * FROM Recipes";
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("Recipes:");
                while (reader.Read())
                {
                    int recipeId = reader.GetInt32(0);
                    int bookId = reader.GetInt32(1);
                    string recipeName = reader.GetString(2);
                    string ingredients = reader.GetString(3);
                    string process = reader.GetString(4);
                    Console.WriteLine(
                        $"RecipeID: {recipeId}, BookID: {bookId}, RecipeName: {recipeName}, Ingredients: {ingredients}, Process: {process}");
                }
            }
        }
    }

    static void Main()
    {
        var connectionString = "Host=localhost;Port=5432;Database=RecipeOrganizer;User Id=postgres;Password=user222;";

        string relativePath = ".\\..\\..\\..\\Photos\\book.jpg";
        var bytesPhoto = ReadImageBytes(relativePath);

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Connected to the PostgreSQL database.");

            // Заповнення користувачів
            for (int i = 1; i <= 30; i++)
            {
                InsertUser(connection);
            }

            // Заповнення книг кулінарії
            for (int i = 1; i <= 30; i++)
            {
                int userID = i;
                var photo = bytesPhoto;
    
                if (UserExists(userID, connection))
                {
                    InsertCookeryBook(userID, photo, connection);
                }
                else
                {
                    Console.WriteLine($"User with UserID {userID} does not exist.");
                }
            }


            // Заповнення рецептів
            for (int i = 1; i <= 30; i++)
            {
                int ii = i;
                int bookID = ii % 5 + 1;
                InsertRecipe(bookID, connection);
            }

            DisplayUsers(connection);
            DisplayCookeryBooks(connection);
            DisplayRecipes(connection);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}