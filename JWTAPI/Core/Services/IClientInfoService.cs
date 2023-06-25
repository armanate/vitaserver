namespace JWTAPI.Core.Services
{
    public interface IClientInfoService
    {
        Task<ClientInfo> GetClientInfo();

        Task<bool> UpdateClientInfo(ClientInfoPostModel clientInfo);
    }
}
