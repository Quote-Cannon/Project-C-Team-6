using Microsoft.EntityFrameworkCore;
using AuthSystem;
using System;
using System.Collections.Generic;
using System.Text;
using AuthSystem.Data;

namespace XUnitTestProject1
{
    public static class MockContext
    {
        public static AuthDbContext GetContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new AuthDbContext(options);

            // Add entities in memory
            //dbContext.Seed();

            return dbContext;
        }
    }
}
