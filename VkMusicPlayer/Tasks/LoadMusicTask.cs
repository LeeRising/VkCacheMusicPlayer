using Android.App;
using Android.Content;
using Android.OS;
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
            _context.StartActivity(typeof(MusicActivity));
        }

        private async void ReadDataFromDb()
        {
            var vkDb = new SQLiteAsyncConnection(DataHolder.VkDbPath);
            DataHolder.SongLists = await vkDb.QueryAsync<SongModel>("SELECT artist,title,lyrics_text,position,file FROM saved_track");
        }
    }
}