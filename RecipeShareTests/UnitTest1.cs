using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;
using RecipeShare;
using RecipeShare.Controllers;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShareTests
{
    public class RecipeShareTestsClass
    {
        private readonly RecipeShareContext _context;

        public RecipeShareTestsClass()
        {
            var options = new DbContextOptionsBuilder<RecipeShareContext>()
                .UseInMemoryDatabase(databaseName: "RecipeShare") 
                .Options;
            _context = new RecipeShareContext(options);

            // Creamy Garlic Pasta
            _context.Recipe.Add(new Recipe
            {
                Title = "Creamy Garlic Pasta",
                Ingredients = "200g spaghetti|2 tbsp butter|3 cloves garlic (minced)|1 cup milk|1/2 cup grated Parmesan|Salt and pepper to taste",
                Steps = "Cook spaghetti according to package instructions; drain and set aside.|In a saucepan, melt butter over medium heat.|Add garlic and sauté for 1–2 minutes until fragrant.|Stir in milk and bring to a simmer.|Add Parmesan and stir until melted and smooth.|Toss pasta into the sauce and mix well.|Season with salt and pepper. Serve warm.",
                CookingTime = 20,
                DietaryTags = "Vegetarian, Nut-Free"
            });
            _context.SaveChanges();

            // Creamy Tomato Pasta
            _context.Recipe.Add(new Recipe
            {
                Title = "Creamy Tomato Pasta",
                Ingredients = "250g pasta (penne or fusilli)|1 tbsp olive oil|1 garlic clove (minced)|1 can (400g) chopped tomatoes|2 tbsp cream or full-cream plain yogurt|Salt & pepper to taste|1 tsp dried basil|Grated Parmesan (optional)",
                Steps = "Cook pasta according to package instructions, then drain.|In a pan, heat olive oil and sauté garlic for 1 minute.|Add chopped tomatoes, salt, pepper, and basil. Simmer for 10 minutes.|Stir in cream and simmer for another 2–3 minutes.|Add cooked pasta to the sauce, mix well.|Serve with grated Parmesan if desired.",
                CookingTime = 25,
                DietaryTags = "Vegetarian"
            });
            _context.SaveChanges();

            // Quick Veggie Stir-Fry
            _context.Recipe.Add(new Recipe
            {
                Title = "Quick Veggie Stir-Fry",
                Ingredients = "1 tbsp vegetable oil|1 cup broccoli florets|1 bell pepper (sliced)|1 carrot (sliced thinly)|2 tbsp soy sauce|1 tsp sesame oil|1 tsp grated ginger|1 tsp cornstarch mixed with 2 tbsp water",
                Steps = "Heat vegetable oil in a large pan or wok over high heat.|Add broccoli, pepper, and carrot. Stir-fry for 4–5 minutes.|Add soy sauce, sesame oil, and ginger. Stir for another 2 minutes.|Pour in cornstarch mixture and stir until sauce thickens (1–2 mins).|Remove from heat and serve with rice or noodles.",
                CookingTime = 15,
                DietaryTags = "Vegan, Dairy-Free, Nut-Free"
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetFilter_Valid_201()
        {
            var controller = new RecipeShareController(_context);

            Recipe? expected = (await controller.GetFilter("Vegan")).Value?.FirstOrDefault();

            Recipe? actual = (await controller.GetFilter("Vegan")).Value?.FirstOrDefault();

            Assert.Equal(expected?.ID, actual?.ID); 
        }

        [Fact]
        public async Task GetFilter_Different_400()
        {
            var controller = new RecipeShareController(_context);

            //Recipe? expected = (await controller.GetFilter("Vegan")).Value?.FirstOrDefault();
            Recipe? expected = new Recipe
            {
                ID = 1,
                Title = "Vegan Salad",
                Ingredients = "Lettuce, Tomato, Cucumber",
                Steps = "Chop ingredients and mix.",
                CookingTime = 10,
                DietaryTags = "Vegan"
            };

            Recipe? actual = (await controller.GetFilter("Vegan")).Value?.FirstOrDefault();

            Assert.NotEqual(expected?.ID, actual?.ID);
        }

        [Fact]
        public async Task GetFilter_InValid_201()
        {
            var controller = new RecipeShareController(_context);

            Recipe? expected = (await controller.GetFilter("Presbytharian")).Value?.FirstOrDefault();

            Recipe? actual = (await controller.GetFilter("Vegan")).Value?.FirstOrDefault();

            Assert.NotEqual(expected?.ID, actual?.ID);
        }
    }
}