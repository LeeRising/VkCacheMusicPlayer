using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace VkMusicPlayer
{
    public class MenuDialogFramgent : DialogFragment
    {
        private readonly int _position;

        public MenuDialogFramgent(int position , Bundle bundle)
        {
            _position = position;
            Arguments = bundle;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.music_menu_fragment,container,false);
            var tv = view.FindViewById<TextView>(Resource.Id.TestTv);
            tv.Text = DataHolder.SongLists[_position].Title;
            Dialog.SetTitle("Music menu");
            return view;
        }
    }
}