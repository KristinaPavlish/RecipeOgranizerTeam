using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RecipeOgranizer.Dal.Models;

namespace RecipeOgranizer.Dal.Context;

public partial class RecipeOrganizerContext : DbContext
{
    public RecipeOrganizerContext()
    {
    }

    public RecipeOrganizerContext(DbContextOptions<RecipeOrganizerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cookerybook> Cookerybooks { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RecipeOrganizer;User Id=postgres;Password=user222;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cookerybook>(entity =>
        {
            entity.HasKey(e => e.Bookid).HasName("cookerybooks_pkey");

            entity.ToTable("cookerybooks");

            entity.HasIndex(e => e.Bookname, "cookerybooks_bookname_key").IsUnique();

            entity.Property(e => e.Bookid).HasColumnName("bookid");
            entity.Property(e => e.Bookname)
                .HasMaxLength(255)
                .HasColumnName("bookname");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Cookerybooks)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cookerybooks_userid_fkey");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Recipesid).HasName("recipes_pkey");

            entity.ToTable("recipes");

            entity.HasIndex(e => e.Recipename, "recipes_recipename_key").IsUnique();

            entity.Property(e => e.Recipesid).HasColumnName("recipesid");
            entity.Property(e => e.Bookid)
                .ValueGeneratedOnAdd()
                .HasColumnName("bookid");
            entity.Property(e => e.Ingredients)
                .HasColumnType("character varying")
                .HasColumnName("ingredients");
            entity.Property(e => e.Process)
                .HasColumnType("character varying")
                .HasColumnName("process");
            entity.Property(e => e.Recipename)
                .HasMaxLength(255)
                .HasColumnName("recipename");

            entity.HasOne(d => d.Book).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.Bookid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipes_bookid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(255)
                .HasColumnName("userpassword");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
