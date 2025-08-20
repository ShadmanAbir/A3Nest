using A3Nest.Application.DTOs;
using A3Nest.Domain.Enums;

namespace A3Nest.Presentation.Services;

public class SampleDataService : ISampleDataService
{
    public async Task<IEnumerable<PropertyDto>> GetSamplePropertiesAsync()
    {
        await Task.Delay(100); // Simulate async operation

        return new List<PropertyDto>
        {
            new PropertyDto
            {
                Id = 1,
                Name = "Sunset Apartments",
                Address = new AddressDto
                {
                    Street = "123 Main St",
                    City = "Anytown",
                    State = "ST",
                    PostalCode = "12345",
                    Country = "USA"
                },
                PropertyType = PropertyType.Residential,
                TotalUnits = 24,
                Description = "Modern apartment complex with amenities",
                PurchasePrice = new MoneyDto { Amount = 2500000, Currency = "USD" },
                CurrentValue = new MoneyDto { Amount = 3000000, Currency = "USD" },
                PurchaseDate = DateTime.Now.AddYears(-5),
                OwnerId = 1,
                CreatedAt = DateTime.Now.AddYears(-5),
                UpdatedAt = DateTime.Now
            },
            new PropertyDto
            {
                Id = 2,
                Name = "Downtown Office Complex",
                Address = new AddressDto
                {
                    Street = "456 Business Ave",
                    City = "Metro City",
                    State = "ST",
                    PostalCode = "67890",
                    Country = "USA"
                },
                PropertyType = PropertyType.Commercial,
                TotalUnits = 12,
                Description = "Prime commercial space in downtown area",
                PurchasePrice = new MoneyDto { Amount = 5000000, Currency = "USD" },
                CurrentValue = new MoneyDto { Amount = 6500000, Currency = "USD" },
                PurchaseDate = DateTime.Now.AddYears(-3),
                OwnerId = 1,
                CreatedAt = DateTime.Now.AddYears(-3),
                UpdatedAt = DateTime.Now
            },
            new PropertyDto
            {
                Id = 3,
                Name = "Riverside Condos",
                Address = new AddressDto
                {
                    Street = "789 River Rd",
                    City = "Riverside",
                    State = "ST",
                    PostalCode = "54321",
                    Country = "USA"
                },
                PropertyType = PropertyType.Mixed,
                TotalUnits = 18,
                Description = "Luxury condominiums with river views",
                PurchasePrice = new MoneyDto { Amount = 3500000, Currency = "USD" },
                CurrentValue = new MoneyDto { Amount = 4200000, Currency = "USD" },
                PurchaseDate = DateTime.Now.AddYears(-2),
                OwnerId = 1,
                CreatedAt = DateTime.Now.AddYears(-2),
                UpdatedAt = DateTime.Now
            }
        };
    }

