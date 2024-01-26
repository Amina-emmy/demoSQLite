using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;

namespace demoSQLite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Database db;
        Infos infos;

        ListView lsv1; // to assign our layout's listView to it
        List<Infos> listInfos; // to stock the list loaded from the DB
        Adapter adapter; // our lsv1 has adapter, we will assign this to it

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Create the DataBase
            db = new Database();
            db.CreateDatabase(); // the function in Database.cs

            // LOAD Data
            LoadData(); // the function in here
            lsv1 = FindViewById<ListView>(Resource.Id.listView1);

            // ADD Data
            infos = new Infos(); 
            var name = FindViewById<EditText>(Resource.Id.inp_name);
            var feeling = FindViewById<EditText>(Resource.Id.inp_feel);
            var btnAdd = FindViewById<Button>(Resource.Id.btn_add);

            btnAdd.Click += (s, e) =>
            {
                infos.Name=name.Text;
                infos.Feeling=feeling.Text;
                db.InsertData(infos);
                LoadData();
            };

        }

        private void LoadData()
        {
            // the method return a List<Infos> so we store it in the var we declared
            listInfos = db.LoadtData(); // the function in Database.cs
            // create a new adapter in the MainActivity with the list we got
            adapter = new Adapter(this,listInfos);
            // set the adapter of our listView we got by Id
            try {            
                lsv1.Adapter = adapter;
            }
            catch { }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}