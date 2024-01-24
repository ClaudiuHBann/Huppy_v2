using Microsoft.EntityFrameworkCore;

using Shared.Models;

namespace Huppy.API.Models
{
public partial class HuppyContext : DbContext
{
    private readonly string _connectionString = "";

    public HuppyContext(IConfiguration config)
    {
#if DEBUG
        _connectionString = config["ConnectionStringHuppyTest"] ?? "";
#else
        _connectionString = config["ConnectionStringHuppy"] ?? "";
#endif
    }

    public HuppyContext(DbContextOptions<HuppyContext> options) : base(options)
    {
    }

    public virtual DbSet<AppEntity> Apps { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }
    public virtual DbSet<LinkEntity> Links { get; set; }
    public virtual DbSet<PackageEntity> Packages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("Arch", ["32", "64"])
            .HasPostgresEnum("Format", ["EXE", "DMG"])
            .HasPostgresEnum("OS", ["Windows", "Mac"]);

        modelBuilder.Entity<AppEntity>(entity =>
                                       {
                                           entity.HasKey(e => e.Id).HasName("app_id_pk");

                                           entity.ToTable("app", tb => tb.HasComment("Apps"));

                                           entity.HasIndex(e => e.Name, "unique_app_name").IsUnique();

                                           entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                                           entity.Property(e => e.Category).HasColumnName("category");
                                           entity.Property(e => e.Image).HasColumnName("image");
                                           entity.Property(e => e.Name).HasMaxLength(128).HasColumnName("name");
                                           entity.Property(e => e.Proposed).HasColumnName("proposed");

                                           entity.HasOne(d => d.CategoryNavigation)
                                               .WithMany(p => p.Apps)
                                               .HasForeignKey(d => d.Category)
                                               .OnDelete(DeleteBehavior.ClientSetNull)
                                               .HasConstraintName("category_fk");
                                       });

        modelBuilder.Entity<CategoryEntity>(
            entity =>
            {
                entity.HasKey(e => e.Id).HasName("category_id_pk");

                entity.ToTable("category", tb => tb.HasComment("Apps categories"));

                entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                entity.Property(e => e.Count).HasDefaultValue(0).HasColumnName("count");
                entity.Property(e => e.Description).HasMaxLength(256).HasColumnName("description");
                entity.Property(e => e.Name).HasMaxLength(64).HasColumnName("name");
            });

        modelBuilder.Entity<LinkEntity>(entity =>
                                        {
                                            entity.HasKey(e => e.Id).HasName("link_id_pk");

                                            entity.ToTable("link", tb => tb.HasComment("App's Links"));

                                            entity.HasIndex(e => e.Url, "unique_link_url").IsUnique();

                                            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                                            entity.Property(e => e.App).HasColumnName("app");
                                            entity.Property(e => e.Url).HasMaxLength(1024).HasColumnName("url");

                                            entity.HasOne(d => d.AppNavigation)
                                                .WithMany(p => p.Links)
                                                .HasForeignKey(d => d.App)
                                                .OnDelete(DeleteBehavior.ClientSetNull)
                                                .HasConstraintName("link_app_fk");
                                        });

        modelBuilder.Entity<PackageEntity>(entity =>
                                           {
                                               entity.HasKey(e => e.Id).HasName("package_id_pk");

                                               entity.ToTable("package");

                                               entity.HasIndex(e => e.Name, "unique_package_name").IsUnique();

                                               entity.Property(e => e.Id).HasColumnName("id");
                                               entity.Property(e => e.Name).HasMaxLength(36).HasColumnName("name");
                                               entity.Property(e => e.Apps).HasColumnName("apps");
                                           });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
}
