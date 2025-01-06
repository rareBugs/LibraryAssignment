using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Databases
{
    public class RealDatabase : DbContext
    {
        public RealDatabase(DbContextOptions<RealDatabase> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=DESKTOP-A3I9KLP;Database=CQRS_Project;Trused_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasKey(a => a.Id);

            modelBuilder.Entity<Book>().HasKey(b => b.Id);

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}