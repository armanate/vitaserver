using JWTAPI.Core.Models;

namespace JWTAPI.Services
{
    public class CountryService: ICountryService
    {
        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
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
                    _context.Country.Remove(model);
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

        public async Task<List<Country>> GetALl()
        {
            return await _context.Country.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Country> GetById(long id)
        {
            return await _context.Country.AsNoTracking().FirstAsync(i => i.Id.Equals(id));

        }

        public async Task<Country> Insert(Country model)
        {
            try
            {
                var res = await _context.Country.AddAsync(model);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Country> Update(Country model)
        {
            try
            {
                var res = await GetById(model.Id);
                if (res != null)
                {
                    var ress = _context.Country.Update(model);
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
