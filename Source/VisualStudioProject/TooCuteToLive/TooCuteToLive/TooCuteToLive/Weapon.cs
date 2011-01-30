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
        protected Vector2 mStrikePos;
        protected float mStrikeTime;
        protected float mOnScreenTime;
        protected Vector2 mScale;
        protected Texture2D mTexture;

        public CharacterManager mCharManager;

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
        }

        public Weapon(ContentManager cm, GraphicsDeviceManager graphics)
        {
            mCharManager = CharacterManager.GetInstance(cm, graphics);
            mTexture = null;
            mScale = new Vector2(1.0f, 0.0f);
            mStrikeTime = 0;
            mStrikePos = Vector2.Zero;
            mState = states.NORMAL;
            mOnScreenTime = 0.5f;
        }

        public virtual void Update(GameTime gt, float yValue)
        {
            if (mState == states.MOVING)
            {
                mStrikeTime -= (float)gt.ElapsedGameTime.TotalSeconds;
                if (mStrikeTime < 0)
                    mState = states.STRIKING;
            }
            else if (mState == states.STRIKING)
            {
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

        public virtual void Strike(Vector2 pos, float waittime, float yValue)
        {
        }

        public virtual void Draw(SpriteBatch sb)
        {
        }

        public virtual bool Ready()
        {
            return false;
        }

        public virtual float getWidth()
        {
            return mTexture.Width;
        }
    }

    class WRainbow: Weapon
    {
        private float mYValue;

        enum states
        {
            STRIKING,
            MOVING,
            NORMAL
        };

        states mState;

        public WRainbow(ContentManager cm, GraphicsDeviceManager graphics)
            : base("rainbow", cm, graphics)
        {
            mState = states.NORMAL;
            mYValue = 0.0f;
        }

        public override void Update(GameTime gt, float yValue)
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
                else if (mOnScreenTime <= .3)
                {
                    mCharManager.pointKill(mStrikePos);
                }
            }
        }

        public override void Strike(Vector2 pos, float waittime, float yValue)
        {
            mStrikePos = pos;
            mState = states.MOVING;
            mStrikeTime = waittime;
            mYValue = yValue;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (mState == states.STRIKING)
                sb.Draw(mTexture, new Vector2(mStrikePos.X, 0.0f), null, Color.White, 0.0f,
                        new Vector2(0.0f, 50.0f), mScale, SpriteEffects.None, 0.0f);
            //sb.Draw(mTexture, new Vector2(mStrikePos.X, 0.0f), Color.White);

        }

        public override bool Ready()
        {
            return mState == states.NORMAL;
        }
    }

    class WHeart : Weapon
    {
        private float mYValue;
        private float mBlowuptime, mFallrate, mExptime;
        private const float gravity = .1f, maxgrav = 40;
        private Vector2 mPos, mSpeed;
        private AnimatedSprite mSprite, mSpriteExp;
        private bool killed;

        enum states
        {
            FALLING,
            WAITING,
            EXPLODING,
            GONE
        };

        states mState;

        public WHeart(ContentManager cm, GraphicsDeviceManager graphics)
            : base(cm, graphics)
        {
            mYValue = 0.0f;
            mPos = new Vector2();
            mState = states.GONE;

            mSprite = new AnimatedSprite();
            mSprite.Load(cm, "Weapons/bombheart", 6, .5f, 117.6f, 100, false);

            mSpriteExp = new AnimatedSprite();
            mSpriteExp.Load(cm, "AnimatedSprites/mushroomspritesheet", 12, .02f, 118, 127, false);
            mSpriteExp.Scale = 1.25f;
        }

        public override void Update(GameTime gt, float yValue)
        {
            if (mState == states.FALLING)
            {
                mFallrate -= (float)gt.ElapsedGameTime.TotalSeconds;
                if (mFallrate < 0)
                {
                    mFallrate = .01f;
                    mSpeed.Y += (mSpeed.Y >= 3) ? 1 : gravity;
                    mPos.Y += (mSpeed.Y > maxgrav) ? maxgrav : mSpeed.Y;
                }

                if (mPos.Y + mSprite.getHeight() / 2 >= mStrikePos.Y)
                {
                    //mPos.Y = mStrikePos.Y - mSprite.getHeight() / 2;
                    mState = states.WAITING;
                }
            }
            else if (mState == states.WAITING)
            {
                mBlowuptime -= (float)gt.ElapsedGameTime.TotalSeconds;
                mSprite.Update((float)gt.ElapsedGameTime.TotalSeconds);
                if (mBlowuptime < 0)
                {
                    mState = states.EXPLODING;
                    mPos.X = mStrikePos.X - mSpriteExp.getWidth() / 2;
                    mPos.Y = mStrikePos.Y - 3*(mSpriteExp.getHeight() / 4);
                }
            }
            else if (mState == states.EXPLODING)
            {
                mExptime -= (float)gt.ElapsedGameTime.TotalSeconds;
                mSpriteExp.Update((float)gt.ElapsedGameTime.TotalSeconds);

                if (mExptime < 0)
                {
                    mState = states.GONE;
                }
                else if (mExptime < .1 && !killed)
                {
                    /* XXX */
                    killed = true;
                    LinkedList<Vector2> points = circlehack(mStrikePos, 40);

                    //Console.WriteLine("killing points");
                    foreach (Vector2 vec in points)
                    {
                        mCharManager.pointKill(vec);
                        //Console.WriteLine(vec.X + " " + vec.Y);
                    }
                }
            }
        }

        public override void Strike(Vector2 pos, float waittime, float yValue)
        {
            mStrikePos = pos;
            mState = states.FALLING;

            mPos.Y = -20;
            mPos.X = mStrikePos.X - mSprite.getWidth() / 2;
            mSpeed = Vector2.Zero;
            mBlowuptime = .1f;
            mExptime = .24f;
            mFallrate = .01f;
            mSprite.Reset();
            mSpriteExp.Reset();
            killed = false;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (mState == states.FALLING || mState == states.WAITING)
                mSprite.Draw(sb, mPos);
            else if (mState == states.EXPLODING)
                mSpriteExp.Draw(sb, mPos);
        }

        public override bool Ready()
        {
            return mState == states.GONE;
        }

        private LinkedList<Vector2> circlehack(Vector2 pos, int rad, int skip = 20)
        {
            LinkedList<Vector2> ret = new LinkedList<Vector2>();
            Vector2 temp;

            for(int i = 0; i < 360; i += skip)
            {
                temp.X = pos.X + (float)Math.Cos(i)*rad;
                temp.Y = pos.Y - (float)Math.Sin(i)*rad;

                ret.AddLast(new Vector2(temp.X, temp.Y));
            }

            return ret;
        }
    }
}