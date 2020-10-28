using Microsoft.AspNetCore.Http;
using ProjectHub.Data.Models;
using ProjectHub.Data.SqlDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Not using this at the moment
namespace ProjectHub.Data.Services
{
    public class GlossaryService
    {
        private List<GlossaryItem> TestGlossary = new List<GlossaryItem>
        {
            new GlossaryItem
            {
                Term = "Access Token",
                Definition = "A credential that can be used by an application to access an API. It informs the API that the bearerof the token has ben authorized to access the API and perform specific ations specified by the scope that has been granted."
            },
            new GlossaryItem
            {
                Term = "Test Term",
                Definition = "Test Definition"
            }
        };

        private readonly IGlossaryData _database;

        public GlossaryService(IGlossaryData data)
        {
            _database = data;
        }

        public async Task<List<GlossaryItem>> GetAllGlossaryItemsAsync()
        {
            return await _database.GetAllGlossaryItems();
        }

        public async Task<GlossaryItem> GetGlossaryItemByTerm(string term)
        {
            return await _database.GetGlossaryItemByTerm(term);
        }

        public async Task<int> AddGlossaryItem(GlossaryItem glossaryItem)
        {
            return await _database.InsertGlossaryItem(glossaryItem);
        }

        public async Task<int> DeleteGlossaryItemByTerm(string term)
        {
            return await _database.DeleteGlossaryItemByTerm(term);
        }
    }
}
