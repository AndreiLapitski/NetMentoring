using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.MainActivity);

            Button button = FindViewById<Button>(Resource.Id.button);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            EditText editText = FindViewById<EditText>(Resource.Id.editText);

            button.Click += (object sender, EventArgs e) =>
            {
                Toast.MakeText(this, "Hello, " + editText.Text, ToastLength.Long).Show();
            };

            button2.Click += (object sender, EventArgs e) =>
            {
                Toast.MakeText(this, SpeakerClassLibrary.Speaker.SayHelloNow(editText.Text) , ToastLength.Long).Show();
            };

        }
    }
}