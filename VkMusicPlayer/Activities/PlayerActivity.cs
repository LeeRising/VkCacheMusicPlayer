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
    }
}