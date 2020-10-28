using ProjectHub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectHub.Data.SqlDatabase
{
    public interface IReferenceData
    {
        Task<List<Reference>> GetAllReferences();
        Task<int> InsertReference(Reference reference);
    }
}