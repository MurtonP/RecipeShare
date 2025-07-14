using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeShare.Models
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public required string Title { get; set; }
        public required string Ingredients { get; set; }
        public required string Steps { get; set; }
        public int CookingTime { get; set; } // in minutes
        public required string DietaryTags { get; set; }
    }
}
