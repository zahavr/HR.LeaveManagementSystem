namespace HR.LeaveManagementSystem.UI.Contracts;

public interface ILocalStorageService
{
    Task<T?> GetItemAsync<T>(string key);
    
    Task SetItemAsync<T>(string key, T value);
    
    Task RemoveItemAsync(string key);

    Task<bool> ContainKeyAsync(string key);
}