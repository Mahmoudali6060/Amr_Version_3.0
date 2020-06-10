using AMR_Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Common.Interfaces
{
    public interface IAmrDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
