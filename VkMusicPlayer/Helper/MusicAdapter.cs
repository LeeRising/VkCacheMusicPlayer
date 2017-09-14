using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using VkMusicPlayer.Activities;

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
                convertView = LayoutInflater.From(_context).Inflate(Resource.Layout.music_list_layout, null,false);
            var artistTextView = convertView.FindViewById<TextView>(Resource.Id.ArtistTv);
            var songTextView = convertView.FindViewById<TextView>(Resource.Id.SongTv);
            var songPlayBtn = convertView.FindViewById<TextView>(Resource.Id.PlayBtn);
            songPlayBtn.Typeface = Typeface.CreateFromAsset(_context.Assets, "fonts/materialdesignicons.ttf");
            artistTextView.Text = _songList[position].Artist;
            songTextView.Text = _songList[position].Title;
            songPlayBtn.Click += (sender, e) =>
            {
                _intent.PutExtra("position", position);
                _context.StartActivity(_intent);
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