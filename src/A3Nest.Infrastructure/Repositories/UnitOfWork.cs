using Microsoft.EntityFrameworkCore.Storage;
using A3Nest.Domain.Entities;
using A3Nest.Infrastructure.Data;

namespace A3Nest.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly A3NestDbContext _context;
    private IDbContextTransaction? _transaction;

    private IRepository<User>? _users;
    private IRepository<Role>? _roles;
    private IRepository<Property>? _properties;
    private IRepository<Unit>? _units;
    private IRepository<Tenant>? _tenants;
    private IRepository<LeaseApplication>? _leaseApplications;
    private IRepository<Message>? _messages;
    private IRepository<A3Nest.Domain.Entities.Task>? _tasks;
    private IRepository<CalendarEvent>? _calendarEvents;
    private IRepository<Report>? _reports;

    public UnitOfWork(A3NestDbContext context)
    {
        _context = context;
    }

    public IRepository<User> Users => _users ??= new Repository<User>(_context);
    public IRepository<Role> Roles => _roles ??= new Repository<Role>(_context);
    public IRepository<Property> Properties => _properties ??= new Repository<Property>(_context);
    public IRepository<Unit> Units => _units ??= new Repository<Unit>(_context);
    public IRepository<Tenant> Tenants => _tenants ??= new Repository<Tenant>(_context);
    public IRepository<LeaseApplication> LeaseApplications => _leaseApplications ??= new Repository<LeaseApplication>(_context);
    public IRepository<Message> Messages => _messages ??= new Repository<Message>(_context);
    public IRepository<A3Nest.Domain.Entities.Task> Tasks => _tasks ??= new Repository<A3Nest.Domain.Entities.Task>(_context);
    public IRepository<CalendarEvent> CalendarEvents => _calendarEvents ??= new Repository<CalendarEvent>(_context);
    public IRepository<Report> Reports => _reports ??= new Repository<Report>(_context);

    public async System.Threading.Tasks.Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async System.Threading.Tasks.Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async System.Threading.Tasks.Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}