using Microsoft.EntityFrameworkCore;
using Northwind.Infrastructure.Persistence;

namespace Northwind.Application.UnitTests.Common;

public static class NorthwindContextFactory
{
    public static NorthwindDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NorthwindDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NorthwindDbContext(options);
        context.Database.EnsureCreated();

        var customers = CustomerFactory.Generate(3);
        context.Customers.AddRange(customers);
        context.SaveChanges();

        return context;
    }

    public static void Destroy(NorthwindDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}