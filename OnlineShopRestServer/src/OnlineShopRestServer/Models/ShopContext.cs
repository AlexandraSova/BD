using Microsoft.EntityFrameworkCore;

namespace OnlineShopRestServer.Models
{
    public class ShopContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;User Id=postgres;Password=666;Port=5432;Database=shop;");
        }
        public DbSet<address> address { get; set; }
        public DbSet<client> client { get; set; }
        public DbSet<item> item { get; set; }
        public DbSet<manager> manager { get; set; }
        public DbSet<order> order { get; set; }
        public DbSet<product> product { get; set; }
        public DbSet<Models.type> type { get; set; }
    }
}
