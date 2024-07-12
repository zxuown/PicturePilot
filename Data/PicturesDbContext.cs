using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PicturePilot.Data.Entities;

namespace PicturePilot.Data;

public class PicturesDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public PicturesDbContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Image> Images { get; set; } = null!;

    public virtual DbSet<Tag> Tags { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<Report> Reports { get; set; } = null!;

    public virtual DbSet<Favorite> Favorites { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public virtual DbSet<UserImageHistory> History { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Favorite>()
            .HasKey(f => new { f.UserId, f.ImageId });

        modelBuilder.Entity<UserImageHistory>()
    .HasKey(uh => new { uh.UserId, uh.ImageId });


        modelBuilder.Entity<UserImageHistory>()
            .HasOne(uh => uh.User)
            .WithMany(u => u.History)
            .HasForeignKey(uh => uh.UserId);

        modelBuilder.Entity<UserImageHistory>()
            .HasOne(uh => uh.Image)
            .WithMany()
            .HasForeignKey(uh => uh.ImageId);

        modelBuilder.Entity<User>()
    .HasMany(u => u.Images)
    .WithOne(i => i.User)
    .HasForeignKey(i => i.UserId);


    }
}
