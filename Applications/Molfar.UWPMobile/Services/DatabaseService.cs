using Molfar.Core.Services;
using SQLite;
using System;
using System.IO;


namespace Molfar.UWPMobile.Services
{
    public class DatabaseService : IDatabaseService
    {
        private const string DB_PATH = "MOLFAR_DB";

        public SQLiteConnection Connection { get; private set; }

        public DatabaseService()
        {
            Connection = CreateDatabase();
        }

        private SQLiteConnection CreateDatabase()
        {
            try
            {
                string dbPath = Path.Combine(
           Windows.Storage.ApplicationData.Current.LocalFolder.Path,
           DB_PATH);



                var db = new SQLiteConnection(dbPath);

                return db;
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }
    }
}
