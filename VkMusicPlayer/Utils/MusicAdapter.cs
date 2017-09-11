using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using VkMusicPlayer.Activities;

namespace VkMusicPlayer
{
    public class MusicAdapter : BaseAdapter<saved_track>
    {
        private readonly Context _context;
        private readonly List<saved_track> _songList;

        public MusicAdapter(Context context, List<saved_track> songList)
        {
            _context = context;
            _songList = songList;
        }

        public override saved_track this[int position] => _songList[position];

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.music_list_layout, null,false);
            var artistTextView = convertView.FindViewById<TextView>(Resource.Id.ArtistTv);
            var songTextView = convertView.FindViewById<TextView>(Resource.Id.SongTv);
            var songPlayBtn = convertView.FindViewById<ImageView>(Resource.Id.PlayBtn);
            artistTextView.Text = _songList[position].Artist;
            songTextView.Text = _songList[position].Title;
            songPlayBtn.Click += (sender, e) =>
            {
                var intent = new Intent(_context,typeof(PlayerActivity));
                intent.PutExtra("Position", position.ToString());
                _context.StartActivity(intent);
            };
            return convertView;
        }

        public saved_track GetTrack(int position)
        {
            return _songList[position];
        }

        public override int Count => _songList.Count;
    }
}