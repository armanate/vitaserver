namespace JWTAPI.Services
{
    public class DocumentService:IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
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
                    _context.Document.Remove(model);
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

        public async Task<List<Document>> GetALl()
        {
            return await _context.Document.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Document> GetById(long id)
        {
            return await _context.Document.AsNoTracking().FirstAsync(i => i.Id.Equals(id));

        }

        public async Task<Document> Insert(Document model)
        {
            try
            {
                var res = await _context.Document.AddAsync(model);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Document> Update(Document model)
        {
            try
            {
                var res = await GetById(model.Id);
                if (res != null)
                {
                    var ress = _context.Document.Update(model);
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
