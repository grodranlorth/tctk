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
    class Item
    {
        private Vector2 mPosition;
        private Texture2D cupcake;
        private string mTextureName;
        private ContentManager mContent;
        private bool remove;
        private float timer;
        public bool eating;

        enum states
        {
            DRAW,
            DONT_DRAW
        };

        states mState = states.DONT_DRAW;

        public Vector2 Position
        {
            get { return mPosition; }
        }

        public Item(string name, Vector2 position, ContentManager content)
        {
            mContent = content;
            mTextureName = name;
            mPosition = position;
            mState = states.DRAW;
            cupcake = mContent.Load<Texture2D>("Items/cupcake");
            remove = false;
            timer = 2.0f;
            eating = false;
        }

        public void Load()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (eating)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    remove = true;
                    timer = 2.0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mState == states.DRAW)
                spriteBatch.Draw(cupcake, new Vector2(mPosition.X, mPosition.Y), Color.White);
        }

        public bool Remove
        {
            get { return remove; }
            set { remove = value; }
        }
    }
}
