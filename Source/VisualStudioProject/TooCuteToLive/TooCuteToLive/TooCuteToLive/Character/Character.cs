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
        private float xspeed, yspeed;
        private BoundingSphere bSphere;
        private AnimatedSprite mSprite;
        private int distance;
        private Vector2 destination;

        private ContentManager mContent;

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
            SEEKING
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
            xspeed = yspeed = 1.0f;
            mSprite = new AnimatedSprite();
            mSprite.Load(mContent, mTextureName, frameCount, 0.05f);
            bSphere = new BoundingSphere(new Vector3(position.X + mSprite.getWidth() / 2, position.Y + mSprite.getHeight() / 2, 0.0f), mSprite.getWidth() / 2);
            distance = 10000;
            destination = Vector2.Zero;
        }

//        public void changeImage(string textureName)
//        {
//            mTextureName = textureName;
//            mSprite.Load(mContent, mTextureName, CHAR_MED_ON_FIRE_FRAMES, 0.2f);
//        }

        public void setSeek(bool seek)
        {
            if (seek == true && mStates == states.WALKING)
                mStates = states.SEEKING;
            else if (seek == false)
                mStates = states.WALKING;
        }

        public void Update(GameTime gameTime)
        {
            mPosition.X+= xspeed;
            mPosition.Y+= yspeed;
            if (mPosition.X > 800)
                xspeed *= -1;
            else if (mPosition.X <= 0)
                xspeed *= -1;
            if (mPosition.Y >= 600)
                yspeed *= -1;
            else if (mPosition.Y <= 0)
                yspeed *= -1;
            bSphere.Center = new Vector3(mPosition.X + mSprite.getWidth() / 2, mPosition.Y + mSprite.getHeight() / 2, 0.0f);
            mSprite.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
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
        }
    }
}
