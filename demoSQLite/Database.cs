using Android.Database;
using Android.Util;
using Java.Util;
using SQLite;
using System.Collections.Generic;


namespace demoSQLite
{
    internal class Database
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        // Create the DB and the tables within it  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool CreateDatabase()
        {
            try
            {
                // create the DB in that path with the name myDataBase
                using (var con = new SQLiteConnection(System.IO.Path.Combine(path,"myDatabase.db")))
                {
                    // create our table within our DB , between the <> is the class of the table
                    con.CreateTable<Infos>();
                    return true;
                }

            }catch(SQLiteException ex)
            {
                Log.Info("this is an exception",ex.Message);
                return false;
            }
        }

        // ADD Data    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool InsertData(Infos infos)
        {
            try
            {
                // create the DB in that path with the name myDataBase
                using (var con = new SQLiteConnection(System.IO.Path.Combine(path, "myDatabase.db")))
                {
                    // insert data
                    con.Insert(infos);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("this is an exception", ex.Message);
                return false;
            }
        }
        // LOAD Data    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public List<Infos> LoadtData()
        {
            try
            {
                // create the DB in that path with the name myDataBase
                using (var con = new SQLiteConnection(System.IO.Path.Combine(path, "myDatabase.db")))
                {
                    return con.Table<Infos>().ToList();
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("this is an exception", ex.Message);
                return null;
            }
        }
        // Delete Data    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool DeleteData(Infos infos)
        {
            try
            {
                // create the DB in that path with the name myDataBase
                using (var con = new SQLiteConnection(System.IO.Path.Combine(path, "myDatabase.db")))
                {
                    // insert data
                    con.Delete(infos);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("this is an exception", ex.Message);
                return false;
            }
        }
        // Edit Data    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool EditData(Infos infos)
        {
            try
            {
                // create the DB in that path with the name myDataBase
                using (var con = new SQLiteConnection(System.IO.Path.Combine(path, "myDatabase.db")))
                {
                    // insert data
                    con.Query<Infos>("UPDATE infos set Name=?,Feeling=? WHERE id=?",infos.Name,infos.Feeling,infos.id);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {
                Log.Info("this is an exception", ex.Message);
                return false;
            }
        }
    }
}