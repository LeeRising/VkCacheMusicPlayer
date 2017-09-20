using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace VkMusicPlayer
{
    [Activity(Label = "Music Player", ParentActivity = typeof(MusicActivity))]
    public class PlayerActivity : AppCompatActivity
    {
        public MediaPlayer Player => new MediaPlayer();
        private int _position;
        private TextView _songName;
        private IconifyTextView _playPuase, _next, _previos, _shuffle, _playList, _replay;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Player);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ComponentInit();
            _position = Intent.GetIntExtra("position", 0);
            _songName.Text = $"{DataHolder.SongLists[_position].Artist} - {DataHolder.SongLists[_position].Title}";
            //PlayMusic(_position);
            SendAudioCommand(StreamingBackgroundService.ActionPlay);
            _playPuase.Click += delegate
            {
                var pauseLabel = GetString(Resource.String.pause);
                var playLabel = GetString(Resource.String.play);
                if (_playPuase.Text == pauseLabel)
                {
                    _playPuase.Text = playLabel;
                    Player.Pause();
                }
                if (_playPuase.Text == playLabel)
                {
                    _playPuase.Text = pauseLabel;
                    Player.Start();
                }
            };
        }

        private void SendAudioCommand(string action)
        {
            var intent = new Intent(action);
            StartService(intent);
        }

        protected override void OnStop()
        {
            base.OnStop();
            var intent = DataHolder.IntentsList[0];
            if (intent != null) DataHolder.IntentsList.Remove(intent);
        }

        public IList<T> Shuffle<T>(IList<T> list)
        {
            var shuffList = list;
            var rand = new Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                var value = shuffList[k];
                shuffList[k] = shuffList[n];
                shuffList[n] = value;
            }
            return shuffList;
        }

        private void ComponentInit()
        {
            _songName = FindViewById<TextView>(Resource.Id.SongNameTv);
            _playPuase = FindViewById<IconifyTextView>(Resource.Id.PlayPauseBtn);
            _next = FindViewById<IconifyTextView>(Resource.Id.NextBtn);
            _previos = FindViewById<IconifyTextView>(Resource.Id.PreviousBtn);
            _shuffle = FindViewById<IconifyTextView>(Resource.Id.ShuffleBtn);
            _playList = FindViewById<IconifyTextView>(Resource.Id.PlayListBtn);
            _replay = FindViewById<IconifyTextView>(Resource.Id.ReplayBtn);

            Player.SetAudioStreamType(Stream.Music);
        }

        private void PlayMusic(int position)
        {
            MusicEncoder.ProcessBytes(DataHolder.SongLists[position].File);
            Player.SetDataSource(DataHolder.CachePath + "/tmp.mp3");
            Player.Start();
        }
    }
}