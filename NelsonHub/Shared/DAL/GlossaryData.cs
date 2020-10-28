using NelsonHub.Shared.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Shared.DAL
{
    public class GlossaryData : IGlossaryData
    {
        private readonly INelsonHubSqlDataAccess _db;

        public GlossaryData(INelsonHubSqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<GlossaryItemModel>> GetAllGlossaryItems()
        {
            string sql = "select * from glossary";

            return _db.LoadData<GlossaryItemModel, dynamic>(sql, new { });
        }

        public Task<GlossaryItemModel> GetGlossaryItemByTerm(string term)
        {
            string sql = $"select * from glossary where term ilike '{term}'";

            return _db.LoadOneData<GlossaryItemModel, dynamic>(sql, new { });
        }

        public Task<int> InsertGlossaryItem(GlossaryItemModel item)
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
