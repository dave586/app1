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
    public class menu
    {

        public int restaurantForeignKey;
        public List<menuItem> restaurantMenu;

        public menu() { } //needed for SQLite

        public menu(int restForKey, List<menuItem> restMenu)
        {
            this.restaurantForeignKey = restForKey;
            this.restaurantMenu = restMenu;
        }


        public int getRestaurantForeignKey()
        {
            return restaurantForeignKey;
        }

        public List<menuItem> getMenu()
        {
            return restaurantMenu;
        }
     }
    }
