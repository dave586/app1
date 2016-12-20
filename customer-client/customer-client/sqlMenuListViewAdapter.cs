using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace customer_client
{
    public class sqlMenuListViewAdapter : BaseAdapter<menuItem>
    {
        private dataAccess data = dataAccess.getInstance();
        Activity context;
        List<menuItem> list;

        public sqlMenuListViewAdapter(Activity _context)//, List<menuItem> _list)
		:base()
	{
            this.context = _context;
            //this.list = _list;
        }

        /*public sqlMenuListViewAdapter(Activity _context)
        :base()
        {
            this.context = _context;
        }*/

        public override int Count
        {
            get { return data.getAllItems().Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override menuItem this[int position]
        {
            get { return data.getAllItemOrdered().ElementAt<menuItem>(position); }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListViewItemRow, parent, false);

            menuItem item = this[position];
            view.FindViewById<TextView>(Resource.Id.Title).Text = item.foodName;


            string price = "$" + item.itemCost;


            view.FindViewById<TextView>(Resource.Id.Description).Text = price;//item.foodDescription;

            using (var imageView = view.FindViewById<ImageView>(Resource.Id.Thumbnail))
            {
                imageView.SetImageResource(item.imageID);
            }
            return view;
        }
    }
}