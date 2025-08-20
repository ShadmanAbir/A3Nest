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
    public DbSet<A3Nest.Domain.Entities.Task> Tasks { get; set; }
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
                contactInfo.Property(c => c.AlternatePhoneNumber).HasMaxLength(20);
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

            // Note: Role entity is separate from UserRole enum in User entity
            // This entity represents system roles with permissions, not user roles
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
                address.Property(a => a.PostalCode).HasMaxLength(20);
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
            entity.Property(e => e.Bedrooms).IsRequired();
            entity.Property(e => e.Bathrooms).IsRequired();
            entity.Property(e => e.SquareFootage).HasColumnType("decimal(10,2)");
            entity.Property(e => e.IsAvailable).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1000);
            
            // Configure Amenities as JSON
            entity.Property(e => e.Amenities)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            
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

            // Unit → LeaseApplications (1:N)
            entity.HasMany(u => u.LeaseApplications)
                .WithOne(la => la.Unit)
                .HasForeignKey(la => la.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureTenantEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DateOfBirth).IsRequired();
            entity.Property(e => e.SocialSecurityNumber).IsRequired().HasMaxLength(11);
            entity.Property(e => e.EmploymentStatus).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EmergencyContactName).HasMaxLength(200);
            entity.Property(e => e.EmergencyContactPhone).HasMaxLength(20);
            entity.Property(e => e.IsActive).IsRequired();
            
            // Configure ContactInfo value object
            entity.OwnsOne(e => e.ContactInfo, contactInfo =>
            {
                contactInfo.Property(c => c.PhoneNumber).HasMaxLength(20);
                contactInfo.Property(c => c.Email).HasMaxLength(255);
                contactInfo.Property(c => c.AlternatePhoneNumber).HasMaxLength(20);
            });

            // Configure CurrentAddress value object
            entity.OwnsOne(e => e.CurrentAddress, address =>
            {
                address.Property(a => a.Street).HasMaxLength(200);
                address.Property(a => a.City).HasMaxLength(100);
                address.Property(a => a.State).HasMaxLength(50);
                address.Property(a => a.PostalCode).HasMaxLength(20);
                address.Property(a => a.Country).HasMaxLength(100);
            });

            // Configure MonthlyIncome value object
            entity.OwnsOne(e => e.MonthlyIncome, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
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
            entity.Property(e => e.RejectionReason).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            
            // Configure DateRange value object
            entity.OwnsOne(e => e.RequestedLeaseTerms, dateRange =>
            {
                dateRange.Property(dr => dr.StartDate).HasColumnName("LeaseStartDate");
                dateRange.Property(dr => dr.EndDate).HasColumnName("LeaseEndDate");
            });
            
            // Configure Money value objects
            entity.OwnsOne(e => e.ProposedRent, money =>
            {
                money.Property(m => m.Amount).HasColumnType("decimal(18,2)");
                money.Property(m => m.Currency).HasMaxLength(3);
            });
            
            entity.OwnsOne(e => e.SecurityDepositAmount, money =>
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
                .WithMany(u => u.LeaseApplications)
                .HasForeignKey(la => la.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // LeaseApplication → ApprovedBy (N:1)
            entity.HasOne(la => la.ApprovedBy)
                .WithMany()
                .HasForeignKey(la => la.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
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
        modelBuilder.Entity<A3Nest.Domain.Entities.Task>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.Property(e => e.Priority).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            
            // Configure Tags as JSON
            entity.Property(e => e.Tags)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Task → Users (AssignedTo)
            entity.HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task → Users (AssignedBy)
            entity.HasOne(t => t.AssignedBy)
                .WithMany()
                .HasForeignKey(t => t.AssignedById)
                .OnDelete(DeleteBehavior.SetNull);

            // Task → Property (N:1)
            entity.HasOne(t => t.Property)
                .WithMany()
                .HasForeignKey(t => t.PropertyId)
                .OnDelete(DeleteBehavior.SetNull);

            // Task → Unit (N:1)
            entity.HasOne(t => t.Unit)
                .WithMany()
                .HasForeignKey(t => t.UnitId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private void ConfigureCalendarEventEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalendarEvent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.EventType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.RecurrencePattern).HasMaxLength(500);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            
            // Configure AttendeeIds as JSON
            entity.Property(e => e.AttendeeIds)
                .HasConversion(
                    v => string.Join(',', v.Select(x => x.ToString())),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                );

            // CalendarEvent → Users (OwnerId)
            entity.HasOne(ce => ce.Owner)
                .WithMany(u => u.CalendarEvents)
                .HasForeignKey(ce => ce.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // CalendarEvent → Property (N:1)
            entity.HasOne(ce => ce.Property)
                .WithMany()
                .HasForeignKey(ce => ce.PropertyId)
                .OnDelete(DeleteBehavior.SetNull);

            // CalendarEvent → Unit (N:1)
            entity.HasOne(ce => ce.Unit)
                .WithMany()
                .HasForeignKey(ce => ce.UnitId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private void ConfigureReportEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.ReportType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.Format).IsRequired().HasMaxLength(20);
            entity.Property(e => e.SchedulePattern).HasMaxLength(500);
            
            // Configure DateRange value object
            entity.OwnsOne(e => e.ReportPeriod, dateRange =>
            {
                dateRange.Property(dr => dr.StartDate).HasColumnName("ReportStartDate");
                dateRange.Property(dr => dr.EndDate).HasColumnName("ReportEndDate");
            });
            
            // Configure Parameters as JSON
            entity.Property(e => e.Parameters)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, object>()
                );

            // Configure Data as JSON
            entity.Property(e => e.Data)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, object>()
                );

            // Configure RecipientIds as JSON
            entity.Property(e => e.RecipientIds)
                .HasConversion(
                    v => string.Join(',', v.Select(x => x.ToString())),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                );

            // Report → User (GeneratedBy)
            entity.HasOne(r => r.GeneratedBy)
                .WithMany()
                .HasForeignKey(r => r.GeneratedById)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}