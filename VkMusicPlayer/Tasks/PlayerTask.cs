using System;
using Android.OS;

namespace VkMusicPlayer
{
    public class PlayerTask : AsyncTask<string,string,string>
    {
        private readonly PlayerActivity _context;

        public PlayerTask(PlayerActivity context)
        {
            _context = context;
        }

        protected override string RunInBackground(params string[] @params)
        {
            try
            {
                _context.Player.SetDataSource(@params[0]);
                _context.Player.Prepare();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "";
        }
    }
}