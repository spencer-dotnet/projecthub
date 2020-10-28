using Microsoft.AspNetCore.Components;
using NelsonHub.Client.Components;
using NelsonHub.Client.Services;
using NelsonHub.Shared.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Client.Pages.Other
{
    public partial class ReferencePage : ComponentBase
    {
        private Modal modal { get; set; }
        private List<ReferenceModel> references;
        private ReferenceModel newReference = new ReferenceModel();
        private string[] CategoryValues = Enum.GetNames(typeof(ReferenceCategory));

        [Inject]
        protected IHttpService Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            references = await Http.Get<List<ReferenceModel>>("api/reference");
        }

        private async void AddReference()
        {
            ReferenceModel reference = new ReferenceModel()
            {
                Title = newReference.Title,
                Description = newReference.Description,
                Link = newReference.Link,
                Category = newReference.Category
            };

            reference = await Http.Post<ReferenceModel>("api/reference", reference);

            if(reference.Id == 0)
                newReference = new ReferenceModel();

            references.Add(reference);

            StateHasChanged();

            modal.Close();
        }
    }
}
