using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<DbUser> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration de DbUser
        modelBuilder.Entity<DbUser>(builder =>
        {
            builder.ToTable("users");
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Username).HasColumnName("username");
            builder.Property(x => x.Mail).HasColumnName("email");
            builder.Property(x => x.Password).HasColumnName("password").HasColumnType("binary(60)");
            builder.Property(x => x.Role).HasColumnName("role");


            // builder.HasMany(user => user.ListOfTierLists)
            //     .WithOne(tierList => tierList.User)
            //     .HasForeignKey(tierList => tierList.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);
        });
    }
}