using System;
using System.Collections.Generic;
using System.IO;

namespace VkMusicPlayer
{
    public static class DataHolder
    {
        public static List<saved_track> SongLists = new List<saved_track>();
        public static readonly string VkDbPath = "/data/data/com.vkontakte.android/databases/databaseVerThree.db";
        public static readonly string AppDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "VkSong.db");
    }
}