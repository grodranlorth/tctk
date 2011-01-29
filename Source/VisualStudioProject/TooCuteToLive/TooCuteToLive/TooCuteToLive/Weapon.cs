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
    class Weapon
    {
        private Vector2 mStrikePos;
        private float mStrikeTime;
        private float mScale;
        private Texture2D mTexture;

        enum states
        {
            STRIKING,
            MOVING,
            NORMAL
        };

        states mState;

        public Weapon(string textureName, ContentManager cm)
        {
            mTexture = cm.Load<Texture2D>("Weapons/" + textureName);
            mScale = 1;
            mStrikeTime = 0;
            mStrikePos = Vector2.Zero;
            mState = states.NORMAL;
        }

        public void Update(GameTime gt)
        {
            if (mState == states.MOVING)
            {
                mStrikeTime -= (float)gt.ElapsedGameTime.TotalSeconds;
                if (mStrikeTime < 0)
                    mState = states.STRIKING;
            }
            else if (mState == states.STRIKING)
            {
                /* animation */

                //mState = states.NORMAL;
            }
        }

        public void Strike(Vector2 pos, float waittime)
        {
            mStrikePos = pos;
            mState = states.MOVING;
            mStrikeTime = waittime;
        }

        public void Draw(SpriteBatch sb)
        {
            if (mState == states.STRIKING)
                sb.Draw(mTexture, new Vector2(mStrikePos.X, 0.0f), Color.White);
        }
    }
}
