using Ecommerce.Shared.Books;
using Ecommerce.Shared.User;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<BookVariant> BookVariants { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Series> Series { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }


		private DataSeeder dataSeeder = new DataSeeder();

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookVariant>().HasKey(book => new
            {
                book.BookId,
                book.BookTypeId
            });

			modelBuilder.Entity<User>()
			    .HasOne(u => u.Role)
			    .WithMany()
			    .HasForeignKey(u => u.RoleId)
			    .IsRequired();

			//Seed data
			modelBuilder.Entity<Category>().HasData(dataSeeder.SeedCategories());
            modelBuilder.Entity<Author>().HasData(dataSeeder.SeedAuthors());
            modelBuilder.Entity<BookType>().HasData(dataSeeder.SeedBookTypes());
            modelBuilder.Entity<BookVariant>().HasData(dataSeeder.SeedBookVariants());
            modelBuilder.Entity<Series>().HasData(dataSeeder.SeedSeries());
            modelBuilder.Entity<Book>().HasData( dataSeeder.SeedBooks());
			modelBuilder.Entity<Role>().HasData(dataSeeder.SeedRoles());
			modelBuilder.Entity<Image>().HasData(dataSeeder.SeedBookImages());

		}

	}
}
