namespace JWTAPI.Services;

public class BusinessEntityIdService : IBusinessEntityId
{
    private readonly AppDbContext _context;

    public BusinessEntityIdService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Delete(string name)
    {
        try
        {
            var model = await GetById(name);
            if (model != null)
            {
                _context.BusinessEntityId.Remove(model);
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

    public async Task<List<BusinessEntityId>> GetALl()
    {
        return await _context.BusinessEntityId.OrderBy(s => s.Name).ToListAsync();

    }

    public async Task<BusinessEntityId> GetById(string name)
    {
        return await _context.BusinessEntityId.AsNoTracking().FirstAsync(i => i.Name.Equals(name));
    }

    public async Task<BusinessEntityId> Insert(BusinessEntityId model)
    {
        try
        {
            var res = await _context.BusinessEntityId.AddAsync(model);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
        catch (Exception)
        {

            return null;
        }
    }

    public async Task<BusinessEntityId> Update(BusinessEntityId model)
    {
        try
        {
            var res = await GetById(model.Name);
            if (res != null)
            {
                var ress = _context.BusinessEntityId.Update(model);
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

