using Microsoft.EntityFrameworkCore;
using A3Nest.Domain.Entities;
using A3Nest.Domain.ValueObjects;

namespace A3Nest.Infrastructure.Data;

public class A3NestDbContext : DbContext
{
    public A3NestDbContext(DbContextOptions<A3NestDbContext> options) : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<LeaseApplication> LeaseApplications { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<CalendarEvent> CalendarEvents { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure entity relationships and constraints
        ConfigureUserEntity(modelBuilder);
        ConfigureRoleEntity(modelBuilder);
        ConfigurePropertyEntity(modelBuilder);
        ConfigureUnitEntity(modelBuilder);
        ConfigureTenantEntity(modelBuilder);
        ConfigureLeaseApplicationEntity(modelBuilder);
        ConfigureMessageEntity(modelBuilder);
        ConfigureTaskEntity(modelBuilder);
        ConfigureCalendarEventEntity(modelBuilder);
        ConfigureReportEntity(modelBuilder);
        
        // Configure Value Objects
        ConfigureValueObjects(modelBuilder);
    }

    private void ConfigureUserEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).HasConversion<string>();
            
            // Configure ContactInfo value object
            entity.OwnsOne(e => e.ContactInfo, contactInfo =>
            {
                contactInfo.Property(c => c.PhoneNumber).HasMaxLength(20);
                contactInfo.Property(c => c.Email).HasMaxLength(255);
                contactInfo.Property(c => c.AlternateEmail).HasMaxLength(255);
            });
        });
    }

    private void ConfigureRoleEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            
            // Configure Permissions as JSON
            entity.Property(e => e.Permissions)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Role → Users (1:N)
            entity.HasMany(r => r.Users)
                .WithOne()
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigurePropertyEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.PropertyType).HasConversion<string>();
            
            // Configure Address value object
            entity.OwnsOne(e => e.Address, address =>
            {
                address.Property(a => a.Street).HasMaxLength(200);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.State).HasMaxLength(50);
                address.Property(a => a.ZipCode).HasMaxLength(20);
                address.Property(a => a.Country).HasMaxLength(100);
            });
            
            // Configure Money value objects
            entity.OwnsOne(e => e.PurchasePrice, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });
            
            entity.OwnsOne(e => e.CurrentValue, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });

            // Property → Units (1:N)
            entity.HasMany(p => p.Units)
                .WithOne(u => u.Property)
                .HasForeignKey(u => u.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Property → Owner (N:1)
            entity.HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Property → PropertyManager (N:1)
            entity.HasOne(p => p.PropertyManager)
                .WithMany()
                .HasForeignKey(p => p.PropertyManagerId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }   
 private void ConfigureUnitEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.UnitType).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
            
            // Configure Money value objects
            entity.OwnsOne(e => e.RentAmount, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });
            
            entity.OwnsOne(e => e.SecurityDeposit, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });
        });
    }

    private void ConfigureTenantEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Status).HasConversion<string>();
            
            // Configure ContactInfo value object
            entity.OwnsOne(e => e.ContactInfo, contactInfo =>
            {
                contactInfo.Property(c => c.PhoneNumber).HasMaxLength(20);
                contactInfo.Property(c => c.Email).HasMaxLength(255);
                contactInfo.Property(c => c.AlternateEmail).HasMaxLength(255);
            });

            // Tenant → LeaseApplications (1:N)
            entity.HasMany(t => t.LeaseApplications)
                .WithOne(la => la.Tenant)
                .HasForeignKey(la => la.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureLeaseApplicationEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaseApplication>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            
            // Configure DateRange value object
            entity.OwnsOne(e => e.LeaseTerms, dateRange =>
            {
                dateRange.Property(dr => dr.StartDate).HasColumnName("LeaseStartDate");
                dateRange.Property(dr => dr.EndDate).HasColumnName("LeaseEndDate");
            });
            
            // Configure Money value objects
            entity.OwnsOne(e => e.MonthlyRent, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });
            
            entity.OwnsOne(e => e.SecurityDeposit, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });

            // LeaseApplication → Property (N:1)
            entity.HasOne(la => la.Property)
                .WithMany(p => p.LeaseApplications)
                .HasForeignKey(la => la.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            // LeaseApplication → Unit (N:1)
            entity.HasOne(la => la.Unit)
                .WithMany()
                .HasForeignKey(la => la.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureMessageEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.MessageType).HasConversion<string>();
            
            // Configure Attachments as JSON
            entity.Property(e => e.Attachments)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Message → Users (SenderId, ReceiverId)
            entity.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message → ParentMessage (self-referencing)
            entity.HasOne(m => m.ParentMessage)
                .WithMany(m => m.Replies)
                .HasForeignKey(m => m.ParentMessageId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureTaskEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.Property(e => e.Priority).HasConversion<string>();
            
            // Configure DateRange value object
            entity.OwnsOne(e => e.DateRange, dateRange =>
            {
                dateRange.Property(dr => dr.StartDate).HasColumnName("TaskStartDate");
                dateRange.Property(dr => dr.EndDate).HasColumnName("TaskEndDate");
            });

            // Task → Users (AssignedTo)
            entity.HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Task → Property (N:1)
            entity.HasOne(t => t.Property)
                .WithMany()
                .HasForeignKey(t => t.PropertyId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }