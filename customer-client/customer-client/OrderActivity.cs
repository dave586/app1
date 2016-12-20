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

            
            

            //Resource.Drawable.waffleBurger;
           
            int holder = 0;
            if (Intent.HasExtra("resId") && Intent.GetIntExtra("resId", holder) == 1)
            {
                menuItem glazedGingerBeef = new menuItem("Glazed Ginger Beef", "Candied shishito peppers, crisp ramen, poblano avacoado aioli, mango salsa", "14.50", 2130837512);
                menuItem chickenTacos = new menuItem("Chicken tacos", "Poblano avacoado aioli, roma tomatoes, microgreens, brocolli slaw, shishito peppers, mango salsa", "13.00", 2130837507);
                menuItem lobsterCake = new menuItem("Lobster Cakes", "Chili Remoulade, Roaster peppers", "14.25", 2130837515);
                menuItem pasta = new menuItem("Creamy Pesto Penne", "Bechamel, proscioutto, chicken, tomato ", "18.50", 2130837519);
                menuItem twoOneBurger = new menuItem("Two one Burger", "Mayo, Dijon, Arugala, roma tomatoes, onion, jam, pickles, aged cheddar, maple peppered bacon, brioche bun", "15.50", 2130837529);


                menuList = new List<menuItem>();
                menuList.Add(glazedGingerBeef);
                menuList.Add(chickenTacos);
                menuList.Add(lobsterCake);
                menuList.Add(pasta);
                menuList.Add(twoOneBurger);
            }

            if (Intent.HasExtra("resId") && Intent.GetIntExtra("resId", holder) == 2)
            {
                menuItem strawberryMousse = new menuItem("Strawberry Mousse", "A creamy strawberry based served with a fresh cut strawberry in the glass of your choice", "18.00", 2130837526);
                menuItem tenderloin = new menuItem("Tenderloin", "A delicious tenderloin steak, served with house seasoning on top of grilled vegetables", "22.00", 2130837521);
                menuItem steakFrites = new menuItem("Steak Frites", "A juicy high grade steak, served with microgreens, and cheesy frites", "18.00", 2130837525);
                menuItem tunaTataki = new menuItem("Tuna Tataki", "A savoury smoked tuna served with a light and spicy manga sala", "15.00", 2130837527);
                menuItem goudaBurger = new menuItem("Gouda Burger", "A colourful pink waffle burger, served with gouda cheese and the toppings of your choice", "14.00", 2130837530);



                menuList = new List<menuItem>();
                menuList.Add(strawberryMousse);
                menuList.Add(tenderloin);
                menuList.Add(steakFrites);
                menuList.Add(tunaTataki);
                menuList.Add(goudaBurger);
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