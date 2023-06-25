using JWTAPI.Core.Models;

namespace JWTAPI.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
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
                    _context.Payments.Remove(model);
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

        public async Task<List<Payments>> GetALl()
        {
            return await _context.Payments.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Payments> GetById(long id)
        {
            return await _context.Payments.AsNoTracking().FirstAsync(i => i.Id.Equals(id));

        }

        public async Task<List<Payments>> GetPaymetByUserId(long id)
        {
            try
            {
                return await _context.Payments.Where(u => u.UserId.Equals(id)).OrderBy(o => o.Id).ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<List<Payments>> GetPaymetByUserIdNotExpier(long id)
        {
            try
            {
                return await _context.Payments.Where(i => i.UserId.Equals(id) && i.ExpireDate > DateTime.UtcNow).OrderBy(s => s.Id).ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Payments> Insert(Payments model)
        {
            try
            {
                var res = await _context.Payments.AddAsync(model);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<Payments> Update(Payments model)
        {
            try
            {
                var res = await GetById(model.Id);
                if (res != null)
                {
                    var ress = _context.Payments.Update(model);
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

        public async Task<List<Payments>> UserPayment(long userid)
        {
            try
            {
                return await  _context.Payments.Where(u => u.UserId.Equals(userid)).OrderBy(o=>o.Id).ToListAsync();
            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}
