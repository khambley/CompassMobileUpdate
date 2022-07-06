using CompassMobileUpdate.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace CompassMobileUpdate.DataAccess
{
    public class LocalSql
    {
        static object _databaseLock = new Object();
        SQLiteAsyncConnection _database;

        //TODO: Use Xamarin.Essentials SecureStorage instead. AccountStore is depreciated in latest Xamarin.
        AccountStore _store;
        
        const string _authService = "com.exeloncorp.compass";

        string DatabasePath
        {
            get
            {
                var sqliteFilename = "CompassMobile.db";

                // Xamarin will find the closest match to "MyDocuments" on each platform targeted. KLH
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                var path = Path.Combine(documentsPath, sqliteFilename);

                return path;
                
            }
        }

        public LocalSql()
        {
            _database = new SQLiteAsyncConnection(DatabasePath);
            _database.CreateTableAsync<LocalMeter>();
            _database.CreateTableAsync<LocalVoltageRule>();
        }

        public AppUser GetAppUser()
        {
            var accounts = FindAccountsForServiceAsync(_authService).ToList();
            if(accounts.Count > 0)
            {
                return AppUser.GetUserFromAccount(accounts[0]);
            }
            else
            {
                return null;
            }
        }
        public List<Account> FindAccountsForServiceAsync(string serviceName)
        {
            // Get json for accounts for the service
            var json = SecureStorage.GetAsync(serviceName);

            try
            {
                // Try to return deserialized list of accounts
                return JsonConvert.DeserializeObject<List<Account>>(json.Result);
            }
            catch { }

            // If this fails, return an empty list
            return new List<Account>();
        }

        public async Task<List<LocalVoltageRule>> GetVoltageRules()
        {
            return await _database.Table<LocalVoltageRule>().ToListAsync();
        }
    }
}
