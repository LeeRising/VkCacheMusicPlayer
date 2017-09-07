using System;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Object = Java.Lang.Object;
using SQLite;
using VkMusicPlayer.Helper;

namespace VkMusicPlayer.Utils.Tasks
{
    public class LoadMusicTask : AsyncTask
    {
        private readonly Context _context;
        private ProgressDialog _progressDialog;
        public LoadMusicTask(Context context)
        {
            _context = context;
        }

        protected override void OnPreExecute()
        {
            base.OnPreExecute();
            _progressDialog = ProgressDialog.Show(_context,"Load","Cache check");
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            ReadDataFromDb();
            return true;
        }

        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);
            _progressDialog.Hide();
            (_context as AppCompatActivity)?.Finish();
            _context.StartActivity(typeof(MusicActivity));
        }

        private async void ReadDataFromDb()
        {
            try
            {
                var vkDb = new SQLiteAsyncConnection(DataHolder.VkDbPath,SQLiteOpenFlags.ReadOnly);
                //DataHolder.Song = await vkDb.ExecuteScalarAsync<SongModel>("SELECT artist,title,lyrics_text,position,file FROM saved_track WHERE _id=490");
                DataHolder.Song.artist = await vkDb.ExecuteScalarAsync<string>("SELECT artist FROM saved_track WHERE _id=1");
            }
            catch (Exception e)
            {
                var ex = e;
            }
        }
    }
}