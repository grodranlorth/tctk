﻿using System;
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
        private float timeEating;
        private float respawnRate;
        private float timespawning;

        private float[] hopY = { 1.0f, 0.0f, 2.0f, 0.0f, 3.0f, 0.0f, 4.0f, 0.0f, 5.0f, 0.0f, -1.0f, 0.0f, -2.0f, 0.0f, -3.0f, 0.0f, -4.0f, 0.0f, -5.0f, 0.0f};
        private float[] fathopY = { 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
        private int hopCounter = 0;

        private bool multipleOfTwo;

        private ContentManager mContent;

        private bool remove;

        private Texture2D hacktex;

        age mAge;

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
            mAge = age.BABY;
            mStates = states.SPAWNING;
            mSpeed = new Vector2(1.0f, 0.25f);
            //mSpeed = new Vector2(0, 0);

            mSprite = new AnimatedSprite();
            //mSprite.Load(mContent, "AnimatedSprites/" + mTextureName, frameCount, 30, 149, 139, false);
            mSprite.Load(mContent, "AnimatedSprites/" + mTextureName, frameCount, 0.125f, 128, 128, false);


            bSphere = new BoundingSphere(new Vector3(position.X + mSprite.getWidth() / 2, position.Y + mSprite.getHeight() / 2, 0), mSprite.getWidth() / 2);
            distance = 10000;
            destination = Vector2.Zero;
            timeEating = 2.0f;

            timeOnFire = 0.5f;

            respawnRate = 3.0f;
            remove = false;
            multipleOfTwo = false;
            hacktex = Class1.CreateCircle((int)mSprite.getWidth() / 2, Color.Yellow);

            timespawning = 2.0f;
        }

        public void changeImage(string textureName, int numFrames)
        {

            mTextureName = textureName;
            mSprite.Load(mContent, "AnimatedSprites/" + mTextureName, numFrames, 30, 149, 139, false);

        }

 //       public void setSeek(bool seek)
 //       {
 //           if (seek == true && mStates == states.WALKING)
 //               mStates = states.SEEKING;
 //           else if (seek == false)
 //               mStates = states.WALKING;
 //       }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (mStates != states.DEAD)
            {
                bSphere.Center = new Vector3(mPosition.X + mSprite.getWidth() / 2, mPosition.Y + mSprite.getHeight() / 2, 0.0f);

            }
            mSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);


            if (mStates == states.SPAWNING)
            {
                if (timespawning > 0)
                {
                    timespawning =- gameTime.ElapsedGameTime.Seconds;
                }
                else
                {
                    mSprite.LoadEnum(mContent, spriteText.BABYJUMP, false);
                    mStates = states.WALKING;

                }
            }
            else if (mStates == states.ONFIRE)
            {
                timeOnFire -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                mPosition.X += mSpeed.X;
                mPosition.Y += mSpeed.Y; 

                if (timeOnFire <= 0)
                {
                   if (mAge == age.BABY)
                   {
                        mSprite.LoadEnum(mContent, spriteText.BABYDEATH, true);

                   }
                   else if (mAge == age.MEDIUM)
                   {
                       mSprite.LoadEnum(mContent, spriteText.MIDDEATH, true);
                   }
                   else
                   {
                       mSprite.LoadEnum(mContent, spriteText.FATTYDEATH, true);
                   }
                    mStates = states.DEAD;
                    timeOnFire = 0.5f;
                    
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
            else if (mStates == states.WALKING)
            {
                mPosition.X += mSpeed.X;

                if (mAge != age.FAT)
                    mPosition.Y += hopY[hopCounter];
                //else
                    //mPosition.Y += fathopY[hopCounter];

                hopCounter++;

                if (hopCounter >= 20)
                {
                    if (mAge == age.FAT)
                        hopCounter = 0;
                    else if (mPosition.Y > graphics.GraphicsDevice.Viewport.Height / 10)
                    {
                        if (HackRandom.rand.Next(0, 7) <= 4)
                            mPosition.Y -= 10.0f;
                        else
                            mPosition.Y += 10.0f;
                    }
                    else
                    {
                        if (HackRandom.rand.Next(0, 6) <= 4)
                            mPosition.Y += 10.0f;
                        else
                            mPosition.Y -= 10.0f;
                    }

                    hopCounter = 0;
                }

            }
            else if (mStates == states.SEEKING)
            {
                mSpeed.X = 1;
                mSpeed.Y = 1;
                int xDir = 0;
                int yDir = 0;
                float xMove = destination.X - Position.X;
                float yMove = destination.Y - Position.Y;

                /* Mmmmm, mmm!  Spaghetti code! */

                if (Math.Abs(mPosition.X - destination.X) < 20 &&
                    Math.Abs(mPosition.Y - destination.Y) < 20)
                {
                    mStates = states.EATING;
                    if (mAge == age.BABY)
                    {
                        mSprite.LoadEnum(mContent, spriteText.BABYEAT, false);
                    }
                    else if (mAge == age.MEDIUM)
                    {
                        mSprite.LoadEnum(mContent, spriteText.MIDEAT, false);
                    }
                    else
                    {
                        mSprite.LoadEnum(mContent, spriteText.FATTYEAT, false);
                    }

                }

                if (mPosition.X != destination.X)
                {
                    xDir = (int)(xMove / Math.Abs(xMove));
                    mPosition.X += mSpeed.X * xDir;
                }

                if (mPosition.Y != destination.Y)
                {
                    yDir = (int)(yMove / Math.Abs(yMove));
                    mPosition.Y += mSpeed.Y * yDir;
                }
            }
            else if (mStates == states.EATING)
            {
                timeEating -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeEating < 0)
                {
                    mStates = states.WALKING;
                    if (mAge == age.BABY) {
                        mAge = age.MEDIUM;
                        mStates = states.WALKING;
                        timeEating = 2.0f;
                        mSprite.LoadEnum(mContent, spriteText.MIDJUMP, false);
                    }
                    else if (mAge == age.MEDIUM)
                    {
                        mAge = age.FAT;
                        mSprite.LoadEnum(mContent, spriteText.FATTYJUMP, false);
                    }
                }

            }
            //bSphere.Center = new Vector3(mPosition.X, mPosition.Y, 0.0f);

            if (mPosition.X + getWidth() > graphics.GraphicsDevice.Viewport.Width)
                mSpeed.X *= -1;
            else if (mPosition.X <= 0)
                mSpeed.X *= -1;
            if (mPosition.Y + getHeight() > graphics.GraphicsDevice.Viewport.Height)
                mSpeed.Y *= -1;
            else if (mPosition.Y <= 0)
                mSpeed.Y *= -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mSprite.Draw(spriteBatch, mPosition);
            //spriteBatch.Draw(hacktex, new Vector2(bSphere.Center.X - mSprite.getWidth() / 2, bSphere.Center.Y - mSprite.getHeight() / 2), Color.White);
        }

        public states getState()
        {
            return mStates;
        }

        public bool Collides(BoundingSphere boundSphere)
        {
            return bSphere.Contains(boundSphere) == ContainmentType.Intersects;
        }

        public bool Collides(Vector2 point)
        {
            //return (bSphere.Contains(new Vector3(point.X, point.Y, 0.0f)) == ContainmentType.Contains);

            Vector2 c = new Vector2(mPosition.X + 25, mPosition.Y +  30);

            double dist = Math.Sqrt(Math.Pow((c.X - point.X), 2) + Math.Pow((c.Y - point.Y), 2));
            //Console.WriteLine("center: " + c.X + " " + c.Y + " Point: " + point.X + " " + point.Y);
            //Console.WriteLine("dist: " + dist);

            return dist <= 40;
        }

        public void kill()
        {
            float angle = HackRandom.rand.Next(0, 360);
            mSpeed.X = (float)Math.Cos(angle) * 3;
            mSpeed.Y = (float)Math.Sin(angle) * 3;

            mStates = states.ONFIRE;
            if (mAge == age.BABY) {
                mSprite.LoadEnum(mContent, spriteText.BABYONFIRE, false);
            } else if (mAge == age.MEDIUM) {
                mSprite.LoadEnum(mContent, spriteText.MIDONFIRE, false);
            }
            else
            {
                mSprite.LoadEnum(mContent, spriteText.FATTYONFIRE, false);
            }
        }

        public void setSeek(bool SEEK)
        {
            if (mStates == states.SEEKING || mStates == states.WALKING)
            {
                if (SEEK)
                {

                    mStates = states.SEEKING;
                    
                }
                else
                {
                    mStates = states.WALKING;
                }
            }
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

        public bool OnFire()
        {
            return mStates == states.ONFIRE;
        }

        public void SetOnFire()
        {
            kill();
        }

        public float getWidth()
        {
            return mSprite.getWidth();
        }

        public float getHeight()
        {
            return mSprite.getHeight();
        }

        public bool MultipleOfTwo
        {
            get { return multipleOfTwo; }
            set { multipleOfTwo = value; }
        }

        public void CeaseEating()
        {
            /*if (mStates == states.EATING || mStates == states.SEEKING)
            {
                mStates = states.WALKING;
                if (mAge == age.BABY)
                {
                    mSprite.LoadEnum(mContent, spriteText.BABYJUMP, false);
                }
                else if (mAge == age.MEDIUM)
                {
                    mSprite.LoadEnum(mContent, spriteText.MIDJUMP, false);
                }
                else
                {
                    mSprite.LoadEnum(mContent, spriteText.FATTYJUMP, false);
                }
            }*/
        }
    }
}
