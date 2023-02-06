using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.DbContexts;

public partial class CartApiDbContext : DbContext
{
    public CartApiDbContext()
    {
    }

    public CartApiDbContext(DbContextOptions<CartApiDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=BCKLAP-BCHRML3;Database=CartApiDb;User Id=BUCKAROO\\\\\\\\j.veselaj;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC07C4442D63");

            entity.ToTable("Cart");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3214EC07C9B024C6");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
