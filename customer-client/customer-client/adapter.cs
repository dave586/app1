using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;






namespace customer_client
{
    public class adapter : BaseAdapter<menuItem> //the menu item type isnt working for some reason, simliar to <menuItem>
    {


        private dataAccess data = dataAccess.getInstance();
        private Activity context;

        public override int Count
        {
            get
            {
                //throw new NotImplementedException();
                return data.getAllItems().Count;
                //throw new InvalidOperationException();
                //Maybe count later???
                
            }
        }

        // change from DataAccessLayer



        public adapter(Activity context)
        {
            this.context = context;
        }



        //changing from menuItem
        public override menuItem this[int position]
        {
            get
            {
                return data.getAllItemOrdered().ElementAt<menuItem>(position);
            }
        }


        //method for getting item id 
        public override long GetItemId(int position)
        {
            return position;
        }





        //get view 
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            menuItem st = this[position];

            if (convertView == null)
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            TextView txt = convertView.FindViewById<TextView>(Android.Resource.Id.Text1);
            txt.Text = "" + st;//this will call the toString of the menuItem class
            return convertView;
        }









    }
}