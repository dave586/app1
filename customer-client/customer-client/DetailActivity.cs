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
    [Activity(Label = "Detail View")]
    public class DetailActivity : Activity
    {
        private TextView price;
        private TextView description;
        private Button addButton;
        private ImageView image;
        private TextView name;

        private dataAccess data = dataAccess.getInstance();
        //private adapter stAdapter; // data adapter for stored items
        //private adapter adapter;

        private menuItem item;

        //Extra Name Constants
        private const string ENAME = "menuItemName";
        private const string EDESCRIPTION = "foodDescription";
        private const string EIMAGE = "itemImage";
        private const string ECOST = "itemCost";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Build menuItem object to populate fields. Should be replaced once menuItem gets a constructor update
            buildItem();

            //Set the main view
            SetContentView(Resource.Layout.detail);

            //SetUpElements
            connectElements();

            //avoid automaticaly appear of android keyboard when activitry starts
            Window.SetSoftInputMode(SoftInput.StateHidden);

            addButton.Click += AddButton_Click;

        }

        /*
         * This builds a menu item out of the extras, to be used later to populate the fields for the activity
         * 
         * IMPORTANT: This method should be axed entirely as soon as menuItem gets a full constructor, & this functionality
         *              should be covered by the constructor
         */
        private void buildItem()
        {






            //Creates a new menu item from the item name
           // item = new menuItem(Intent.GetStringExtra("menuItemName"),Intent.GetStringExtra("itemCost"), Intent.GetIntExtra(EIMAGE, 10));
            //decimal aprice = Intent.GetStringExtra(ECOST).ToDecimal(
            item = new menuItem(Intent.GetStringExtra(ENAME), Intent.GetStringExtra(EDESCRIPTION), Intent.GetStringExtra(ECOST), Intent.GetIntExtra(EIMAGE, 10));
            //item = new menuItem(Intent.GetStringExtra(ENAME), Intent.GetStringExtra(ECOST));


            //item = new menuItem(Intent.GetStringExtra("menuItemName"), Intent.GetStringExtra(ECOST), Intent.GetIntExtra(EIMAGE, 10));


            //Adds additional information to the object where available
            //Adds food description
            /*if (Intent.HasExtra(EDESCRIPTION))
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle(Intent.GetStringExtra(EDESCRIPTION));
                dialog.SetMessage("Here");
                dialog.Show();

                string foodDescription = Intent.GetStringExtra("foodDescription");
                item.setDescription(foodDescription);
            }
            //Adds item image
            if (Intent.HasExtra(EIMAGE))
            {
                int itemImage = Intent.GetIntExtra(EIMAGE, 10);
                //int intItemImage = Int32.Parse(itemImage);
                item.setImgId(itemImage);
            }
            //Adds item cost
            if (Intent.HasExtra(ECOST))
            {
                Decimal itemCost = decimal.Parse(Intent.GetStringExtra(ECOST));
                item.setCost(itemCost);
            }*/
        }

        //Connects variables to the elements in the .axml
        private void connectElements()
        {
            //Connect variables to elements
            description = FindViewById<TextView>(Resource.Id.description);
            addButton = FindViewById<Button>(Resource.Id.addButton);
            image = FindViewById<ImageView>(Resource.Id.image);
            price = FindViewById<TextView>(Resource.Id.price);
            name = FindViewById<TextView>(Resource.Id.foodName);

            //Update .xaml values with menuItem values
            string b = item.getDescription();
            description.Text = item.getDescription();
            price.Text = "$" + item.itemCost;


            name.Text = item.getFoodName();
            var imageView = FindViewById<ImageView>(Resource.Id.image);
            imageView.SetImageResource(item.imageID);
        }


        // action delagation, setting new adapter and sting up the views
        /*
        private void connectActions()
        {
            viewOrder.Click += ViewOrder_Click;

            ArrayAdapter<string> theAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, testFoodItems);



            itemListView.Adapter = theAdapter;
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
        */

        /*
         * Jordans Code - Adds the item, & start 'OrderTableAct'
         *                Slightly modified, removed parts used for the list, since they're not required here
         */
        private void AddButton_Click(object sender, EventArgs e)
        {
            data.addItem(item);    //updated item info
            //stAdapter.NotifyDataSetChanged(); //sends signal to list that it should refresh the data 

            //Start new activity
            var ordertableact = new Android.Content.Intent(this, typeof(OrderTableAct));

            //Extras
            //Intent.PutExtra(ENAME, item.getFoodName());
          
            //Intent.PutExtra(ENAME, item.getFoodName());
            //Intent.PutExtra(EDESCRIPTION, item.getDescription());
            // Intent.PutExtra(EIMAGE, "placeholder");     //No image in menuItem object, placeholder string
            // Intent.PutExtra(ECOST, item.getItemCost().ToString());

            StartActivity(ordertableact);
            Finish(); //returns to full menu after add
        }
    }
}