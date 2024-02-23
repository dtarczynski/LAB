using Microsoft.EntityFrameworkCore;
using MyWebApplication.Api.Domain;

namespace MyWebApplication.Api.Infrastructure;

public class MyDbContext(DbContextOptions contextOptions) : DbContext(contextOptions)
{
   public DbSet<Item> Items => Set<Item>();
}