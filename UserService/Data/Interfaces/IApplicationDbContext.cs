using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UserService.Entities;

namespace UserService.Data.Interfaces;

public interface IApplicationDbContext
{
    DbSet<UserTask> UserTasks { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
