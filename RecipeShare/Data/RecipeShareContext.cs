using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Models;

namespace RecipeShare.Data
{
    public class RecipeShareContext : DbContext
    {
        public RecipeShareContext (DbContextOptions<RecipeShareContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeShare.Models.Recipe> Recipe { get; set; } = default!;
    }
}
