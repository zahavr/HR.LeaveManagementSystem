using System.Text.Json;
using HR.LeaveManagementSystem.UI.Contracts;
using Microsoft.JSInterop;

namespace HR.LeaveManagementSystem.UI.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<T?> GetItemAsync<T>(string key)
    {
        string? json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
        if (json == null)
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task<bool> ContainKeyAsync(string key)
    {
        string json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

        return string.IsNullOrEmpty(json) is false;
    }
}