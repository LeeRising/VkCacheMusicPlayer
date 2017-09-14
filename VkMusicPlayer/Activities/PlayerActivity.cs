using System;
using System.Collections.Generic;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;

namespace VkMusicPlayer.Activities
{
    [Activity(Label = "Music Player", ParentActivity = typeof(MusicActivity))]
    public class PlayerActivity : AppCompatActivity
    {
        private MediaPlayer _player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Player);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            MusicEncoder.ProcessBytes(Intent.GetStringExtra("File"));
            MusicPlayerInit();
        }

        public void Shuffle<T>(IList<T> list)
        {
            var rand = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private async void MusicPlayerInit()
        {
            if (_player != null)
                _player = new MediaPlayer();
            _player.SetAudioStreamType(Stream.Music);
            _player.Prepared += (sender, args) => _player.Start();
            _player.Completion += (sender, args) => _player.Stop();
            _player.Error += (sender, args) =>
            {
                Console.WriteLine("Error in playback resetting: " + args.What);
                _player.Stop();
            };
            await _player.SetDataSourceAsync(DataHolder.CachePath + "/tmp.mp3");
            _player.PrepareAsync();
        }
    }
}