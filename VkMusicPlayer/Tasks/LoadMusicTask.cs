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
                var vkDb = new SQLiteConnection(DataHolder.AppDbPath);
                DataHolder.SongLists = vkDb.Table<saved_track>().ToList().OrderBy(x=> x.Position).ToList();
            }
            catch (Exception e)
            {
                var v = e;
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
                var command = new[] { "su", "-c", $"chmod 777 {DataHolder.VkDbPath}" };
                p.Exec(command).WaitFor();
                if (!CanRunRootCommands()) return true;
                var exist = File.Exists(DataHolder.AppDbPath);
                if (exist)
                {
                    var appDbBytes = File.ReadAllBytes(DataHolder.AppDbPath);
                    var vkDbBytes = File.ReadAllBytes(DataHolder.VkDbPath);
                    if (appDbBytes != vkDbBytes)
                    {
                        command = new[] { "su", "-c", $"rm {DataHolder.AppDbPath}" };
                        p.Exec(command).WaitFor();
                        exist = false;
                    }
                }
                if (exist) return true;
                command = new[] { "su", "-c", $"cp {DataHolder.VkDbPath} {DataHolder.AppDbPath}" };
                p.Exec(command).WaitFor();
                command = new[] { "su", "-c", $"chmod 777 {DataHolder.AppDbPath}" };
                p.Exec(command).WaitFor();
                return true;
            }
            catch (Exception e)
            {
                var v = e;
                return false;
            }
        }

        public static bool CanRunRootCommands()
        {
            var retval = false;
            Java.Lang.Process suProcess;
            try
            {
                suProcess = Runtime.GetRuntime().Exec("su");
                var os = new Java.IO.DataOutputStream(suProcess.OutputStream);
                var osRes = new Java.IO.DataInputStream(suProcess.InputStream);
                if (null != os && null != osRes)
                {
                    os.WriteBytes("id\n");
                    os.Flush();
                    string currUid = osRes.ReadLine();
                    bool exitSu = false;
                    if (null == currUid)
                    {
                        retval = false;
                        exitSu = false;
                        Console.WriteLine("Can't get root access or denied by user");
                    }
                    else if (true == currUid.Contains("uid=0"))
                    {
                        retval = true;
                        exitSu = true;
                        Console.WriteLine("Root access granted");
                    }
                    else
                    {
                        retval = false;
                        exitSu = true;
                        Console.WriteLine("Root access rejected: " + currUid);
                    }

                    if (exitSu)
                    {
                        os.WriteBytes("exit\n");
                        os.Flush();
                    }
                }
            }
            catch (Java.Lang.Exception e)
            {
                retval = false;
                Console.WriteLine("Root access rejected [" + e.Class.Name + "] : " + e.Message);
            }

            return retval;
        }
    }
}