using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlantWebsite.Models;

namespace PlantWebsite.Data
{
    public class WebsiteDbContext : DbContext
    {
        // A database context class is needed to coordinate EF Core functionality (CRUD) for the Product model.
        // The dbcontext is derived from Microsoft.EntityFrameworkCore.DbContext and specifies the intities to include in de datamaodel.
   
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
            : base(options) { }

        public DbSet<Product> Product { get; set; }


    }
}
