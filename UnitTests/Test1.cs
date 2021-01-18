using AuthSystem.Areas.Identity.Data;
using AuthSystem.Controllers;
using AuthSystem.Data;
using AuthSystem.Models;
using AuthSystem.UnitTests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class Test1
    {
        //private readonly IHtmlLocalizer<ProductsController> _localizer;
        private readonly AuthDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        [Fact] // test methods without any parameters
        public async Task GetProducts()
        {
            // creates a new context
            var context = MockContext.GetContext("test1");
            // adds the mockdata to the context
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);
            
            string[] arr = new String[0];
            var response = controller.Index("", arr, arr, arr, arr);
            var result = response.Result;
            // assert verifies the condition
            // IsAssignableFrom checks if the products .. are assigned
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task GetProductWithFilter()
        {
            var context = MockContext.GetContext("test2");
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);

            string searchString = "Name";
            string[] productOffer = new String[1]{ "Kind1" };
            string[] productType = new String[1] { "Type1" };
            string[] productTrade = new String[1] { "Trade1" };
            string[] productDeliver = new String[1] { "Delivery1" };
            
            var response = controller.Index(searchString, productOffer, productType, productTrade, productDeliver);
            var result = response.Result;

            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async Task CreateProductValid()
        {
            var context = MockContext.GetContext("test3");
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);

            var product = new Product
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
            };

            await controller.Create(product, null, null, null);
            var plants = context.Products;
            var count = plants.Count();
            Assert.Equal(4, count);
        }

        [Fact]
        public async Task EditProductValid()
        {
            var context = MockContext.GetContext("test5");
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);

            var product = new Product
            {
                Name = "UpdatedName1",
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
            };

            await controller.Edit(1, product, null, null, null);

            var plant = context.Products.FirstOrDefault(p => p.Id == 1);

            Assert.Equal(plant, product);
        }
        [Fact]
        public async Task EditInvalid()
        {
            var context = MockContext.GetContext("test6");
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);

            var product = new Product
            {
                Name = "UpdatedName1",
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
            };

            
            var response = controller.Edit(10, product, null, null, null);

            var plant = context.Products.FirstOrDefault(p => p.Id == 1);
            
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task DeleteProductValid()
        {
            var context = MockContext.GetContext("test7");
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);

            //await controller.Delete(product.Id);
            await controller.DeleteConfirmed(1);
            var plants = context.Products;
            var count = plants.Count();

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task DeleteProductInvalid()
        {
            var context = MockContext.GetContext("test8");
            context.Seed();
            var controller = new ProductsController(_userManager, _signInManager, context);

            //await controller.Delete(product.Id);
            await controller.DeleteConfirmed(10);
            var plants = context.Products;
            var count = plants.Count();

            Assert.Equal(2, count);
        }
    }
}
