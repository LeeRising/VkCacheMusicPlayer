using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using VkMusicPlayer.Helper;

namespace VkMusicPlayer
{
    [Activity(Label = "My Music")]
    public class MusicActivity : AppCompatActivity
    {
        private ListViewCompat _musicListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            _musicListView = (ListViewCompat) FindViewById(Resource.Id.MusicLv);
            _musicListView.Adapter = new MusicAdapter(this,DataHolder.SongLists);
        }
    }
}