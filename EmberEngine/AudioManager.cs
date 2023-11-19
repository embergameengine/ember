using NAudio;
using NAudio.Wave;

namespace EmberEngine
{
    public static class AudioManager
    {
        static WaveOutEvent player;

        public static void Mute()
        {
            player.Volume = 0f;
        }

        public static void Unmute()
        {
            player.Volume = 1f;
        }

        public static void Init()
        {
            player = new WaveOutEvent();
        }

        public static void PlayFile(string path)
        {
            WaveStream stream = new WaveFileReader(path);
            WaveChannel32 volumeStream = new WaveChannel32(stream);

            player.Init(volumeStream);

            player.Play();

        }
    }
}
