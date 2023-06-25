namespace JWTAPI.Core.Services
{
    public interface IBusinessEntityId
    {
        Task<List<BusinessEntityId>> GetALl();
        Task<BusinessEntityId> GetById(string name);
        Task<BusinessEntityId> Update(BusinessEntityId model);
        Task<bool> Delete(string name);
        Task<BusinessEntityId> Insert(BusinessEntityId model);
    }
}
