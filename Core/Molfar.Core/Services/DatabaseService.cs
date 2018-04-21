using SQLite;
using System;
using System.IO;


namespace Molfar.Core.Services
{
    public class DatabaseService
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
           Environment.GetFolderPath(Environment.SpecialFolder.Personal),
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
