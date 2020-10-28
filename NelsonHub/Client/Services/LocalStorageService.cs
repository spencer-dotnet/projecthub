using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NelsonHub.Client.Services
{
    public interface ILocalStorageService
    {
        Task<string> GetItem(string key);
        Task SetItem(string key, string value);
        Task RemoveItem(string key);
    }
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetItem(string key)
        {
            var item = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            return item;
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task SetItem(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
    }
}
