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
using Android.Media;

namespace customer_client
{
    [Activity(Label = "soundManager")]

    public class soundManager : Activity
    {
        MediaPlayer _player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _player = MediaPlayer.Create(this, Resource.Drawable.BasicDing);
        }
    }
}