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
    [Activity(Label = "resSelectDiffAct")]
    public class resSelectDiffAct : Activity
    {
        public int resId;
        public Button selectBut;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.resSelect);

            FindViews();
            // Create your application here

            selectBut.Click += delegate
            {
                var testActivity = new Android.Content.Intent(this, typeof(MainActivity));
                StartActivity(testActivity);
            };
        }

        private void FindViews()
        {
            selectBut = FindViewById<Button>(Resource.Id.selectResBut);
        }
    }
}