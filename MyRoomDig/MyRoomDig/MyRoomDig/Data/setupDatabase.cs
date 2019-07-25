namespace MyRoomDig.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SQLite;
    using Models;
    using System;
    using Xamarin.Forms;

    public class setupDatabase
    {
        readonly SQLiteAsyncConnection database;
        public setupDatabase(string dbPath)
        {
            try
            {
                database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<SetupApp>().Wait();
                database.CreateTableAsync<SetupMain>().Wait();
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("SetupDatabase", ex.ToString(), "");
            }
        }

        #region SetupApp
        public Task<List<SetupApp>> GetItemsSetupAppAsync()
        {
            return database.Table<SetupApp>().ToListAsync();
        }
        public Task<SetupApp> GetItemSetupAppAsync(int id)
        {
            return database.Table<SetupApp>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<SetupApp> GetItemAsyncRecord()
        {
            return database.Table<SetupApp>().FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(SetupApp item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(SetupApp item)
        {
            return database.DeleteAsync(item);
        }
        #endregion

        #region SetupMain
        public Task<List<SetupMain>> GetItemsSetupMainAsync()
        {
            return database.Table<SetupMain>().OrderBy(x => x.Id).ToListAsync();
        }
        public Task<SetupMain> GetMainAsync()
        {
            return database.Table<SetupMain>().Where(m => m.IsMain).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(SetupMain item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public async Task<int> SaveListItemMainAsync(List<SetupMain> setupMains)
        {
            int Updates = 0;
            foreach (SetupMain item in setupMains)
            {
                if (item.Id != 0)
                {
                    Updates += await database.UpdateAsync(item);
                }
                else
                {
                    Updates += await database.InsertAsync(item);
                }
            }
            return Updates;
        }
        #endregion
    }
}
