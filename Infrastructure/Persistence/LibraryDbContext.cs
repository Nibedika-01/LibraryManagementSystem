using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence;

public class LibraryDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Author> Authors { get; set; }
	public DbSet<Book> Books { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<OTP> OTPs { get; set; }

	public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Book)
            .WithMany(b => b.Issues)
            .HasForeignKey(i => i.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Issue>()
            .HasOne(i => i.Student)
            .WithMany(s => s.Issues)
            .HasForeignKey(i => i.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OTP>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.OtpCode).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Purpose).HasMaxLength(50);
            entity.HasIndex(e => new { e.Email, e.IsUsed });
        });

        modelBuilder.Entity<Student>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);
        modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
        modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Issue>().HasQueryFilter(i => !i.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}
