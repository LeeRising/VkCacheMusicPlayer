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
        private readonly int _position;
        private readonly AppCompatActivity _activity;

        public MenuDialogFramgent(AppCompatActivity activity, int position)
        {
            _activity = activity;
            _position = position;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.music_menu_fragment, container, false);
            _menuList = view.FindViewById<ListView>(Resource.Id.MenuListView);
            _menuList.ItemClick += (sender, e) =>
            {
                if (e.Id == 0)
                {
                    if (DataHolder.SongLists[_position].Lyrics_text != null)
                    {
                        var intent = new Intent(Activity, typeof(LyricsActivity));
                        intent.PutExtra("Lyrics", DataHolder.SongLists[_position].Lyrics_text);
                        intent.PutExtra("Title", DataHolder.SongLists[_position].Title);
                        StartActivity(intent);
                    }
                    else
                        Toast.MakeText(_activity, "No lyrics for this song!", ToastLength.Long).Show();
                    Dialog.Dismiss();
                }
                if (e.Id == 1)
                {
                    var sharingIntent = new Intent(Intent.ActionSend);
                    sharingIntent.SetType("text/plain");
                    sharingIntent.PutExtra(Intent.ExtraSubject, "Listen this incredible music");
                    sharingIntent.PutExtra(Intent.ExtraText, $"{DataHolder.SongLists[_position].Artist} - {DataHolder.SongLists[_position].Title}");
                    sharingIntent.PutExtra(Intent.ExtraTitle, "Send music");
                    StartActivity(Intent.CreateChooser(sharingIntent,"Sharing options"));
                }
            };
            Dialog.SetTitle("Music menu");
            return view;
        }
    }
}