using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace News_API.Models;

public partial class NewsApiContext : DbContext
{
    public NewsApiContext()
    {
    }

    public NewsApiContext(DbContextOptions<NewsApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookMark> BookMarks { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-NEJRS4J;Database=NewsApi;Encrypt=False; Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookMark>(entity =>
        {
            entity.ToTable("BookMark");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.HasOne(d => d.News).WithMany(p => p.BookMarks)
                .HasForeignKey(d => d.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BookMark_News");

            entity.HasOne(d => d.User).WithMany(p => p.BookMarks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BookMark_Users");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.Property(e => e.Aurthor)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
