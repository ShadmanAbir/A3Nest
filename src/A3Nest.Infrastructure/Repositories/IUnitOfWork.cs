using A3Nest.Domain.Entities;

namespace A3Nest.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<Property> Properties { get; }
    IRepository<Unit> Units { get; }
    IRepository<Tenant> Tenants { get; }
    IRepository<LeaseApplication> LeaseApplications { get; }
    IRepository<Message> Messages { get; }
    IRepository<A3Nest.Domain.Entities.Task> Tasks { get; }
    IRepository<CalendarEvent> CalendarEvents { get; }
    IRepository<Report> Reports { get; }

    System.Threading.Tasks.Task<int> SaveChangesAsync();
    System.Threading.Tasks.Task BeginTransactionAsync();
    System.Threading.Tasks.Task CommitTransactionAsync();
    System.Threading.Tasks.Task RollbackTransactionAsync();
}