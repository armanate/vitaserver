namespace JWTAPI.Core.Services
{
    public interface IDocumentService
    {
        Task<List<Document>> GetALl();
        Task<Document> GetById(long id);
        Task<Document> Update(Document model);
        Task<bool> Delete(long id);
        Task<Document> Insert(Document model);
    }
}
