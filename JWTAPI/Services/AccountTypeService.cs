namespace JWTAPI.Services
{
    public class AccountTypeService:IAccountTypeService
    {
        private readonly AppDbContext _context;

        public AccountTypeService(AppDbContext context)
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
                    _context.AccountType.Remove(model);
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

        public async Task<List<AccountType>> GetALl()
        {
            var m =  await _context.AccountType.OrderBy(s => s.Id).ToListAsync();
            return m;
        }

        public async Task<AccountType> GetById(long id)
        {
            return await _context.AccountType.AsNoTracking().FirstAsync(i=>i.Id.Equals(id));

        }

        public async Task<AccountType> Insert(AccountType model)
        {
            try
            {
                var res = await _context.AccountType.AddAsync(model);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<AccountType> Update(AccountType model)
        {
            try
            {
                var res = await GetById(model.Id);
                if (res != null)
                {
                    var ress = _context.AccountType.Update(model);
                    await _context.SaveChangesAsync();
                    return ress.Entity;
                }
                return null;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
