using Java.IO;
using Console = System.Console;

namespace VkMusicPlayer
{
    public static class MusicEncoder
    {
        private static readonly int[] Mask = { 0x0D, 0x1E, 0x2F, 0x40, 0x51, 0x62, 0x73, 0x84, 0x95, 0xA6, 0xB7, 0xC8, 0xD9, 0xEA, 0xFB, 0x0C };
        public static void ProcessBytes(string encodedPath,string fileName)
        {
            var strByte = 0;
            using (var encodedFile = new File(encodedPath))
            {
                try
                {
                    using (var finStream = new FileInputStream(encodedFile))
                    {
                        var buffer = new byte[finStream.Available()];
                        finStream.Read(buffer, 0, finStream.Available());
                        for (var i = 0; i < buffer.Length; i++)
                        {
                            buffer[i] ^= (byte)Mask[strByte];
                            Mask[strByte] += 0x10;
                            if (strByte < 15)
                                strByte++;
                            else
                                strByte = 0;
                        }
                        using (var writer = new FileOutputStream(DataHolder.CachePath + $"/{fileName}.mp3"))
                            writer.Write(buffer);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}