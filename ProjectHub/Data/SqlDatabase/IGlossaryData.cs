using ProjectHub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectHub.Data.SqlDatabase
{
    public interface IGlossaryData
    {
        Task<List<GlossaryItem>> GetAllGlossaryItems();
        public Task<GlossaryItem> GetGlossaryItemByTerm(string term);
        Task<int> InsertGlossaryItem(GlossaryItem item);

        Task<int> DeleteGlossaryItemByTerm(string term);
    }
}