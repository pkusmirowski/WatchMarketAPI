using Microsoft.EntityFrameworkCore;
using WatchMarketAPI.Models;

namespace WatchMarketAPI.Interfaces
{
    public interface IWatchesContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Watch> Watches { get; set; }

        Task<int> SaveChangesAsync();

        void UpdateEntity<TEntity>(TEntity entity) where TEntity : class;

        Task<List<User>> GetUsersAsync();
    }
}
