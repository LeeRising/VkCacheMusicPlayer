using System.Collections.Generic;

namespace VkMusicPlayer.Helper
{
    public static class DataHolder
    {
        public static List<SongModel> SongLists { get; set; } = new List<SongModel>();
        public static SongModel Song { get; set; } = new SongModel();
        public static readonly string VkDbPath = "/data/data/com.vkontakte.android/databases/databaseVerThree.db";
    }
}