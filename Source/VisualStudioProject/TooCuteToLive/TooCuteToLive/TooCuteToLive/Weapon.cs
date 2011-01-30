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
        private float mOnScreenTime;
        private Vector2 mScale;
        private Texture2D mTexture;
        private float mYValue;

        private CharacterManager mCharManager;

        enum states
        {
            STRIKING,
            MOVING,
            NORMAL
        };

        states mState;

        public Weapon(string textureName, ContentManager cm, GraphicsDeviceManager graphics)
        {
            mCharManager = CharacterManager.GetInstance(cm, graphics);
            mTexture = cm.Load<Texture2D>("Weapons/" + textureName);
            mScale = new Vector2(1.0f, 0.0f);
            mStrikeTime = 0;
            mStrikePos = Vector2.Zero;
            mState = states.NORMAL;
            mOnScreenTime = 0.5f;
            mYValue = 0.0f;
        }

        public void Update(GameTime gt, float yValue)
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
                if (mScale.Y <= (mYValue + 50) / mTexture.Height)
                    mScale.Y += 0.1f;

                mOnScreenTime -= (float)gt.ElapsedGameTime.TotalSeconds;
                if (mOnScreenTime <= 0)
                {
                    mState = states.NORMAL;
                    mOnScreenTime = 0.5f;
                    mScale.Y = 0.0f;
                }
                mCharManager.pointKill(mStrikePos);
            }
        }

        public void Strike(Vector2 pos, float waittime, float yValue)
        {
            mStrikePos = pos;
            mState = states.MOVING;
            mStrikeTime = waittime;
            mYValue = yValue;
        }

        public void Draw(SpriteBatch sb)
        {
            if (mState == states.STRIKING)
                sb.Draw(mTexture, new Vector2(mStrikePos.X, 0.0f), null, Color.White, 0.0f, 
                        new Vector2(0.0f, 50.0f), mScale, SpriteEffects.None, 0.0f);
                //sb.Draw(mTexture, new Vector2(mStrikePos.X, 0.0f), Color.White);

        }

        public float getWidth()
        {
            return mTexture.Width;
        }
    }
}
