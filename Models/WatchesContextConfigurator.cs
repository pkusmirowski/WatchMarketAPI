using Microsoft.EntityFrameworkCore;

namespace WatchMarketAPI.Models
{
    public static class WatchesContextConfigurator
    {
        public static void ConfigureDbContext(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Watches;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public static void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Comments__3214EC07BD5B5E9B");

                entity.Property(e => e.CommentText).HasColumnType("text");
                entity.Property(e => e.DatePosted).HasColumnType("datetime");

                entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__Produc__52593CB8");

                entity.HasOne(d => d.User).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__UserId__534D60F1");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC0771D70E39");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Buyer).WithMany(p => p.TransactionBuyers)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Buyer__4F7CD00D");

                entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Produ__4D94879B");

                entity.HasOne(d => d.Seller).WithMany(p => p.TransactionSellers)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__Selle__4E88ABD4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0793A5203D");

                entity.HasIndex(e => e.Username, "UQ__Users__536C85E4DE9CC2D4").IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105348A138E03").IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValue("user");
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Watch>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Watches__3214EC071AAE9EDF");

                entity.Property(e => e.Brand)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Model)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });
        }
    }
}
