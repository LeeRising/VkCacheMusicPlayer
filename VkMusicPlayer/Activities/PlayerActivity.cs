using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace VkMusicPlayer.Activities
{
    [Activity(Label = "Music Player",ParentActivity = typeof(MusicActivity))]
    public class PlayerActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Player);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            var tv = FindViewById<TextView>(Resource.Id.playerTest);
            tv.Text = Intent.GetStringExtra("Position") ?? "Data not available";
        }

        public void Shuffle<T>(IList<T> list)
        {
            var rand = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}