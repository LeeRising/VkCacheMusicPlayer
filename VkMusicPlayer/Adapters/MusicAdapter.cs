using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace VkMusicPlayer
{
    public class MusicAdapter : BaseAdapter<saved_track>
    {
        private readonly Context _context;
        private readonly List<saved_track> _songList;
        private readonly Intent _intent;

        public MusicAdapter(Context context, List<saved_track> songList)
        {
            _context = context;
            _songList = songList;
            _intent = new Intent(_context, typeof(PlayerActivity));
        }

        public override saved_track this[int position] => _songList[position];

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.main_music_layout, null, false);
            var artistTextView = convertView.FindViewById<TextView>(Resource.Id.ArtistTv);
            var songTextView = convertView.FindViewById<TextView>(Resource.Id.SongTv);
            var songPlayBtn = convertView.FindViewById<IconifyTextView>(Resource.Id.PlayBtn);
            artistTextView.Text = _songList[position].Artist;
            songTextView.Text = _songList[position].Title;
            songPlayBtn.Click += (sender, e) =>
            {
                DataHolder.Possition = position;
                if (DataHolder.IntentsList.Contains(_intent)) return;
                _context.StartActivity(_intent);
                DataHolder.IntentsList.Add(_intent);
            };
            return convertView;
        }

        public override int Count => _songList.Count;
    }
}