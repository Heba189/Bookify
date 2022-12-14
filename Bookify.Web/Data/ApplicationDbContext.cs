using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bookify.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; } 
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //    builder.Entity<Category>().Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()"); 
            builder.Entity<BookCategory>().HasKey(e => new { e.BookId, e.CategoryId });
            
            base.OnModelCreating(builder);
        }
    }
}