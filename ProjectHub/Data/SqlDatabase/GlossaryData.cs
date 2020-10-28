using ProjectHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Data.SqlDatabase
{
    public class GlossaryData : IGlossaryData
    {
        private readonly ISqlDataAccess _db;

        public GlossaryData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<GlossaryItem>> GetAllGlossaryItems()
        {
            string sql = "select * from glossary";

            return _db.LoadData<GlossaryItem, dynamic>(sql, new { });
        }

        public Task<GlossaryItem> GetGlossaryItemByTerm(string term)
        {
            string sql = $"select * from glossary where term ilike '{term}'";

            return _db.LoadOneData<GlossaryItem, dynamic>(sql, new { });
        }

        public Task<int> InsertGlossaryItem(GlossaryItem item)
        {
            string sql = @$"INSERT INTO glossary (term, definition)
                        SELECT @Term, @Definition
                        WHERE
                        NOT EXISTS (
                        SELECT term FROM glossary WHERE term = @Term
                        );";

            return _db.SaveData(sql, item);
        }

        public Task<int> DeleteGlossaryItemByTerm(string term)
        {
            string sql = $@"delete from glossary where term = '{term}';";

            return _db.SaveData(sql, term);
        }

        //public Task<int> EditGlossaryItemByTerm(string term)
        //{
        //}
    }
}
