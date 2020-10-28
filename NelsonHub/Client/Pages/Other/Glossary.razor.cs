using Microsoft.AspNetCore.Components;
using NelsonHub.Client.Components;
using NelsonHub.Client.Services;
using NelsonHub.Shared.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Client.Pages.Other
{
    public partial class Glossary: ComponentBase
    {
        private Modal modal { get; set; }
        private Modal editModal { get; set; }

        private List<GlossaryItemModel> glossary;

        private GlossaryItemModel newGlossaryItem = new GlossaryItemModel();

        [Inject]
        protected IHttpService Http { get; set; }


        protected override async Task OnInitializedAsync()
        {
            glossary = await Http.Get<List<GlossaryItemModel>>("api/glossary");

            glossary.Sort((x, y) => x.Term.CompareTo(y.Term));
        }

        private async void InsertGlossaryItem()
        {
            GlossaryItemModel item = new GlossaryItemModel
            {
                Term = newGlossaryItem.Term,
                Definition = newGlossaryItem.Definition
            };

            var returnedItem = await Http.Post<GlossaryItemModel>("api/glossary", item);

            if(returnedItem.Term != null)
                glossary.Add(returnedItem);

            // clear our form
            newGlossaryItem = new GlossaryItemModel();

            modal.Close();

            StateHasChanged();
        }

        private void OpenEditModal(GlossaryItemModel entry)
        {
            newGlossaryItem = new GlossaryItemModel
            {
                Term = entry.Term,
                Definition = entry.Definition
            };

            editModal.Open();

            StateHasChanged();
        }

        private void EditGlossaryItem()
        {

            editModal.Close();
            // form submission
        }

        private async void DeleteGlossaryItem(GlossaryItemModel item)
        {
            Console.WriteLine($"Delete {item.Term}");

            var itemDeleted = await Http.Delete<GlossaryItemModel>("api/glossary", item);

            if (itemDeleted.Term != null)
            {
                glossary.RemoveAll(x => x.Term == itemDeleted.Term);
            }

            StateHasChanged();
        }
    }
    /*
     Things to do and change,
    Change the name of the method that controls the edit button
    Change so that InsertGlossaryItem is the function called when the save button is clicked as it will make the call to the db

    Set up the edit functionality on the database.
     
     */
}
