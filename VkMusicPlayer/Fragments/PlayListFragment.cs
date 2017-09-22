using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace VkMusicPlayer
{
    public class PlayListFragment : DialogFragment
    {
        private ListView _playListView;
        private readonly AppCompatActivity _activity;
        public PlayListFragment(AppCompatActivity activity)
        {
            _activity = activity;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.play_list_fragment, container,false);
            _playListView = view.FindViewById<ListView>(Resource.Id.PlayListView);
            _playListView.Adapter = new PlayListAdapter(_activity,DataHolder.PlayLists);
            _playListView.SetSelection(DataHolder.Possition);
            _playListView.ItemClick += (s,e)=>
            {
                DataHolder.Possition = e.Position;
                Dismiss();
            };
            return view;
        }
    }
}