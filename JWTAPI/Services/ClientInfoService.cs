namespace JWTAPI.Services
{
    public class ClientInfoService : IClientInfoService
    {
        private readonly AppDbContext _context;

        public ClientInfoService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ClientInfo> GetClientInfo()
        {
            var model = await _context.ClientInfo.FirstAsync();
            return model;
        }

        public async Task<bool> UpdateClientInfo(ClientInfoPostModel clientInfo)
        {
            try 
            {
                var model = await _context.ClientInfo.FirstAsync();
                model.LastVersionUrl= clientInfo.LastVersionUrl;
                model.LastVersionCode= clientInfo.LastVersionCode;
                model.IsForce = clientInfo.IsForce;
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            { 
                return false;
            }
        }
    }
}
