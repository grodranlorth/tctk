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
    class Character
    {
        private string mTextureName;
        private Vector2 mPosition;
        private Vector2 mSpeed;
        private BoundingSphere bSphere;
        private AnimatedSprite mSprite;
        private int distance;
        private Vector2 destination;

        private float timeOnFire;
        private float respawnRate;

        private ContentManager mContent;

        private bool remove;

        enum age
        {
            BABY,
            MEDIUM,
            FAT
        };

        age mAge;

        enum states
        {
            WALKING,
            RUNNINGAWAY,
            ONFIRE,
            EATING,
            SEEKING,
            DEAD
        };

        states mStates;

        public Vector2 Position
        {
            get { return mPosition; }
        }

        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public Character(string textureName, Vector2 position, ContentManager content, int frameCount)
        {
            mContent = content;
            mTextureName = textureName;
            mPosition = position;
            mAge = age.MEDIUM;
            mStates = states.WALKING;
            mSpeed = new Vector2(1.0f, 0.25f);
            mSprite = new AnimatedSprite();
            mSprite.Load(mContent, mTextureName, frameCount, 0.05f);
            bSphere = new BoundingSphere(new Vector3(position.X + mSprite.getWidth() / 2, position.Y + mSprite.getHeight() / 2, 0.0f), mSprite.getWidth() / 2);
            distance = 10000;
            destination = Vector2.Zero;
            timeOnFire = 5.0f;
            respawnRate = 3.0f;
            remove = false;
        }

        public void changeImage(string textureName, int numFrames)
        {
            mTextureName = textureName;
            mSprite.Load(mContent, mTextureName, numFrames, 0.1f);
        }

 //       public void setSeek(bool seek)
 //       {
 //           if (seek == true && mStates == states.WALKING)
 //               mStates = states.SEEKING;
 //           else if (seek == false)
 //               mStates = states.WALKING;
 //       }

        public void Update(GameTime gameTime)
        {
            if (mStates != states.DEAD)
            {
                mPosition.X+= mSpeed.X;
                mPosition.Y+= mSpeed.Y;
                if (mPosition.X > 800)
                    mSpeed.X *= -1;
                else if (mPosition.X <= 0)
                    mSpeed.X *= -1;
                if (mPosition.Y > 600)
                    mSpeed.Y *= -1;
                else if (mPosition.Y <= 0)
                    mSpeed.Y *= -1;
                bSphere.Center = new Vector3(mPosition.X + mSprite.getWidth() / 2, mPosition.Y + mSprite.getHeight() / 2, 0.0f);
            }
            mSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            if (mStates == states.ONFIRE)
            {
                mSpeed.X = 4;
                mSpeed.Y = 4;

                timeOnFire -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeOnFire <= 0)
                {
                    mStates = states.DEAD;
                    timeOnFire = 5.0f;
                }
            }
            else if (mStates == states.DEAD)
            {
                //changeImage("charMediumDead", Frames.CHAR_MED_DEAD_FRAMES);
                respawnRate -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (respawnRate <= 0)
                {
                    remove = true;
                    respawnRate = 3.0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mSprite.Draw(spriteBatch, mPosition);
        }

        public bool Collides(BoundingSphere boundSphere)
        {
            return bSphere.Contains(boundSphere) == ContainmentType.Intersects;
        }

        public bool Collides(Vector2 point)
        {
            return bSphere.Contains(new Vector3(point.X, point.Y, 0.0f)) == ContainmentType.Contains;
        }

        public void kill()
        {
            mStates = states.ONFIRE;
            changeImage("charMediumOnFire", Frames.CHAR_MED_ON_FIRE_FRAMES);
        }

        public bool Remove
        {
            get { return remove; }
            set { remove = value; }
        }

        public BoundingSphere BSphere
        {
            get { return bSphere; }
        }

        public Vector2 Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }
    }
}
