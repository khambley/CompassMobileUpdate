using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace CompassMobileUpdate.DataAccess
{
    public static class SecureStorageAccountStore
    {
        public static async Task SaveAsync(Account account, string serviceId)
        {
            // Find existing accounts for the service
            var accounts = await FindAccountsForServiceAsync(serviceId);

            // Remove existing account with Id if exists
            accounts.RemoveAll(a => a.Username == account.Username);

            // Add account we are saving
            accounts.Add(account);

            // Serialize all the accounts to javascript
            var json = JsonConvert.SerializeObject(accounts);

            // Securely save the accounts for the given service
            await SecureStorage.SetAsync(serviceId, json);
        }
        public static async Task<List<Account>> FindAccountsForServiceAsync(string serviceId)
        {
            var json = await SecureStorage.GetAsync(serviceId);

            try
            {
                return JsonConvert.DeserializeObject<List<Account>>(json);
            }
            catch
            {

            }
            return new List<Account>();
        }
    }
}
