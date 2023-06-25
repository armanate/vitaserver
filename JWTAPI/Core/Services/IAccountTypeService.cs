using System.Security.Principal;

namespace JWTAPI.Core.Services
{
    public interface IAccountTypeService
    {
        Task<List<AccountType>> GetALl();
        Task<AccountType> GetById(long id);
        Task<AccountType> Update(AccountType model);
        Task<bool> Delete(long id);
        Task<AccountType> Insert(AccountType model);
    }
}
