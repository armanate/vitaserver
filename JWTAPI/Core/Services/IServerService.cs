namespace JWTAPI.Core.Services
{
    public interface IServerService
    {
        Task<List<Server>> GetALl();
        Task<Server> GetById(long id);
        Task<Server> Update(Server model);
        Task<bool> Delete(long id);
        Task<Server> Insert(Server model);
        Task<List<ServiceViewModel>> GetServerByList(List<long> list);

    }
}
