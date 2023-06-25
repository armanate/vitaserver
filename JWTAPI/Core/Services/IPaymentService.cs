using JWTAPI.Core.Models;

namespace JWTAPI.Core.Services
{
    public interface IPaymentService
    {
        Task<List<Payments>> GetALl();
        Task<Payments> GetById(long id);
        Task<List<Payments>> GetPaymetByUserId(long id);
        Task<List<Payments>> GetPaymetByUserIdNotExpier(long id);
        Task<Payments> Update(Payments model);
        Task<bool> Delete(long id);
        Task<Payments> Insert(Payments model);
        Task<List<Payments>> UserPayment(long userid);

    }
}
