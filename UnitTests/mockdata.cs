using AuthSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.UnitTests
{
    public static class mockdata
    {
        public static void Seed(this AuthDbContext dbContext)
        {
            dbContext.Products.Add(new Models.Product
            {
                Name = "Name1",
                LatinName = "LatinName1",
                Description = "Description1",
                Kind = "Kind1",
                Type = "Type1",
                Light = "Light1",
                Water = "Water1",
                ProductDate = DateTime.Now,
                Picture = null,
                PictureTwo = null,
                PictureThree = null,
                Trade = "Trade1",
                Delivery = "Delivery1",
                Soil = "Soil1"
            }) ;

            dbContext.Products.Add(new Models.Product
            {
                Name = "Name2",
                LatinName = "LatinName2",
                Description = "Description2",
                Kind = "Kind2",
                Type = "Type2",
                Light = "Light2",
                Water = "Water2",
                ProductDate = DateTime.Now,
                Picture = null,
                PictureTwo = null,
                PictureThree = null,
                Trade = "Trade2",
                Delivery = "Delivery2",
                Soil = "Soil2"
            });

            dbContext.Products.Add(new Models.Product
            {
                Name = "Name3",
                LatinName = "LatinName3",
                Description = "Description3",
                Kind = "Kind3",
                Type = "Type3",
                Light = "Light3",
                Water = "Water3",
                ProductDate = DateTime.Now,
                Picture = null,
                PictureTwo = null,
                PictureThree = null,
                Trade = "Trade3",
                Delivery = "Delivery3",
                Soil = "Soil3"
            });

            dbContext.SaveChanges();
        }
    }
}
