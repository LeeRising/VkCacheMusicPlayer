using Java.IO;

namespace VkMusicPlayer
{
    public class MusicEncoder
    {
        private readonly int[] _mask = { 0x0D, 0x1E, 0x2F, 0x40, 0x51, 0x62, 0x73, 0x84, 0x95, 0xA6, 0xB7, 0xC8, 0xD9, 0xEA, 0xFB, 0x0C };
        public byte[] ProcessBytes(string inFilename)
        {
            var strByte = 0;
            var encodedFile = new File(inFilename);
            try
            {
                using (var finStream = new FileInputStream(encodedFile))
                {
                    var buffer = new byte[finStream.Available()];
                    finStream.Read(buffer, 0, finStream.Available());
                    for (var i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] ^= (byte)_mask[strByte];
                        _mask[strByte] += 0x10;
                        if (strByte < 15)
                            strByte++;
                        else
                            strByte = 0;
                    }
                    return buffer;

                }
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}