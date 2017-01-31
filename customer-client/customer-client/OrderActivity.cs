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
using Android.Views.InputMethods;

//using Xamarin.Forms;

namespace customer_client
{
    [Activity(Label = "Quick Byte Menu")]
    public class OrderActivity : Activity
    {
        private dataAccess data = dataAccess.getInstance();
        private ListView itemListView;
        private adapter stAdapter; // data adapter for stored items
        private adapter theAdapter;

        private Button viewOrder;
        private Button callWaiter;
        //private Button addItemButton;
        //private EditText itemNameEditText;

        private List<string> testFoodItems;

        private List<menuItem> menuList;
      



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.menuOrder);
            //avoid automaticaly appear of android keyboard when activitry starts
            Window.SetSoftInputMode(SoftInput.StateHidden);

            

            //ListView testListView = new ListView();

            loadViews();


            if (Intent.HasExtra("tableNumber"))
            {
                //GetTableActivity.Window.SetTitle
                this.Title = "Table Number:  " + Intent.GetStringExtra("tableNumber");

            }

            
            

            //Resource.Drawable.water;
           
            int holder = 0;
            if (Intent.HasExtra("resId") && Intent.GetIntExtra("resId", holder) == 1)
            {
                menuItem strawberryMousse = new menuItem("Strawberry Mousse", "A creamy strawberry based served with a fresh cut strawberry in the glass of your choice", "18.00", Resource.Drawable.strawberryMousse);
                menuItem tenderloin = new menuItem("Tenderloin", "A delicious tenderloin steak, served with house seasoning on top of grilled vegetables", "22.00", Resource.Drawable.primeRib);
                menuItem steakFrites = new menuItem("Steak Frites", "A juicy high grade steak, served with microgreens, and cheesy frites", "18.00", Resource.Drawable.steakFrites);
                menuItem tunaTataki = new menuItem("Tuna Tataki", "A savoury smoked tuna served with a light and spicy manga sala", "15.00", Resource.Drawable.tunaTataki);
                menuItem goudaBurger = new menuItem("Gouda Burger", "A colourful pink waffle burger, served with gouda cheese and the toppings of your choice", "14.00", Resource.Drawable.twoOneBurger);



                menuList = new List<menuItem>();
                menuList.Add(strawberryMousse);
                menuList.Add(tenderloin);
                menuList.Add(steakFrites);
                menuList.Add(tunaTataki);
                menuList.Add(goudaBurger);
            }

            if (Intent.HasExtra("resId") && Intent.GetIntExtra("resId", holder) == 2)
            {
                menuItem pizza = new menuItem("Pizza", "Insert new description here", "14.50", Resource.Drawable.pizza);
                menuItem burger = new menuItem("Burger", "Insert new description here", "13.00", Resource.Drawable.burger);
                menuItem iceCream = new menuItem("Ice Cream", "Insert new description here", "14.25", Resource.Drawable.icecream);
                menuItem soda = new menuItem("Soda", "Insert new description here", "18.50", Resource.Drawable.soda);
                menuItem water = new menuItem("Water", "Insert new description here", "15.50", Resource.Drawable.water);


                menuList = new List<menuItem>();
                menuList.Add(pizza);
                menuList.Add(burger);
                menuList.Add(iceCream);
                menuList.Add(soda);
                menuList.Add(water);
            }

                

            connectActions();

        }




        //loading the views kidney had a text field a button and a list view, this need to be changed to our view 
        private void loadViews()
        {
            itemListView = FindViewById<ListView>(Resource.Id.itemListView);
            viewOrder = FindViewById<Button>(Resource.Id.viewOrder);
            callWaiter = FindViewById<Button>(Resource.Id.callWaiter);
            //addItemButton = FindViewById<Button>(Resource.Id.addItemButton);
            //itemNameEditText = FindViewById<EditText>(Resource.Id.nameEditText);
        }





        // action delagation, setting new adapter and sting up the views
        private void connectActions()
        {
            viewOrder.Click += ViewOrder_Click;


            //ArrayAdapter<Tuple<string, int>> theAdapter = new ArrayAdapter<Tuple<string, int>>(this, Android.Resource.Layout.ActivityListItem, testFoodItems);

            ArrayAdapter<string> theAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, testFoodItems);



            itemListView.Adapter = new menuListViewAdapter(this, menuList);//theAdapter;
            itemListView.FastScrollEnabled = true;

            itemListView.ItemClick += itemListView_ItemClick;

            callWaiter.Click += callWaiter_Click;

        }

        private void callWaiter_Click(object sender, EventArgs e)
        {
            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Ping Sent");
            dialog.SetMessage("A waiter will be there shortly");
            dialog.Show();
        }

        private void ViewOrder_Click(object sender, EventArgs e)
        {
            var ordertableact = new Android.Content.Intent(this, typeof(OrderTableAct));
            //extras here
            StartActivity(ordertableact);





           // var detailAct = new Android.Content.Intent(this, typeof(DetailActivity));
           // StartActivity(detailAct);
        }






        //Will just display an alert of all the student info
        private void itemListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            menuItem message = menuList[e.Position];
            //public final static string EXTRA_MESSAGE = "com."

            /*var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Menu Info");
            dialog.SetMessage(message);
            dialog.Show();*/

            //menuItem clickedItem = new menuItem(message);

            //var ordertableact = new Android.Content.Intent(this, typeof(OrderTableAct));


            //ordertableact.PutExtra("menuItemName", message);
            //StartActivity(ordertableact);







            string thePrice = message.itemCost;

            var detailAct = new Android.Content.Intent(this, typeof(DetailActivity));

            detailAct.PutExtra("menuItemName", message.getFoodName());
            detailAct.PutExtra("foodDescription", message.getDescription());
            detailAct.PutExtra("itemImage", message.getImgId());
            detailAct.PutExtra("itemCost", thePrice);








            StartActivity(detailAct);




            //data.addItem(clickedItem);
            //theAdapter.NotifyDataSetChanged();


            //menuItem selectedSt = theAdapter[e.Position];

            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Menu Info");
            //dialog.SetMessage(selectedSt.ID + " " + selectedSt.foodName);
            //dialog.Show();

        }






        //Will ask if they want to remove the student
        private void itemListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            /*
            menuItem selectedSt = theAdapter[e.Position];

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Delete menu item");
            dialog.SetMessage(selectedSt.ID + " " + selectedSt.foodName);
            dialog.SetPositiveButton("Delete",
                (senderAlert, args) =>
                { // action for this button
                    data.deleteItemByID(selectedSt.ID);
                    stAdapter.NotifyDataSetChanged();
                    Toast.MakeText(this, "The menu item has been deleted", ToastLength.Short).Show();
                }
                );
            dialog.SetNegativeButton("cancel", (senderAlert, args) => { });

            dialog.Show();
            */





        }


        /*
        private void AddItemButton_Click(object sender, EventArgs e)
        {
            data.addItem(new menuItem(itemNameEditText.Text));//change from addStudent
            itemNameEditText.Text = ""; //clear field after entering data
            stAdapter.NotifyDataSetChanged(); //sends signal to list that it should refresh the data 

            //Hide keyboard after use for our text field
            hideKeyBoard(itemNameEditText);

        }*/


        //this hides the key board
        private void hideKeyBoard(Android.Views.View element)
        {
            InputMethodManager im = (InputMethodManager)GetSystemService(Context.InputMethodService);
            im.HideSoftInputFromWindow(element.WindowToken, 0);

        }


    }
}