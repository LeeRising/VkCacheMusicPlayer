using System;
using System.Collections.Generic;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace VkMusicPlayer.Activities
{
    [Activity(Label = "Music Player", ParentActivity = typeof(MusicActivity))]
    public class PlayerActivity : AppCompatActivity
    {
        private static MediaPlayer Player=>new MediaPlayer();
        private int _position;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Player);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            _position = Intent.GetIntExtra("position",0);
            FindViewById<TextView>(Resource.Id.SongNameTv).Text = $"{DataHolder.SongLists[_position].Artist} - {DataHolder.SongLists[_position].Title}";
            MusicEncoder.ProcessBytes(DataHolder.SongLists[_position].File);
            //MusicPlayerInit();
        }

        protected override void OnStop()
        {
            base.OnStop();
            var intent = DataHolder.IntentsList[0];
            if (intent != null) DataHolder.IntentsList.Remove(intent);
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
            try
            {
                Player.SetAudioStreamType(Stream.Music);
                Player.Prepared += (sender, args) => Player.Start();
                Player.Completion += (sender, args) => Player.Stop();
                Player.Error += (sender, args) =>
                {
                    Console.WriteLine("Error in playback resetting: " + args.What);
                    Player.Stop();
                };
                await Player.SetDataSourceAsync(DataHolder.CachePath + "/tmp.mp3");
                Player.PrepareAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}