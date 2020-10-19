using ProjectHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Data.SqlDatabase
{
    public class ReferenceData : IReferenceData
    {
        private readonly ISqlDataAccess _db;

        public ReferenceData(ISqlDataAccess db)
        {
            _db = db;
        }
        public Task<List<Reference>> GetAllReferences()
        {
            string sql = "select * from reference";

            return _db.LoadData<Reference, dynamic>(sql, new { });
        }

        public Task<int> InsertReference(Reference reference)
        {
            string sql = @$"INSERT INTO reference (title, link, description, category)
                        SELECT @Title, @Link, @Description, @Category ;";

            return _db.SaveData(sql, reference);
        }
    }
}
