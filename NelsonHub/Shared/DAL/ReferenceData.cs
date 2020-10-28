using NelsonHub.Shared.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Shared.DAL
{
    public class ReferenceData : IReferenceData
    {
        private readonly INelsonHubSqlDataAccess _db;

        public ReferenceData(INelsonHubSqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<ReferenceModel>> GetAllReferences()
        {
            string sql = "select * from reference";

            return _db.LoadData<ReferenceModel, dynamic>(sql, new { });
        }

        public Task<int> InsertReference(ReferenceModel reference)
        {
            string sql = @$"INSERT INTO reference (title, link, description, category)
                        SELECT @Title, @Link, @Description, @Category ;";

            return _db.SaveData(sql, reference);
        }
    }
}
