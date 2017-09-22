using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Uri = Android.Net.Uri;

namespace VkMusicPlayer
{
    [Activity(Label = "Music Player", ParentActivity = typeof(MusicActivity))]
    public class PlayerActivity : AppCompatActivity, MediaPlayer.IOnPreparedListener
    {
        private MediaPlayer _player;
        private TextView _songName;
        private IconifyTextView _playPuase, _next, _previos, _shuffle, _playList, _replay;
        private SeekBar _musickSeek;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Player);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            ComponentInit();
            ClickerInit();
            PlayMusic(DataHolder.Possition);
            //SendAudioCommand(StreamingBackgroundService.ActionPlay);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            var intent = DataHolder.IntentsList[0];
            if (intent != null) DataHolder.IntentsList.Remove(intent);
            _player?.Release();
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
            _musickSeek = FindViewById<SeekBar>(Resource.Id.MusicLineBar);
        }

        private void SendAudioCommand(string action)
        {
            var intent = new Intent(action);
            StartService(intent);
        }

        private void ClickerInit()
        {
            _playPuase.Click += delegate
            {
                var pauseLabel = GetString(Resource.String.pause);
                var playLabel = GetString(Resource.String.play);
                if (_playPuase.Text == pauseLabel)
                {
                    _playPuase.Text = playLabel;
                    _player?.Pause();
                }
                else if (_playPuase.Text != playLabel) return;
                _playPuase.Text = pauseLabel;
                _player?.Start();
            };
            _next.Click += delegate
            {
                PlayMusic(DataHolder.Possition++);
            };
            _previos.Click += delegate
            {
                PlayMusic(DataHolder.Possition--);
            };
            _replay.Click += delegate
            {
                _player?.SeekTo(0);
            };
            _shuffle.Click += delegate
            {
                var curentItem = DataHolder.PlayLists[DataHolder.Possition];
                if (_shuffle.TextColors != ColorStateList.ValueOf(Color.Blue))
                {
                    DataHolder.PlayLists = (List<saved_track>)Shuffle(DataHolder.PlayLists);
                    DataHolder.PlayLists.Remove(curentItem);
                    DataHolder.PlayLists.Insert(0, curentItem);
                    DataHolder.Possition = 0;
                    _shuffle.SetTextColor(Color.Blue);
                }
                else
                {
                    DataHolder.PlayLists = DataHolder.NotShuffleList;
                    DataHolder.Possition = DataHolder.NotShuffleList.IndexOf(curentItem);
                    _shuffle.SetTextColor(Color.Black);
                }
            };
            _playList.Click += delegate
            {
                var v = DataHolder.Possition;
                var ft = FragmentManager.BeginTransaction();
                ft.AddToBackStack(null);
                var prev = FragmentManager.FindFragmentByTag("play_list_dialog");
                if (prev != null)
                    ft.Remove(prev);
                new PlayListFragment(this).Show(ft, "play_list_dialog");
                if (v != DataHolder.Possition)
                    PlayMusic(DataHolder.Possition);
            };
        }

        private void UpdateSeekBar()
        {
            _musickSeek.Progress = (int)((float)_player.CurrentPosition / _player.Duration * 100);
            if (_player.IsPlaying)
            {
                Task.Run(() =>
                {
                    UpdateSeekBar();
                });
            }
        }

        private void PlayMusic(int position)
        {
            try
            {
                Task.Run(() =>
                {
                    if (DataHolder.Possition == -1)
                        DataHolder.Possition = DataHolder.PlayLists.Capacity - 1;
                    if (DataHolder.Possition == DataHolder.PlayLists.Capacity + 1)
                        DataHolder.Possition = 0;
                    _songName.Text = $"{DataHolder.PlayLists[DataHolder.Possition].Artist} - {DataHolder.PlayLists[DataHolder.Possition].Title}";
                    MusicEncoder.ProcessBytes(DataHolder.PlayLists[position].File,$"{DataHolder.PlayLists[DataHolder.Possition].Artist}-{DataHolder.PlayLists[DataHolder.Possition].Title}");
                }).ContinueWith(task =>
                {
                    RunOnUiThread(() =>
                    {
                        var uri = Uri.Parse(DataHolder.CachePath + $"/{DataHolder.PlayLists[DataHolder.Possition].Artist}-{DataHolder.PlayLists[DataHolder.Possition].Title}.mp3");
                        if (_player == null)
                        {
                            _player = MediaPlayer.Create(this, uri);
                            _player.SetAudioStreamType(Stream.Music);
                            _player.SetOnPreparedListener(this);
                        }
                        else
                        {
                            _player.Reset();
                            _player = MediaPlayer.Create(this, uri);
                        }
                        _playPuase.Text = GetString(Resource.String.pause);
                        UpdateSeekBar();
                    });
                });
            }
            catch (Exception e)
            {
                Toast.MakeText(this,e.ToString(),ToastLength.Long).Show();
                throw;
            }
        }

        public void OnPrepared(MediaPlayer mp)
        {
            mp.Start();
        }
    }
}