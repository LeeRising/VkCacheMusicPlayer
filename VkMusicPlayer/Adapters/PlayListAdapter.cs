using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace VkMusicPlayer
{
    public class PlayListAdapter : BaseAdapter<saved_track>
    {
        private readonly Context _context;
        private readonly List<saved_track> _songList;
        public PlayListAdapter(Context context, List<saved_track> songList)
        {
            _context = context;
            _songList = songList;
        }

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            convertView = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.play_list_layout, null,false);
            var artist = convertView.FindViewById<TextView>(Resource.Id.PlayListArtistTv);
            var title = convertView.FindViewById<TextView>(Resource.Id.PlayListSongTv);
            artist.Text = _songList[position].Artist;
            title.Text = _songList[position].Title;
            convertView.SetBackgroundColor(position == DataHolder.Possition ? Color.Azure : Color.White);
            return convertView;
        }

        public override int Count => _songList.Count;

        public override saved_track this[int position] => _songList[position];
    }
}