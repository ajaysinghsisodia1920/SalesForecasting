using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesForecasting.Models;

public partial class SalesForecastingMvcContext : DbContext
{
    public SalesForecastingMvcContext()
    {
    }

    public SalesForecastingMvcContext(DbContextOptions<SalesForecastingMvcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderReturn> OrderReturns { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TL473;Database=SalesForecastingMVC;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF8416334C");

            entity.Property(e => e.OrderId)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Region)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Segment)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.ShipDate)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.ShipMode)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(512)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderReturn>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__OrderRet__F445E9887F6580F5");

            entity.Property(e => e.ReturnId).HasColumnName("ReturnID");
            entity.Property(e => e.Comments)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.OrderId)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderReturns)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderRetu__Order__2E1BDC42");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD78F9E500");

            entity.Property(e => e.ProductId)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Category)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrderId)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Profit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Sales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubCategory)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.Order).WithMany(p => p.Products)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Products__OrderI__276EDEB3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
