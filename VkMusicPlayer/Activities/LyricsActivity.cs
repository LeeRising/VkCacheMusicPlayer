using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace VkMusicPlayer
{
    [Activity(ParentActivity = typeof(MusicActivity))]
    public class LyricsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.SetDefaultDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.Lyrics);
            var lyricsText = FindViewById<TextView>(Resource.Id.LyricsText);
            lyricsText.Text = Intent.GetStringExtra("Lyrics");
            Title = Intent.GetStringExtra("Title");
        }
    }
}