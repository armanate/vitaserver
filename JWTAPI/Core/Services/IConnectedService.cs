namespace JWTAPI.Core.Services
{
    public interface IConnectedService
    {
        Task<List<Connected>> GetALl();
        Task<Connected> GetById(long id);
        Task<Connected> Update(Connected model);
        Task<bool> Delete(long id);
        Task<Connected> Insert(Connected model);
    }
}
