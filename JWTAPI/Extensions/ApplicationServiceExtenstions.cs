using System.Text.Json.Serialization;

namespace JWTAPI.Extensions;
public static class ApplicationServiceExtenstions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddControllers();

        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.AddDbContext<AppDbContext>(options =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("ConnectionDatabase");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<ITokenHandler, Security.Tokens.TokenHandler>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ICountryService, CountryService>();
        
        services.AddScoped<IAccountTypeService, AccountTypeService>();

        services.AddScoped<IConnectedService, ConnectedService>();

        services.AddScoped<IDocumentService, DocumentService>();
        
        services.AddScoped<IPaymentService, PaymentService>();
        
        services.AddScoped<IServerService, ServerService>(); 
        
        services.AddScoped<IBusinessEntityId, BusinessEntityIdService>();
        
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IClientInfoService, ClientInfoService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
