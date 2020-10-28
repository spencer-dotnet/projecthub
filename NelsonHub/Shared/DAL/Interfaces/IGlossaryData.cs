using NelsonHub.Shared.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Shared.DAL
{
    public interface IGlossaryData
    {
        Task<int> DeleteGlossaryItemByTerm(string term);
        Task<List<GlossaryItemModel>> GetAllGlossaryItems();
        Task<GlossaryItemModel> GetGlossaryItemByTerm(string term);
        Task<int> InsertGlossaryItem(GlossaryItemModel item);
    }
}