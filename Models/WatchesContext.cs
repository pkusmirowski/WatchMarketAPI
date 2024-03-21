using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WatchMarketAPI.Interfaces;

namespace WatchMarketAPI.Models
{
    public partial class WatchesContext : DbContext, IWatchesContext
    {
        public WatchesContext()
        {
        }

        public WatchesContext(DbContextOptions<WatchesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Watch> Watches { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }


        public async Task<List<User>> GetUsersAsync()
        {
            return await Users.ToListAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            WatchesContextConfigurator.ConfigureDbContext(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            WatchesContextConfigurator.ConfigureModel(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}