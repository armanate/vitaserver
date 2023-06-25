namespace JWTAPI.Mapping;
public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<UserCredentialsResource, User>();
        CreateMap<AccounTypePost, AccountType>();
        CreateMap<ConnectedPost, Connected>();
        CreateMap<CountryPost, Country>();
        CreateMap<DocumentPost, Document>();
        CreateMap<PaymentsPost, Payments>();
        CreateMap<ServerPost, Server>();
    }
}