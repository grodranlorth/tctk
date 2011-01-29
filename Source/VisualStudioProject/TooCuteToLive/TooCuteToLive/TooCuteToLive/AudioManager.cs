using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TooCuteToLive
{
    class AudioManager
    {
        private float mVolume;

        public void Load(ContentManager content)
        {
            mVolume = 1.0f;
        }

        public void StopSound()
        {

        }

        public void Play(SoundEffectInstance sound, float volume)
        {
            sound.Play();
            sound.Volume = volume;
        }

        public void Play(SoundEffectInstance sound)
        {
            sound.Play();
            sound.Volume = mVolume;
        }
    }
}
