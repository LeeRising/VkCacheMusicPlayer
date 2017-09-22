using System.IO;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Support.V7.App;

namespace VkMusicPlayer
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Directory.Exists(DataHolder.CachePath))
                new LoadMusicTask(this).Execute();
            else
            {
                var intent = new Intent(Intent.ActionView);
                var uri = Uri.Parse("/storage/emulated/0/");
                intent.SetDataAndType(uri, "resource/folder");
                StartActivity(intent);
            }
        }
    }
}

