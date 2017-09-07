﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using VkMusicPlayer.Utils.Tasks;

namespace VkMusicPlayer
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            new LoadMusicTask(this).Execute();
        }
    }
}

