using System;
using System.Collections;
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
    class order
    {
        int sessionID = 0; //ID to identify the current session
        JavaList itemsOrdered = new JavaList<menuItem>();
        int menuCount = 0; //used to determine how many items on list ()

        public order( int assignedID)
        {
            sessionID = assignedID;
        }
        public void addItem(menuItem newItem)
        {
            menuCount = menuCount + 1;
            //itemsOrdered.Add(newItem(newItem, menuCount));
        }
        public void removeItem(int removingItem)
        {
            itemsOrdered[removingItem] = null;
            // need to add removing item possibly?
            //maybe use arraylist in place of array for an easier list
        }
        /*public Array displayList()
        {
            String testDisplay = "";
            for (int i = 0; i < menuCount; i ++)
            {
                testDiplay = testDisplay + itemsOrdered.get(menuCount).getFoodName();
            }
            System.Out.println("test iterate of strings:" + testDisplay);
        }*/
        


    }
}