namespace JWTAPI.Core.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetALl();
        Task<Country> GetById(long id);
        Task<Country> Update(Country model);
        Task<bool> Delete(long id);
        Task<Country> Insert(Country model);
    }
}
