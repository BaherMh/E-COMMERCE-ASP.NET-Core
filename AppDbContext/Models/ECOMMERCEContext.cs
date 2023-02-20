using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class ECOMMERCEContext : DbContext
    {
        public ECOMMERCEContext()
        {
        }

        public ECOMMERCEContext(DbContextOptions<ECOMMERCEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryAttr> CategoryAttr { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttr> ProductAttr { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-Q3MV6FD\\SQLEXPRESS;Database=E-COMMERCE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.RoleId1);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaimsRole)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.RoleId1Navigation)
                    .WithMany(p => p.AspNetRoleClaimsRoleId1Navigation)
                    .HasForeignKey(d => d.RoleId1);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => e.UserId1);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaimsUser)
                    .HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.AspNetUserClaimsUserId1Navigation)
                    .HasForeignKey(d => d.UserId1);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.HasIndex(e => e.UserId1);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLoginsUser)
                    .HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.AspNetUserLoginsUserId1Navigation)
                    .HasForeignKey(d => d.UserId1);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.RoleId1);

                entity.HasIndex(e => e.UserId1);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRolesRole)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.RoleId1Navigation)
                    .WithMany(p => p.AspNetUserRolesRoleId1Navigation)
                    .HasForeignKey(d => d.RoleId1);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRolesUser)
                    .HasForeignKey(d => d.UserId);

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.AspNetUserRolesUserId1Navigation)
                    .HasForeignKey(d => d.UserId1);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasIndex(e => e.UserId)
                    .HasName("AK_AspNetUserTokens_UserId")
                    .IsUnique();

                entity.HasIndex(e => e.UserId1);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AspNetUserTokensUser)
                    .HasForeignKey<AspNetUserTokens>(d => d.UserId);

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.AspNetUserTokensUserId1Navigation)
                    .HasForeignKey(d => d.UserId1);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .IsUnicode(false);

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedAt)
                    .HasColumnName("modifiedAt")
                    .HasColumnType("date");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl)
                    .HasColumnName("photoURL")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryAttr>(entity =>
            {
                entity.ToTable("Category_attr");

                entity.HasIndex(e => e.AttrId);

                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_category_attr_category_id");

                entity.HasIndex(e => e.UnitId);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AttrId).HasColumnName("attr_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .IsUnicode(false);

                entity.HasOne(d => d.Attr)
                    .WithMany(p => p.CategoryAttr)
                    .HasForeignKey(d => d.AttrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_category_attr_attribute");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryAttr)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_category_attr_category");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.CategoryAttr)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Category_attr_Unit");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_comment_produt_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_comment_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("comment")
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_AspNetUsers");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasIndex(e => e.CategoryId);

                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Image_Category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Image_Product");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_notification_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedAt)
                    .HasColumnName("modifiedAt")
                    .HasColumnType("date");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_notification_AspNetUsers");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.ShippingId)
                    .HasName("IX_order_shipping_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_order_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsPaid).HasColumnName("isPaid");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("orderDate")
                    .HasColumnType("date");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ShippingId).HasColumnName("shipping_id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ShippingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_shipping");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_AspNetUsers");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_product_category_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductPhoto).IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_category");
            });

            modelBuilder.Entity<ProductAttr>(entity =>
            {
                entity.ToTable("Product_attr");

                entity.HasIndex(e => e.AttrId)
                    .HasName("IX_product_attr_attr_id");

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_product_attr_product_id");

                entity.HasIndex(e => e.UnitId);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AttrId).HasColumnName("attr_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UnitId).HasColumnName("unit_id");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .IsUnicode(false);

                entity.HasOne(d => d.Attr)
                    .WithMany(p => p.ProductAttr)
                    .HasForeignKey(d => d.AttrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_attr_attribute");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductAttr)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_attr_product");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ProductAttr)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Product_attr_Unit");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.ToTable("Product_order");

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_product_order_product_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_order_Order1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_order_Product1");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_rating_product_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_rating_user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rating_product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rating_AspNetUsers");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
