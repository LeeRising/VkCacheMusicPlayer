using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace VkMusicPlayer
{
    public class MusicAdapter : BaseAdapter<SongModel>
    {
        private readonly Context _context;
        private readonly List<SongModel> _songList;

        public MusicAdapter(Context context, List<SongModel> songList)
        {
            _context = context;
            _songList = songList;
        }

        public override SongModel this[int position] => _songList[position];

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.music_list_layout, parent);
            var artistTextView = (TextView) view.FindViewById(Resource.Id.ArtistTv);
            var songTextView = (TextView)view.FindViewById(Resource.Id.SongTv);
            artistTextView.Text = _songList[position].artist;
            songTextView.Text = _songList[position].title;
            return view;
        }

        public override int Count => _songList.Count;
    }
}