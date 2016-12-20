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


using Android.Media;

namespace customer_client
{
    [Activity(Label = "Your Order")]
    public class OrderTableAct : Activity
    {

        private dataAccess data = dataAccess.getInstance();
        private ListView itemTable;
        private adapter stAdapter; // data adapter for stored items
        private sqlMenuListViewAdapter theAdapter;
        private EditText total;
        private Button dingbutton;
        private Button backToMenu;
        public decimal totalPrice;
        //private EditText itemNameEditText;









        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.orderTable);
            //avoid automaticaly appear of android keyboard when activitry starts
            Window.SetSoftInputMode(SoftInput.StateHidden);

            // Create your application here
            //Intent theIntent = getIntent();
            //Bundle theBundle =


            //if(Intent.GetStringExtra("menuItemName" == null) 
            //{


            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Menu Info");
            //dialog.SetMessage(menuItemName);
            //dialog.Show();

            //Activity currActvity = currentActi
            //Activity currContext = CrossCurr

            loadViews();

            string theTotal = Convert.ToString(itemTotal(0));


            total.Text = "Current Total is:  $" + theTotal;
            rando();

                
                
            

            
            //connectActions();

            


        }

        private void rando()
        {
            if (Intent.HasExtra("menuItemName"))
            {
                string menuItemName = Intent.GetStringExtra("menuItemName");
                this.Title = menuItemName;

                //stAdapter = new adapter(this);
                theAdapter = new sqlMenuListViewAdapter(this);

                itemTable.Adapter = theAdapter;
                itemTable.FastScrollEnabled = true;
                data.addItem(new menuItem(menuItemName));
                //stAdapter.NotifyDataSetChanged();
                theAdapter.NotifyDataSetChanged();
                itemTable.ItemClick += itemTable_ItemClick;
                itemTable.ItemLongClick += itemTable_ItemLongClick;

            }
            else
            {
                theAdapter = new sqlMenuListViewAdapter(this);
                itemTable.Adapter = theAdapter;
                itemTable.FastScrollEnabled = true;
                itemTable.ItemClick += itemTable_ItemClick;
                itemTable.ItemLongClick += itemTable_ItemLongClick;
            }

            backToMenu.Click += delegate //return to menu
            {
                    Finish(); //complete activity
               
            };

            dingbutton.Click += delegate //Finish buttoon press
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirm");
                dialog.SetMessage("Are you sure you want to submit your order?");
                dialog.SetPositiveButton("Yes",//confirm that the user wants to submit the order
                    (senderAlert, args) =>
                    { // action for this button

                        completeOrder(); //runs method that dings, and posts message
                        Handler viewCompletionDetails = new Handler(); //handler & action built to delay completion of order by 3000ms
                        Action completeActivity = () =>
                        {
                            data.clearItems(); //clear items on order table
                            Finish(); //complete activity
                    };

                        viewCompletionDetails.PostDelayed(completeActivity, 3000);
                    }
                    );
            
                dialog.SetNegativeButton("No", (senderAlert, args) => { }); //return to order

                dialog.Show();
                
            };

        }

        private void loadViews()
        {   //the list view with all data get from sqlite
            itemTable = FindViewById<ListView>(Resource.Id.orderTable);
            //button plays a sound
            dingbutton = FindViewById<Button>(Resource.Id.dingBut);

            backToMenu = FindViewById<Button>(Resource.Id.backToMenu);
            //calc total from item price
            total = FindViewById<EditText>(Resource.Id.total);
        }






        //Will just display an alert of all the student info
        private void itemTable_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //var dialog1 = new AlertDialog.Builder(this);
            //dialog1.SetTitle("Delete menu item");
            //dialog1.SetMessage("ID is: ");
            //dialog1.Show();
            
            menuItem selectedSt = theAdapter[e.Position];

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Item Information");
            dialog.SetMessage("Order Number:  "+selectedSt.ID + "\r\n" + "\r\n" + "Name:  "+ selectedSt.foodName + "\r\n" + "Priced at:  $" + selectedSt.itemCost);
        
            dialog.Show();
            
        }




        //Will ask if they want to remove the item
        private void itemTable_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            menuItem selectedSt = theAdapter[e.Position];//stAdapter[e.Position];
            //selectedSt.ID = 1;

            //var dialog1 = new AlertDialog.Builder(this);
            //dialog1.SetTitle("Delete menu item");
            //dialog1.SetMessage("ID is: ");
            //dialog1.Show();

            var dialog = new AlertDialog.Builder(this);
            dialog.SetTitle("Delete Menu Item");
            dialog.SetMessage("Order Number:  " + selectedSt.ID + "\r\n" + "\r\n" + "Name:  " + selectedSt.foodName + "\r\n" + "Priced at:  $" + selectedSt.itemCost);
            dialog.SetPositiveButton("Delete",
                (senderAlert, args) =>
                { // action for this button
                    data.deleteItemByID(selectedSt.ID);
                    theAdapter.NotifyDataSetChanged();
                    Toast.MakeText(this, "The menu item has been deleted", ToastLength.Short).Show();
                    total.Text = Convert.ToString("Total: $"+negaTotal(selectedSt.itemCost));
                }
                );
            dialog.SetNegativeButton("Cancel", (senderAlert, args) => { });

            dialog.Show();
            

            
        }
        //Added new method to subtract from total
        //paramter: string cost of "item to remove"
        private decimal negaTotal(string valueToRemove)
        {
            decimal subtract = Convert.ToDecimal(valueToRemove);
            List<menuItem> totalList = data.getAllItems();


                totalPrice -=subtract;

            return totalPrice;

        }

        //this hides the key board
        private void hideKeyBoard(Android.Views.View element)
        {
            InputMethodManager im = (InputMethodManager)GetSystemService(Context.InputMethodService);
            im.HideSoftInputFromWindow(element.WindowToken, 0);

        }




        //0 to add, 1 to subtract
        //this method does the total sum of item ordered, incomplete
        private decimal itemTotal(int operation) {

            List<menuItem> totalList = data.getAllItems();

            

            for (int index = 0; index < totalList.Count; index++) {
                string itemCostString = totalList[index].itemCost;

                if (operation == 0) {
                    totalPrice += Convert.ToDecimal(itemCostString);
                }
                else if (operation == 1)
                {
                    totalPrice -= Convert.ToDecimal(itemCostString);
                }
                
                    }


            string totalPriceBackToString = Convert.ToString(totalPrice);
            //decimal orderTotal = 10.99m; //set up
            //loop into sqlite and get menuItem prices
           
            return totalPrice;

        }
        public decimal completeTotal()
        {
            return totalPrice;
        }

        MediaPlayer _player; //Soundplaying class variable
        //press the button plays the sound and provides the total, incomplete 
        private void completeOrder() {

            decimal theTotal = completeTotal();
                var finishDialog = new AlertDialog.Builder(this);
                finishDialog.SetTitle("Finalize & Purchase Your Order");
                finishDialog.SetMessage("Final Total:  $" + theTotal + "\r\n" + "\r\n"+"Order Submitted.");
              


                //data.delete

                finishDialog.Show();

            // total.Text = "Current Total is " + theTotal;
            total.Text = "Current Total is being submitted";

            _player = MediaPlayer.Create(this, Resource.Drawable.BasicDing);
                _player.Start();
            //play sound

            //calculate the total



            




        }
        


    }
}