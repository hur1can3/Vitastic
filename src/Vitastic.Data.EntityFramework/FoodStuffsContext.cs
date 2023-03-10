using Microsoft.EntityFrameworkCore;
using Vitastic.Domain.Data.Models;

namespace Vitastic.Data.EntityFramework;

public partial class FoodStuffsContext : DbContext
{
    public virtual DbSet<Blob> Blobs { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<CategoryRecipe> CategoryRecipes { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<Recipe> Recipes { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=FoodStuffs");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blob>(entity =>
        {
            entity.ToTable("Blob");

            _ = entity.Property(e => e.Id).ValueGeneratedNever();

            _ = entity.Property(e => e.Bytes).IsRequired();

            entity.HasOne(d => d.Image)
                .WithOne(p => p.Blob)
                .HasForeignKey<Blob>(d => d.Id)
                .HasConstraintName("FK_Blob_Image");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            _ = entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<CategoryRecipe>(entity =>
        {
            _ = entity.HasKey(e => new { e.RecipeId, e.CategoryId });

            entity.ToTable("CategoryRecipe");

            entity.HasOne(d => d.Category)
                .WithMany(p => p.CategoryRecipes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryRecipe_Category");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.CategoryRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryRecipe_Recipe");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            _ = entity.Property(e => e.CreatedBy).IsRequired();

            _ = entity.Property(e => e.ModifiedBy).IsRequired();

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.Images)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Image_Recipe");

            entity.HasOne<Recipe>()
                .WithOne(d => d.PinnedImage)
                .HasForeignKey<Recipe>(d => d.PinnedImageId)
                .HasConstraintName("FK_Recipe_PinnedImage");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("Recipe");

            _ = entity.Property(e => e.CreatedBy).IsRequired();

            _ = entity.Property(e => e.Directions).IsRequired();

            _ = entity.Property(e => e.Ingredients).IsRequired();

            _ = entity.Property(e => e.ModifiedBy).IsRequired();

            _ = entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            _ = entity.Property(e => e.FirstName).IsRequired();

            _ = entity.Property(e => e.LastName).IsRequired();

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode(false)
                .IsFixedLength(true);

            _ = entity.Property(e => e.Salt).IsRequired();

            _ = entity.Property(e => e.UserName).IsRequired();
        });
    }
}
