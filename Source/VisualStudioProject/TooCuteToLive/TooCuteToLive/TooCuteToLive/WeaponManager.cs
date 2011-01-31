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
    class WeaponEffects
    {
        private float mTimeleft;
    }

    class WeaponManager
    {
        private List<WeaponEffects> mElist;

        private List<Weapon> mWlist;

        private int mCurwep;

        private Random rand;

        public int CurrentWeapon
        {
            get { return mCurwep; }
            set { mCurwep = value; }
        }

        public WeaponManager(ContentManager cm, GraphicsDeviceManager gm)
        {
            mCurwep = 0;
            /* Add Weapons here */
            mWlist = new List<Weapon>();
            mElist = new List<WeaponEffects>();

            mWlist.Add(new WRainbow(cm, gm));
            mWlist.Add(new WHeart(cm, gm));

            rand = new Random();
        }

        public bool checkReady()
        {
            return (mWlist[mCurwep].Ready());
        }

        public void UseCur(Vector2 pos, int mousey)
        {
            if (!mWlist[mCurwep].Ready())
                return;

            if (rand.Next(0, 10) <= 3)
                AudioManager.yourMean.Play();

            mWlist[mCurwep].Strike(pos, 1, mousey);
        }

        public void Update(GameTime gt)
        {
            /* XXX */
            mWlist[mCurwep].Update(gt, 0);
        }

        public void Draw(SpriteBatch sb)
        {
            /* XXX */
            mWlist[mCurwep].Draw(sb);
        }
    }
}
