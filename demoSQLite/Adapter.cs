
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;

namespace demoSQLite
{

    public class ViewHolder : Java.Lang.Object
    {
        public TextView tvName { get; set; }
        public TextView tvFeel { get; set; }

    }


    public class Adapter : BaseAdapter
    {
        private Activity activity;
        private List<Infos> infos;

        // Constructor 
        public Adapter (Activity activity, List<Infos> infos)
        {
            this.activity = activity;
            this.infos = infos;
        }

        public override int Count 
        { 
            get { return infos.Count; }
        
        }

        public override Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return infos[position].id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // the view we created
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_card, parent, false);
            // get its elemnts 
            var tvName = view.FindViewById<TextView>(Resource.Id.tv_name);
            var tvFeel = view.FindViewById<TextView>(Resource.Id.tv_feel);
            tvName.Text = infos[position].Name;
            tvFeel.Text = infos[position].Feeling;

            return view;
        }
    }
}