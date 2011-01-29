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
        private Texture2D mTexture;
        private Vector2 mPosition;
        
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

        private float speed;

        public Character(Texture2D texture, Vector2 position)
        {
            mTexture = texture;
            mPosition = position;
            mAge = age.MEDIUM;
            mStates = states.WALKING;
            speed = 1;
        }

        public void Update(GameTime gameTime)
        {
            mPosition.X+= speed;
            mPosition.Y+= speed;
        }

        public void Draw(GameTime mGameTime, SpriteBatch mSpriteBatch)
        {
            mSpriteBatch.Begin();

            mSpriteBatch.Draw(mTexture, mPosition, Color.White);

            mSpriteBatch.End();
        }
    }
}
