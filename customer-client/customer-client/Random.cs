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
    public class Random: Activity
    {
        private dataAccess data = dataAccess.getInstance();
        private ListView itemListView;
        private adapter stAdapter; // data adapter for stored items
        private adapter theAdapter;

        private Button viewOrder;
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
                this.Title = "Table: " + Intent.GetStringExtra("tableNumber");

            }


            testFoodItems = new List<string>();
            testFoodItems.Add("Pizza");
            testFoodItems.Add("Burger");
            testFoodItems.Add("Pop");
            testFoodItems.Add("Water");
            testFoodItems.Add("Ice Cream");
            testFoodItems.Add("Fries");


            /*menuItem burger = new menuItem();
            burger.foodName = "Burger";
            burger.foodDescription = "This is burger";
            burger.imageID = 2130837506;*/

            //Resource.Drawable.water;

            //decimal pizza1 = 9.56m;
            menuItem burger = new menuItem("Burger", "This is burger", 5.50m, 2130837506);
            menuItem pizza = new menuItem("Pizza", "This is pizza", 9.56m, 2130837514);
            menuItem pop = new menuItem("Pop", "This is pop", 1.99m, 2130837515);
            menuItem water = new menuItem("Water", "This is water", 1.50m, 2130837516);
            menuItem icecream = new menuItem("Ice Cream", "This is ice cream", 1.50m, 2130837510);
            //menuItem fries = new menuItem("Fries", "This is Fries", 1.50m, 2130837515);
            /*
            //menuItem pizza = new menuItem();
            pizza.foodName = "Pizza";
            pizza.foodDescription = "This is pizza";
            pizza.imageID = 2130837513;*/

            /*menuItem pop = new menuItem();
             pop.foodName = "Pop";
             pop.foodDescription = "This is pop";
             pop.imageID = 2130837514;*/



            menuList = new List<menuItem>();
            menuList.Add(burger);
            menuList.Add(pop);
            menuList.Add(pizza);
            menuList.Add(water);
            menuList.Add(icecream);
            //menuList.Add(fries);



            /*
            testFoodItems = new List<Tuple<string, int>>();
             testFoodItems.Add(new Tuple<string, int>("Pizza", Resource.Drawable.Icon));
             testFoodItems.Add(new Tuple<string, int>("Burger", Resource.Drawable.Icon));
             testFoodItems.Add(new Tuple<string, int>("Pop", Resource.Drawable.Icon));
             testFoodItems.Add(new Tuple<string, int>("Water", Resource.Drawable.Icon));
             */

            connectActions();





            //itemListView.ItemsSource = new string[]
            //{
            //    "test",
            //    "test2"
            //};

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }




        //loading the views kidney had a text field a button and a list view, this need to be changed to our view 
        private void loadViews()
        {
            itemListView = FindViewById<ListView>(Resource.Id.itemListView);
            viewOrder = FindViewById<Button>(Resource.Id.viewOrder);
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
            itemListView.ItemLongClick += itemListView_ItemLongClick;


            //addItemButton.Click += AddItemButton_Click;


            //set up addapter for list view 
            //stAdapter = new adapter(this);
            //if (stAdapter is adapter)
            //{
            //   itemNameEditText.Text = "True";
            //}
            //itemListView.Adapter = stAdapter;
            //itemListView.FastScrollEnabled = true;

            //itemListView.ItemClick += itemListView_ItemClick;
            //itemListView.ItemLongClick += itemListView_ItemLongClick;
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







            string thePrice = message.getItemCost().ToString();

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