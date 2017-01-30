using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace customer_client
{
    [Activity(Label = "CIBUS", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button login;
        private EditText emailAddress;
        private EditText password;
        private Button guestLogin;
        private ImageButton facebook;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            //Test

            findViews();
            //clickHandler();
            login.Click += delegate
            {
                //Get entered username & password
                string usr = emailAddress.Text;
                string pass = password.Text;

                //Get list of valid user-pass pairs
                string[,] users = getValidUsers();

                if( credentialsAreValid( usr,pass,users ) )
                {
                    submit(usr);
                }
                else
                {
                    //Tell the user that the login didn't work
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Invalid Login");
                    dialog.SetMessage("The username-password pair you provided is invalid");
                    dialog.Show();
                }
            };

            //Guest Login
            guestLogin.Click += delegate
            {
                //Skips validation, submits w/ guest username
                submit("Guest@test.com");
            };

            //Facebook Login
            facebook.Click += delegate
            {
                //Feature not available dialog
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Functionality in Development");
                dialog.SetMessage("Currently unavailable");
                dialog.Show();
            };
        }

        private bool credentialsAreValid(string usr, string pass, string[,] list)
        {
            bool result = false;

            for (int x = 0; x < list.GetLength(0); x += 1)
            {
                if( list[x,0].Equals(usr) && list[x,1].Equals(pass) )   //If usr-pass pair matches...
                {
                    result = true;
                }
            }

            return result;
        }

        //Contains hard code. Replace it with real code when possible
        private string[,] getValidUsers()
        {
            //Initialize 2D string array w/ [x,0]=usr & [x,1]=pass
            string[,] list = new string[6,2];

            //Begin hard code
            int i = 0;
            while (i<6)
            {
                //list[1, 0] = String.Concat("user1");
                list[i,0] = String.Concat("user", i.ToString());
                list[i,1] = String.Concat("pass", i.ToString());

                i++;
            }
            //End hard code

            return list;
        }

        private void submit(string user)
        {
            var gettableactivity = new Android.Content.Intent(this, typeof(GetTableActivity));
            //extras here
            char[] delimiterChar = { '@' };
            string[] usernameDelim = user.Split(delimiterChar);
            gettableactivity.PutExtra("username", usernameDelim[0]);
            Console.WriteLine(usernameDelim);
            StartActivity(gettableactivity);
        }

        private void findViews()
        {
            login = FindViewById<Button>(Resource.Id.login);
            emailAddress = FindViewById<EditText>(Resource.Id.emailAddress);
            password = FindViewById<EditText>(Resource.Id.password);
            guestLogin = FindViewById<Button>(Resource.Id.guestLogin);
            facebook = FindViewById<ImageButton>(Resource.Id.facebook);
            //throw new NotImplementedException();//
        }
    }
}

