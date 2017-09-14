using System;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Java.Lang;
using Object = Java.Lang.Object;
using SQLite;
using Exception = System.Exception;

namespace VkMusicPlayer
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
            _progressDialog = ProgressDialog.Show(_context, "Load", "Cache check");
        }

        protected override Object DoInBackground(params Object[] @params)
        {
            if (!IsCopyFile()) return false;
            try
            {
                Console.WriteLine(DataHolder.AndroidDataPath);
                var vkDb = new SQLiteConnection(DataHolder.AndroidDataPath);
                DataHolder.SongLists = vkDb.Table<saved_track>().ToList().OrderBy(x => x.Position).ToList();
            }
            catch (Exception)
            {
                LoadCacheMusic();
            }
            return true;
        }

        protected override void OnPostExecute(Object result)
        {
            base.OnPostExecute(result);
            _progressDialog.Hide();
            (_context as Activity)?.Finish();
            _context.StartActivity(typeof(MusicActivity));
        }

        private static bool IsCopyFile()
        {
            try
            {
                var p = Runtime.GetRuntime();
                var command = new[] { "su", "-c", $"cp -a {DataHolder.VkDbPath} {DataHolder.AndroidDataPath}" };
                p.Exec(command).WaitFor();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void LoadCacheMusic()
        {
            var musics = Directory.GetFiles(DataHolder.CachePath);
            var i = 0;
            foreach (var music in musics)
                DataHolder.SongLists.Add(new saved_track
                {
                    Artist = Path.GetFileName(music),
                    File = music,
                    Position = i++,
                    Lyrics_text = i++.ToString()
                });
        }
    }
}