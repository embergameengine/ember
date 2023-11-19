using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine.Components
{
    public class AudioPlayer : Component
    {
        public string audioPath { get; set; }
        public bool playOnAwake { get; set; }

        public AudioPlayer()
        {
            playOnAwake = true;
        }

        public void Load()
        {
            if (playOnAwake)
            {
                AudioManager.PlayFile(audioPath);
            }
            
        }

        public void Play()
        {
            AudioManager.PlayFile(audioPath);
        }

        public void Play(string path)
        {

        }
    }
}
