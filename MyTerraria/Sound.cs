using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace MyTerraria
{
    public enum TypeSound
    {
        BACKGROUN,
        GRAB,
        RUN
    }

    public class Sound
    {
        public static SoundBuffer bufferBackground;
        public static SoundBuffer bufferRun;
        public static SoundBuffer bufferGrab;

        public  SFML.Audio.Sound sound;

        public static TypeSound Type { get; set; }

        public Sound(TypeSound type)
        {
            Type = type;

            bufferBackground = new SoundBuffer(Content.CONTENT_DIR + "Sounds\\terrariya-den2.wav");
            bufferGrab = new SoundBuffer(Content.CONTENT_DIR + "Sounds\\Grab.wav");
            bufferRun = new SoundBuffer(Content.CONTENT_DIR + "Sounds\\Run.wav");

            SoundPlay(type);
        }

        public void SoundPlay(TypeSound type)
        {
            switch (type)
            {
                case TypeSound.BACKGROUN:
                    sound = new SFML.Audio.Sound(bufferBackground);
                    if (sound.Status != SoundStatus.Playing)
                        sound.Play();
                    break;
                case TypeSound.GRAB:
                    sound = new SFML.Audio.Sound(bufferGrab);
                    if (sound.Status != SoundStatus.Playing)
                        sound.Play();
                    break;
                case TypeSound.RUN:
                    sound = new SFML.Audio.Sound(bufferRun);
                    if(sound.Status != SoundStatus.Playing)
                        sound.Play();
                    break;
            }
        }

        public void Update(TypeSound type)
        {
            switch (type)
            {
                case TypeSound.BACKGROUN:
                    if (sound.Status != SoundStatus.Playing)
                        SoundPlay(Type);
                    break;
                case TypeSound.GRAB:
                    if (sound.Status != SoundStatus.Playing)
                        SoundPlay(Type);
                    break;
                case TypeSound.RUN:
                    if (sound.Status != SoundStatus.Playing)
                        SoundPlay(Type);
                    break;
            }
        }
    }
}
