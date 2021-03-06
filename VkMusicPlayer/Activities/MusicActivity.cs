﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;

namespace VkMusicPlayer
{
    [Activity(Label = "My Music")]
    public class MusicActivity : AppCompatActivity
    {
        private ListView _musicListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            try
            {
                ShowMenuDialog();
                SearchBar();
            }
            catch (Exception)
            {
                new AlertDialog.Builder(this)
                    .SetMessage("Something went wrong!")
                    .SetCancelable(false)
                    .SetPositiveButton("Ok", (sender, e) =>
                    {
                        Finish();
                    });
            }
        }
        private void ShowMenuDialog()
        {
            _musicListView = FindViewById<ListView>(Resource.Id.MusicLv);
            _musicListView.Adapter = new MusicAdapter(this, DataHolder.SongLists);
            _musicListView.ItemClick += (sender, e) =>
            {
                var ft = FragmentManager.BeginTransaction();
                ft.AddToBackStack(null);
                var prev = FragmentManager.FindFragmentByTag("music_dialog");
                if (prev != null)
                    ft.Remove(prev);
                var getAdapter = _musicListView.Adapter;
                var track = (getAdapter as MusicAdapter)?[e.Position];
                if (track != null) new MenuDialogFramgent(this, track.Position).Show(ft, "music_dialog");
            };
            DataHolder.PlayLists = DataHolder.NotShuffleList = DataHolder.SongLists;
        }

        private void SearchBar()
        {
            var searchText = FindViewById<EditText>(Resource.Id.SearchText);
            var searchList = DataHolder.SongLists;
            searchText.TextChanged += delegate
                {
                    Task.Run(() =>
                        {
                            if (searchText.Length() > 0)
                                searchList = DataHolder.SongLists
                                    .Where(x => x.Title.ToLower().Contains(searchText.Text.ToLower()) ||
                                                x.Artist.ToLower().Contains(searchText.Text.ToLower())).ToList();
                            else
                                searchList = DataHolder.SongLists;
                            DataHolder.PlayLists = DataHolder.NotShuffleList = searchList;
                        })
                    .ContinueWith(task =>
                        {
                            RunOnUiThread(() =>
                            {
                                _musicListView.Adapter = new MusicAdapter(this, searchList);
                            });
                        });
                };
            FindViewById<IconifyTextView>(Resource.Id.ClearSearch).Click += delegate { searchText.Text = string.Empty; };
        }
    }
}