    public async Task<IEnumerable<TenantDto>> GetSampleTenantsAsync()
    {
        await Task.Delay(100);

        return new List<TenantDto>
        {
            new TenantDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = DateTime.Now.AddYears(-30),
                SocialSecurityNumber = "XXX-XX-1234",
                ContactInfo = new ContactInfoDto
                {
                    Email = "john.smith@email.com",
                    PhoneNumber = "(555) 123-4567",
                    AlternatePhoneNumber = null
                },
                CurrentAddress = new AddressDto
                {
                    Street = "123 Main St, Apt A101",
                    City = "Anytown",
                    State = "ST",
                    PostalCode = "12345",
                    Country = "USA"
                },
                EmploymentStatus = "Full-time",
                MonthlyIncome = new MoneyDto { Amount = 5000, Currency = "USD" },
                EmergencyContactName = "Jane Smith",
                EmergencyContactPhone = "(555) 123-4568",
                IsActive = true,
                CreatedAt = DateTime.Now.AddMonths(-6),
                UpdatedAt = DateTime.Now
            },
            new TenantDto
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Johnson",
                DateOfBirth = DateTime.Now.AddYears(-28),
                SocialSecurityNumber = "XXX-XX-5678",
                ContactInfo = new ContactInfoDto
                {
                    Email = "sarah.johnson@email.com",
                    PhoneNumber = "(555) 987-6543",
                    AlternatePhoneNumber = null
                },
                CurrentAddress = new AddressDto
                {
                    Street = "123 Main St, Apt B205",
                    City = "Anytown",
                    State = "ST",
                    PostalCode = "12345",
                    Country = "USA"
                },
                EmploymentStatus = "Full-time",
                MonthlyIncome = new MoneyDto { Amount = 4500, Currency = "USD" },
                EmergencyContactName = "Bob Johnson",
                EmergencyContactPhone = "(555) 987-6544",
                IsActive = true,
                CreatedAt = DateTime.Now.AddMonths(-3),
                UpdatedAt = DateTime.Now
            },
            new TenantDto
            {
                Id = 3,
                FirstName = "Mike",
                LastName = "Davis",
                DateOfBirth = DateTime.Now.AddYears(-35),
                SocialSecurityNumber = "XXX-XX-9012",
                ContactInfo = new ContactInfoDto
                {
                    Email = "mike.davis@email.com",
                    PhoneNumber = "(555) 456-7890",
                    AlternatePhoneNumber = null
                },
                CurrentAddress = new AddressDto
                {
                    Street = "456 Business Ave, Suite 301",
                    City = "Metro City",
                    State = "ST",
                    PostalCode = "67890",
                    Country = "USA"
                },
                EmploymentStatus = "Self-employed",
                MonthlyIncome = new MoneyDto { Amount = 8000, Currency = "USD" },
                EmergencyContactName = "Lisa Davis",
                EmergencyContactPhone = "(555) 456-7891",
                IsActive = true,
                CreatedAt = DateTime.Now.AddMonths(-12),
                UpdatedAt = DateTime.Now
            }
        };
    }

    public async Task<IEnumerable<UserDto>> GetSampleUsersAsync()
    {
        await Task.Delay(100);

        return new List<UserDto>
        {
            new UserDto
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@a3nest.com",
                Role = UserRole.Admin
            },
            new UserDto
            {
                Id = 2,
                FirstName = "Property",
                LastName = "Manager",
                Email = "manager@a3nest.com",
                Role = UserRole.PropertyManager
            }
        };
    }

    public async Task<IEnumerable<MessageDto>> GetSampleMessagesAsync()
    {
        await Task.Delay(100);

        return new List<MessageDto>
        {
            new MessageDto
            {
                Id = 1,
                Subject = "Maintenance Request",
                Content = "The air conditioning in unit A101 needs repair.",
                SenderId = 1,
                ReceiverId = 2,
                MessageType = MessageType.User,
                IsRead = false,
                CreatedAt = DateTime.Now.AddHours(-2),
                UpdatedAt = DateTime.Now.AddHours(-2)
            },
            new MessageDto
            {
                Id = 2,
                Subject = "Lease Renewal",
                Content = "Your lease is up for renewal next month.",
                SenderId = 2,
                ReceiverId = 1,
                MessageType = MessageType.System,
                IsRead = true,
                ReadAt = DateTime.Now.AddHours(-1),
                CreatedAt = DateTime.Now.AddDays(-1),
                UpdatedAt = DateTime.Now.AddHours(-1)
            }
        };
    }

    public async Task<IEnumerable<TaskDto>> GetSampleTasksAsync()
    {
        await Task.Delay(100);

        return new List<TaskDto>
        {
            new TaskDto
            {
                Id = 1,
                Title = "Fix AC Unit A101",
                Description = "Repair air conditioning unit in apartment A101",
                AssignedToId = 2,
                Status = A3Nest.Domain.Enums.TaskStatus.InProgress,
                Priority = 1, // High priority
                DueDate = DateTime.Now.AddDays(2),
                CreatedAt = DateTime.Now.AddHours(-4),
                UpdatedAt = DateTime.Now.AddHours(-4)
            },
            new TaskDto
            {
                Id = 2,
                Title = "Property Inspection",
                Description = "Monthly inspection of Sunset Apartments",
                AssignedToId = 2,
                Status = A3Nest.Domain.Enums.TaskStatus.New,
                Priority = 2, // Medium priority
                DueDate = DateTime.Now.AddDays(7),
                CreatedAt = DateTime.Now.AddHours(-1),
                UpdatedAt = DateTime.Now.AddHours(-1)
            }
        };
    }

    public async Task<IEnumerable<CalendarEventDto>> GetSampleCalendarEventsAsync()
    {
        await Task.Delay(100);

        return new List<CalendarEventDto>
        {
            new CalendarEventDto
            {
                Id = 1,
                Title = "Property Showing",
                Description = "Show unit B203 to potential tenant",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(1).AddHours(1),
                OwnerId = 2,
                Location = "Sunset Apartments - Unit B203"
            },
            new CalendarEventDto
            {
                Id = 2,
                Title = "Maintenance Appointment",
                Description = "AC repair in unit A101",
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(2).AddHours(2),
                OwnerId = 2,
                Location = "Sunset Apartments - Unit A101"
            }
        };
    }

    public async Task<IEnumerable<ReportDto>> GetSampleReportsAsync()
    {
        await Task.Delay(100);

        return new List<ReportDto>
        {
            new ReportDto
            {
                Id = 1,
                Name = "Monthly Revenue Report",
                Description = "Revenue summary for current month",
                ReportType = "Financial",
                GeneratedAt = DateTime.Now.AddDays(-1),
                GeneratedById = 2,
                ReportPeriod = new DateRangeDto
                {
                    StartDate = DateTime.Now.AddDays(-30),
                    EndDate = DateTime.Now
                },
                Format = "PDF",
                CreatedAt = DateTime.Now.AddDays(-1),
                UpdatedAt = DateTime.Now.AddDays(-1)
            },
            new ReportDto
            {
                Id = 2,
                Name = "Occupancy Report",
                Description = "Current occupancy rates across all properties",
                ReportType = "Occupancy",
                GeneratedAt = DateTime.Now.AddDays(-7),
                GeneratedById = 2,
                ReportPeriod = new DateRangeDto
                {
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now
                },
                Format = "PDF",
                CreatedAt = DateTime.Now.AddDays(-7),
                UpdatedAt = DateTime.Now.AddDays(-7)
            }
        };
    }

    public async Task<OwnerFinancialSummaryDto> GetSampleOwnerFinancialSummaryAsync()
    {
        await Task.Delay(100);

        return new OwnerFinancialSummaryDto
        {
            Period = new DateRangeDto
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now
            },
            TotalIncome = new MoneyDto { Amount = 45000.00m, Currency = "USD" },
            TotalExpenses = new MoneyDto { Amount = 12000.00m, Currency = "USD" },
            NetIncome = new MoneyDto { Amount = 33000.00m, Currency = "USD" },
            RentCollected = new MoneyDto { Amount = 42000.00m, Currency = "USD" },
            MaintenanceCosts = new MoneyDto { Amount = 5000.00m, Currency = "USD" },
            PropertyTaxes = new MoneyDto { Amount = 4000.00m, Currency = "USD" },
            Insurance = new MoneyDto { Amount = 2000.00m, Currency = "USD" },
            OtherExpenses = new MoneyDto { Amount = 1000.00m, Currency = "USD" },
            PropertyBreakdown = new List<PropertyFinancialDto>()
        };
    }
}