

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
    [Activity(Label = "QR")]
    public class QR : Activity
    {
        //Varibales
        private int restID;
        private int tableID;
        
        //Constructor
        public QR()
        {
            captureQR();

        }

        //Builds intent & calls it
        public void captureQR()
        {
            try
            {

                Intent intent = new Intent("com.google.zxing.client.android.SCAN");
                intent.PutExtra("SCAN_MODE", "QR_CODE_MODE"); // "PRODUCT_MODE for bar codes

                //StartActivityForResult(intent, 0);

            }
            catch (Exception e)
            {

                //This chunk is suposed to redirect the user to the store to get an appropriate QR app if they don't have one

                /*Uri marketUri = Uri.parse("market://details?id=com.google.zxing.client.android");
                Intent marketIntent = new Intent(Intent.ACTION_VIEW, marketUri);
                startActivity(marketIntent);*/

            }


        }

        //Recieves intent
        protected void onActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (requestCode == 0)
            {

                if (resultCode == /*RESULT_OK*/0)
                {
                    String contents = data.GetStringExtra("SCAN_RESULT");
                    //resultParse(contents);
                }
                if (resultCode == /*RESULT_CANCELED*/1)
                {
                    //handle cancel
                }
            }
        }

        private void resultParse(String input)
        {
            //Breaks the input string into 2 stings in array 'codes'
            string[] codes = input.Split(',');
            //Converts strings into ints, & saves them to the global vars
            restID = Int32.Parse(codes[0]);
            tableID = Int32.Parse(codes[1]);
        }

        public int getRestID()
        {
            return restID;
        }

        public int getTableID()
        {
            return tableID;
        }

    }
}