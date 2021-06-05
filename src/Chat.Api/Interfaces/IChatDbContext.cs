using Chat.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Chat.Api.Interfaces
{
    public interface IChatDbContext
    {
        DbSet<Message> Messages { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
