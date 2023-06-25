using Microsoft.EntityFrameworkCore;

namespace JWTAPI.Services
{
    public class ServerService : IServerService
    {
        private readonly AppDbContext _context;

        public ServerService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Delete(long id)
        {
            try
            {
                var model = await GetById(id);
                if (model != null)
                {
                    _context.Server.Remove(model);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Server>> GetALl()
        {

            var model = await _context.Server.ToListAsync();
            return model;
        }

        public async Task<Server> GetById(long id)
        {
            return await _context.Server.AsNoTracking().FirstAsync(i => i.Id.Equals(id));

        }

        public async Task<List<ServiceViewModel>> GetServerByList(List<long> list)
        {
            var model = await _context.Server.Where(r => list.Contains(r.Id)).Include(i => i.Country).Include(s => s.Connected).GroupBy(i => new { i.Id, i.CountryId }).Select(c =>
                new ServiceViewModel()
                {
                    Id = c.Key.Id,
                    ServerName = c.Max(i => i.Name),
                    CountConnected = c.Count(),
                    CountryName = c.Max(i => i.Country.Name),
                    Pic = c.Max(i => i.Country.Pic),
                    ServerConfig = c.Max(i => i.Config)
                    
                }).ToListAsync();
            return model;
        }

        public async Task<Server> Insert(Server model)
        {
            try
            {
                var res = await _context.Server.AddAsync(model);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<Server> Update(Server model)
        {
            try
            {
                var res = await GetById(model.Id);
                if (res != null)
                {
                    var ress = _context.Server.Update(model);
                    await _context.SaveChangesAsync();
                    return ress.Entity;
                }
                return null;

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
