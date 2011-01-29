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
    class WorldObjects
    {
        private Vector2 mPosition;
        private Texture2D mTexture;
        private int state;

        BoundingSphere mBoundingSphere;

        enum states
        {
            ONFIRE,
            DESTROYED,
            NORMAL
        };

        states mState;

        public WorldObjects(Texture2D texture, Vector2 position)
        {
            mTexture = texture;
            mPosition = position;
            mState = states.NORMAL;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public bool Collides(Vector2 point)
        {
            return mBoundingSphere.Contains(new Vector3(point.X, point.Y, 0.0f)) == ContainmentType.Contains;
        }
    }
}
