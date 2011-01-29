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
        }

        public void Load()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mState == states.DRAW)
                spriteBatch.Draw(cupcake, new Vector2(mPosition.X, mPosition.Y), Color.White);
        }
    }
}
