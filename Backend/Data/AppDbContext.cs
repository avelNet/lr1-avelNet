using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ColaOrderEntity> ColaOrders { get; set; }
        public DbSet<PizzaOrderEntity> PizzaOrders { get; set; }
        public DbSet<DigitalListItemEntity> DigitalListItems { get; set; }
        public DbSet<MailEntity> Mails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
