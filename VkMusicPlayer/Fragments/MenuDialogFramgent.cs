using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace VkMusicPlayer
{
    public class MenuDialogFramgent : DialogFragment
    {
        private ListView _menuList;
        private string _lyricsText;
        private string _title;
        private AppCompatActivity _activity;

        public MenuDialogFramgent(AppCompatActivity activity, string lyricsText, string title)
        {
            _activity = activity;
            _lyricsText = lyricsText;
            _title = title;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.music_menu_fragment, container, false);
            _menuList = view.FindViewById<ListView>(Resource.Id.MenuListView);
            _menuList.ItemClick += (sender, e) =>
            {
                if (e.Id == 0)
                {
                    if (_lyricsText != null)
                    {
                        var intent = new Intent(Activity, typeof(LyricsActivity));
                        intent.PutExtra("Lyrics", _lyricsText);
                        intent.PutExtra("Title", _title);
                        StartActivity(intent);
                    }
                    else
                        Toast.MakeText(_activity, "No lyrics for this song!", ToastLength.Long).Show();
                    Dialog.Dismiss();
                }
            };
            Dialog.SetTitle("Music menu");
            return view;
        }
    }
}