using Microsoft.EntityFrameworkCore;

namespace Huppy.Models;

public partial class HuppyContext : DbContext
{
    public HuppyContext()
    {
    }

    public HuppyContext(DbContextOptions<HuppyContext> options) : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Link> Links { get; set; }
    public virtual DbSet<Package> Packages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=huppy;Password=sarmale1;Host=162.55.32.18;Port=5432;Database=Huppy");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("Arch", ["32", "64"])
            .HasPostgresEnum("Format", ["EXE", "DMG"])
            .HasPostgresEnum("OS", ["Windows", "Mac"]);

        modelBuilder.Entity<App>(entity =>
                                 {
                                     entity.HasKey(e => e.Id).HasName("app_id_pk");

                                     entity.ToTable("app", tb => tb.HasComment("Apps"));

                                     entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                                     entity.Property(e => e.Category).HasColumnName("category");
                                     entity.Property(e => e.Image).HasMaxLength(255).HasColumnName("image");
                                     entity.Property(e => e.Name).HasMaxLength(128).HasColumnName("name");
                                     entity.Property(e => e.Proposed).HasColumnName("proposed");

                                     entity.HasOne(d => d.CategoryNavigation)
                                         .WithMany(p => p.Apps)
                                         .HasForeignKey(d => d.Category)
                                         .OnDelete(DeleteBehavior.ClientSetNull)
                                         .HasConstraintName("category_fk");
                                 });

        modelBuilder.Entity<Category>(
            entity =>
            {
                entity.HasKey(e => e.Id).HasName("category_id_pk");

                entity.ToTable("category", tb => tb.HasComment("Apps categories"));

                entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                entity.Property(e => e.Count).HasDefaultValueSql("0").HasColumnName("count");
                entity.Property(e => e.Description).HasMaxLength(256).HasColumnName("description");
                entity.Property(e => e.Name).HasMaxLength(64).HasColumnName("name");
            });

        modelBuilder.Entity<Link>(entity =>
                                  {
                                      entity.HasKey(e => e.Id).HasName("link_id_pk");

                                      entity.ToTable("link", tb => tb.HasComment("App's Links"));

                                      entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                                      entity.Property(e => e.App).HasColumnName("app");
                                      entity.Property(e => e.Url).HasMaxLength(1024).HasColumnName("url");

                                      entity.HasOne(d => d.AppNavigation)
                                          .WithMany(p => p.Links)
                                          .HasForeignKey(d => d.App)
                                          .OnDelete(DeleteBehavior.ClientSetNull)
                                          .HasConstraintName("link_app_fk");
                                  });

        modelBuilder.Entity<Package>(entity =>
                                     {
                                         entity.HasKey(e => e.Id).HasName("package_id_pk");

                                         entity.ToTable("package");

                                         entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
                                         entity.Property(e => e.Apps).HasColumnName("apps");
                                         entity.Property(e => e.Name).HasMaxLength(32).HasColumnName("name");
                                     });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
