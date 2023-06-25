using Microsoft.AspNetCore.Http.Connections;

namespace JWTAPI.Persistence;

/// <summary>
/// EF Core already supports database seeding throught overriding "OnModelCreating", but I decided to create a separate seed class to avoid 
/// injecting IPasswordHasher into AppDbContext.
/// To understand how to use database seeding into DbContext classes, check this link: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
/// </summary>
public class DatabaseSeed
{
    public static async Task SeedAsync(AppDbContext context, IPasswordHasher passwordHasher)
    {
        context.Database.EnsureCreated();

        if (await context.Roles.AnyAsync()) return;

        var roles = new List<Role>
        {
            new Role { Name = ApplicationRole.Common.ToString() },
            new Role { Name = ApplicationRole.Administrator.ToString() }
        };

        context.Roles.AddRange(roles);
        await context.SaveChangesAsync();
        var country = new Country()
        {
            Name = "Iran",
            Pic = "sajkdkasjdla33948skldfjklsajd"
        };
        var server = new Server()
        {
            Name = "IRAN",
            Config = "20.2.20.2",
            CountryId = 1,
            Status = true
        };
        var connected = new Connected()
        {
            ServerId = 1,
            UserId = 1,
            Time = DateTime.UtcNow
        };
        var document = new Document()
        {
            UserId = 1,
            DocImage = null
        };
        var accounttype = new AccountType()
        {
            Name = "test",
            Price = "1000",
            Duration = "1 mon",
            ServerList = "1"
        };
        var payment = new Payments()
        {
            UserId = 1,
            AccountTypeId = 1,
            DocumentId = 1,
            PaymentDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddDays(1)
        };
        var users = new List<User>
        {
            new User { Email = "admin@admin.com", Password = passwordHasher.HashPassword("12345678"),Username = "test",Phone="09122221111",RecoveryAnswer="hi",RecoveryQuestion="hi" },
            new User { Email = "common@common.com", Password = passwordHasher.HashPassword("12345678") ,Username = "test1",Phone="09122221111",RecoveryAnswer="hi",RecoveryQuestion="hi"},
        };

        

        context.Users.AddRange(users);
        await context.SaveChangesAsync();
        context.Country.Add(country);
        await context.SaveChangesAsync();
        context.Server.Add(server);
        await context.SaveChangesAsync();
        context.Connected.Add(connected);
        await context.SaveChangesAsync();
        context.Document.Add(document);
        await context.SaveChangesAsync();
        context.AccountType.Add(accounttype);
        await context.SaveChangesAsync();
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
    }
}