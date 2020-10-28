using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectHub.Data.Models;
using Syncfusion.Blazor.Schedule.Internal;

namespace ProjectHub.Pages
{
    public partial class Glossary: ComponentBase
    {
        private List<GlossaryItem> glossary;
        private GlossaryItem newGlossaryItem = new GlossaryItem();
        private bool Visibility { get; set; } = false;
        private string Type { get; set; } = "";
        private bool TermInputIsReadonly { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            glossary = await db.GetAllGlossaryItems();

            glossary.Sort((x, y) => x.Term.CompareTo(y.Term));
        }

        private async void InsertGlossaryItem()
        {
            GlossaryItem item = new GlossaryItem
            {
                Term = newGlossaryItem.Term,
                Definition = newGlossaryItem.Definition
            };

            var rowsChanged = await db.InsertGlossaryItem(newGlossaryItem);

            if (rowsChanged == 1)
            {
                glossary.Add(item);
            }

            // clear our form
            newGlossaryItem = new GlossaryItem();

            Visibility = false;

            StateHasChanged();
        }

        private void EditGlossaryItem(GlossaryItem entry)
        {
            newGlossaryItem = new GlossaryItem {
                Term = entry.Term,
                Definition = entry.Definition
            };

            Type = "Edit";

            TermInputIsReadonly = true;

            Visibility = true;

            StateHasChanged();
        }

        private async void DeleteGlossaryItem(string term)
        {
            Console.WriteLine($"Delete {term}");

            var rowsChanged = await db.DeleteGlossaryItemByTerm(term);

            if (rowsChanged == 1)
            {
                glossary.RemoveAll(x => x.Term == term);
            }

            StateHasChanged();
        }

        private void SaveChanges()
        {
            Visibility = false;
        }
    }
    /*
     Things to do and change,
    Change the name of the method that controls the edit button
    Change so that InsertGlossaryItem is the function called when the save button is clicked as it will make the call to the db

    Set up the edit functionality on the database.
     
     */
}
