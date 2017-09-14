using Android.Content;
using Android.Graphics;

namespace VkMusicPlayer
{
    public class IconifySet
    {
        public static Typeface GetIcon(string path, Context context) => Typeface.CreateFromAsset(context.Assets, path);
    }
}