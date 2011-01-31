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
        public static float mVolume;

        public static SoundEffect chillinSource;
        public static SoundEffectInstance chillin;

        public static SoundEffect hiSource;
        public static SoundEffectInstance hi;
        
        //public static SoundEffect notAgainSource;
        //public static SoundEffectInstance notAgain;
        
        public static SoundEffect owwSource;
        public static SoundEffectInstance oww;
        
        public static SoundEffect youreMeanSource;
        public static SoundEffectInstance yourMean;

        public static SoundEffect chillin2Source;
        public static SoundEffectInstance chillin2;

        public static SoundEffect ooooohSource;
        public static SoundEffectInstance oooooh;

        public static SoundEffect ouchSource;
        public static SoundEffectInstance ouch;

        public static SoundEffect tickBoomSource;
        public static SoundEffectInstance tickBoom;

        public static SoundEffect missileSource;
        public static SoundEffectInstance missile;

        public static SoundEffect pewSource;
        public static SoundEffectInstance pew;

        public static void Load(ContentManager content)
        {
            chillinSource = content.Load<SoundEffect>("Sounds/2qt2livmusic");
            chillin = chillinSource.CreateInstance();

            hiSource = content.Load<SoundEffect>("Sounds/SFX_hi");
            hi = hiSource.CreateInstance();

            //notAgainSource = content.Load<SoundEffect>("Sounds/SFX_notagain");
            //notAgain = notAgainSource.CreateInstance();
                
            owwSource = content.Load<SoundEffect>("Sounds/SFX_oww");
            oww = owwSource.CreateInstance();

            youreMeanSource = content.Load<SoundEffect>("Sounds/SFX_youremean");
            yourMean = youreMeanSource.CreateInstance();

            chillin2Source = content.Load<SoundEffect>("Sounds/BGM_take1");
            chillin2 = chillin2Source.CreateInstance();

            ooooohSource = content.Load<SoundEffect>("Sounds/SFX_ooooh");
            oooooh = ooooohSource.CreateInstance();

            ouchSource = content.Load<SoundEffect>("Sounds/SFX_ouch");
            ouch = ouchSource.CreateInstance();

            tickBoomSource = content.Load<SoundEffect>("Sounds/SFX_3tickboom");
            tickBoom = tickBoomSource.CreateInstance();

            //missileSource = content.Load<SoundEffect>("Sounds/SFX_missle");
            //missile = missileSource.CreateInstance();

            pewSource = content.Load<SoundEffect>("Sounds/SFX_pew");
            pew = pewSource.CreateInstance();

            mVolume = 1.0f;
        }

        public static void StopSound()
        {
            chillin.Stop();
        }

        public static void Play(SoundEffectInstance sound, float volume, float pan, float pitch)
        {
            sound.Play();
            sound.Volume = volume;
            sound.Pan = pan;
            sound.Pitch = pitch;
        }

        public static void Play(SoundEffectInstance sound)
        {
            sound.Play();
            sound.Volume = mVolume;
        }
    }
}
