using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;

namespace VkMusicPlayer
{
    public class DataHolder
    {
        public static List<saved_track> SongLists = new List<saved_track>();
        public static readonly string VkDbPath = "/data/data/com.vkontakte.android/databases/databaseVerThree.db";
        public static readonly string PersonalPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "VkSong.db");
        public static readonly string CachePath = "/storage/emulated/0/Android/data/com.vkontakte.android/files/Music";
        public static readonly string AndroidDataPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,"Android/data/com.canary.vkplayer/VkSong.db");
        public static readonly string DataBasePath = "/data/data/com.canary.vkplayer/database";
        public static List<Intent> IntentsList = new List<Intent>();
    }
}