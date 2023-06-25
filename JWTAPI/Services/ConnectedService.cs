namespace JWTAPI.Services
{
    public class ConnectedService:IConnectedService
    {
        private readonly AppDbContext _context;

        public ConnectedService(AppDbContext context)
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
                    _context.Connected.Remove(model);
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

        public async Task<List<Connected>> GetALl()
        {
            return await _context.Connected.Include(c => c.User)
                .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Connected> GetById(long id)
        {
            return await _context.Connected.AsNoTracking().FirstAsync(i => i.Id.Equals(id));

        }

        public async Task<Connected> Insert(Connected model)
        {
            try
            {
                var res = await _context.Connected.AddAsync(model);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Connected> Update(Connected model)
        {
            try
            {
                var res = await GetById(model.Id);
                if (res != null)
                {
                    var ress = _context.Connected.Update(model);
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
