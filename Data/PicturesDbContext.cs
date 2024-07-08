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
}
