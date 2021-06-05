using Chat.Api.Models;
using Chat.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Data
{
    public class ChatDbContext: DbContext, IChatDbContext
    {
        public DbSet<Message> Messages { get; private set; }
        public ChatDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatDbContext).Assembly);
        }
        
    }
}
