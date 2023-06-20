using SFML.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria
{
    public class SoundController
    {
        SortedDictionary<string, Music> sounds = new SortedDictionary<string, Music> ();

        Music music;
        string musicName;

        public void AddSound(string name, Music music, bool isLoop = false)
        {
            music.Loop = isLoop;

            sounds[name] = music;
            this.music = music;
            musicName = name;
        }

        public void RemoveSound(string name)
        {
            sounds[name] = null;
        }

        public void PlaySound(string name)
        {
            music = sounds[name];

            if (music == null)
                return;

            if (music.Status == SoundStatus.Stopped && music.Loop)
                music.Play();
            else if (!music.Loop)
            {
                music.Stop();
                music.Play();
            }
        }

        public void StopSound(string name)
        {
            music = sounds[name];

            if (music == null)
                return;

            music.Stop();
        }
    }
}
