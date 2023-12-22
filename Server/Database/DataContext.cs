using Microsoft.EntityFrameworkCore;

namespace Eshop.Server.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //** SEED DATA **//
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Description = "Lorem ipsum sudo lomen sili imeni anto destre umeni quarto luremo lumeni",
                Title = "Book 1",
                Image = "https://dummyimage.com/600x400/ffffff/000000",
                Price = 9.99m
            }, 
            new Product
            {
                Id = 2,
                Description = "Lorem ipsum sudo lomen sili imeni anto destre umeni quarto luremo lumeni",
                Title = "Book 2",
                Image = "https://dummyimage.com/600x400/ffffff/000000",
                Price = 9.99m
            }, 
            new Product
            {
                Id = 3,
                Description = "Lorem ipsum sudo lomen sili imeni anto destre umeni quarto luremo lumeni",
                Title = "Book 3",
                Image = "https://dummyimage.com/600x400/ffffff/000000",
                Price = 9.99m
            }
       );
    }

    public DbSet<Product> Products { get; set; }
    
}