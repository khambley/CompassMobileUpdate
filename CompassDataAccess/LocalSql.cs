using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace CompassDataAccess
{
    public class LocalSql
    {
        static object _databaseLock = new Object();
        SQLiteConnection _database;
        AccountStore _store;
        const string _authService = "com.exeloncorp.compass";
    }
}
