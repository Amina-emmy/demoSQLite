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
            lsv1 = FindViewById<ListView>(Resource.Id.listView1);
            LoadData(); // the function in here
            

            // ADD Data
            infos = new Infos(); 
            var inp_name = FindViewById<EditText>(Resource.Id.inp_name);
            var inp_feeling = FindViewById<EditText>(Resource.Id.inp_feel);
            var btnAdd = FindViewById<Button>(Resource.Id.btn_add);

            btnAdd.Click += (s, e) =>
            {
                infos.Name=inp_name.Text;
                infos.Feeling=inp_feeling.Text;
                db.InsertData(infos);
                LoadData();
                // vider les champs after entering Data
                inp_name.Text = "";
                inp_feeling.Text = "";

            };

            // Before Edit or Delete => Bending Data : put the data clicked in lsv1 back to fields  
            lsv1.ItemClick += (s, e) =>
            {
                var editInp_name = e.View.FindViewById<TextView>(Resource.Id.tv_name);
                var editInp_feeling = e.View.FindViewById<TextView>(Resource.Id.tv_feel);
                // put the text of these vars into the inputs we already got before 
                inp_name.Text=editInp_name.Text;
                inp_feeling.Text=editInp_feeling.Text;
                inp_name.Tag = e.Id;

            };

            // DELETE Data
            var btnDelete = FindViewById<Button>(Resource.Id.btn_delete);
            btnDelete.Click += (s, e) =>
            {
                // some vars here are initialized before
                infos.id = int.Parse(inp_name.Tag.ToString());
                infos.Name = inp_name.Text;
                infos.Feeling = inp_feeling.Text;
                db.DeleteData(infos);
                LoadData();
                // vider les champs after entering Data
                inp_name.Text = "";
                inp_feeling.Text = "";
            };

            // EDIT Data
            var btnEdit = FindViewById<Button>(Resource.Id.btn_edit);
            btnEdit.Click += (s, e) =>
            {
                // some vars here are initialized before
                infos.id = int.Parse(inp_name.Tag.ToString());
                infos.Name = inp_name.Text;
                infos.Feeling = inp_feeling.Text;
                db.EditData(infos);
                LoadData();
                // vider les champs after entering Data
                inp_name.Text = "";
                inp_feeling.Text = "";
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