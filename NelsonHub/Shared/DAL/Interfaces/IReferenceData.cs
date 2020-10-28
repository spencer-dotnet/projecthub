using NelsonHub.Shared.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Shared.DAL
{
    public interface IReferenceData
    {
        Task<List<ReferenceModel>> GetAllReferences();
        Task<int> InsertReference(ReferenceModel reference);
    }
}