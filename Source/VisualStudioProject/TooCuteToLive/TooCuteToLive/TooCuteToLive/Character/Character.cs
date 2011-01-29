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

        /* Age of the character */
        /* 1 = Baby */
        /* 2 = Medium */
        /* 3 = Fat */
        enum age
        {
            BABY,
            MEDIUM,
            FAT
        };

        age mAge;

        /* State of the character */
        /* 1 = Walking */
        /* 2 = Running Away */
        /* 3 = OnFire */
        /* 4 = Eating */
        enum states
        {
            WALKING,
            RUNNINGAWAY,
            ONFIRE,
            EATING
        };

        states mStates;

        public Character(string textureName, Vector2 position, ContentManager content)
        {
            mTextureName = textureName;
            mPosition = position;
            mAge = age.MEDIUM;
            mStates = states.WALKING;
            xspeed = yspeed = 1.0f;
            mSprite = new AnimatedSprite();
            mSprite.Load(content, mTextureName, 16, 0.2f);
            bSphere = new BoundingSphere(new Vector3(position.X + mSprite.getWidth() / 2, position.Y + mSprite.getHeight() / 2, 0.0f), mSprite.getWidth() / 2);
        }

        public void Load(ContentManager content)
        {
            
            
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
