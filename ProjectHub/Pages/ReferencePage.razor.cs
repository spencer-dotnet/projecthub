using Microsoft.AspNetCore.Components;
using ProjectHub.Data.Models;
using ProjectHub.Data.SqlDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Pages
{
    public partial class ReferencePage
    {
        private List<Reference> references;
        private string Type { get; set; } = "Add";
        private Reference newReference = new Reference();
        private bool Visibility { get; set; } = false;

        private string[] CategoryValues = Enum.GetNames(typeof(ReferenceCategory));

        [Inject]
        private IReferenceData _database { get; set; }


        protected override async Task OnInitializedAsync()
        {
            references = await _database.GetAllReferences();
        }

        private void AddReference()
        {
            Reference reference = new Reference()
            {
                Title = newReference.Title,
                Description = newReference.Description,
                Link = newReference.Link,
                Category = newReference.Category
            };

            _database.InsertReference(reference);

            newReference = new Reference();

            references.Add(reference);

            StateHasChanged();
        }
    }
}
