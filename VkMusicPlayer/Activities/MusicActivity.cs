using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;

namespace VkMusicPlayer
{
    [Activity(Label = "My Music")]
    public class MusicActivity : AppCompatActivity
    {
        private ListView _musicListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            try
            {
                _musicListView = FindViewById<ListView>(Resource.Id.MusicLv);
                _musicListView.Adapter = new MusicAdapter(this, DataHolder.SongLists);
                _musicListView.ItemClick += (sender, e) =>
                {
                    var ft = FragmentManager.BeginTransaction();
                    var prev = FragmentManager.FindFragmentByTag("music_dialog");
                    if (prev != null)
                        ft.Remove(prev);
                    ft.AddToBackStack(null);
                    new MenuDialogFramgent(e.Position, null).Show(ft, "music_dialog");
                };
            }
            catch (Exception)
            {
                new AlertDialog.Builder(this)
                    .SetMessage("Something went wrong!")
                    .SetCancelable(false)
                    .SetPositiveButton("Ok", (sender, e) =>
                    {
                        Finish();
                    });
            }
        }
    }
}