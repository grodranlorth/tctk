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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Text;

namespace TooCuteToLive
{
    class AnimatedSprite
    {
        /* Number of frames the animation has */
        private int mFrameCount;

        /* Frames Per Second */
        private float mFPS;

        /* The current frame to show */
        private int mFrame;

        private Texture2D mTexture;

        /* Elapsed time */
        private float mElapsed;

        private float tWidth;
        private float tHeight;

        private bool runOneTime;

        public AnimatedSprite() { }

        /// <summary>
        /// Loads function - self explanatory
        /// </summary>
        /// <param name="content">The current content manager</param>
        /// <param name="name">Name of the asset - assumes the animatedSprites folder</param>
        /// <param name="frameCount">number of frames</param>
        /// <param name="FPS">Frames Per Second</param>
        public void Load(ContentManager content, string name, int frameCount, float FPS, float width, float height, bool runOnce)
        {
            mFrameCount = frameCount;
            mTexture = content.Load<Texture2D>("AnimatedSprites/" + name);
            mFPS = FPS;
            mFrame = 0;
            mElapsed = 0.0f;
            tHeight = height;
            tWidth = width;
            runOneTime = runOnce; 
        }

        /// <summary>
        /// Update function - self explanatory
        /// </summary>
        /// <param name="elapsed">elapsed time - use (float)gameTime.ElapsedGameTime.TotalSeconds</param>
        public void Update(float elapsed)
        {
            mElapsed += elapsed;

            /* If enough has passed, update the frame */
            if (mElapsed > mFPS)
            {
                mFrame++;

                if (!runOneTime)
                {
                    mFrame = mFrame % mFrameCount;
                }
                
                mElapsed -= mFPS;
            }
        }

        private Rectangle getRect(int frameNum)
        {
            int fpr = (int)(2048 / tWidth);
            int gap = 2048 - (int)(tWidth * fpr);
            int row = (int)((((frameNum + 1) * tWidth) + (gap * ((frameNum + 1) * tWidth) / 2048)) / 2048);
           
            int xpos = (frameNum - (row * fpr)) * (int)tWidth;
            int ypos = row * (int)tHeight;
            Rectangle myRect = new Rectangle(xpos, ypos, (int)tWidth, (int)tHeight);
            Console.WriteLine(myRect); 
            return myRect;

        }

        /// <summary>
        /// Draw function - self explanatory
        /// </summary>
        /// <param name="spriteBatch">the spritebatch to draw</param>
        /// <param name="position">where you want the texture to be drawn</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = mTexture.Width / mFrameCount;
            Rectangle sourcerect = getRect(mFrame);

            spriteBatch.Draw(mTexture, position, sourcerect, Color.White);

        }

        /// <summary>
        /// Reset function - Resets the frames and elapsed time to zero
        /// </summary>
        public void Reset()
        {
            mFrame = 0;
            mElapsed = 0.0f;
        }

        public float getHeight()
        {
            return tHeight;
        }

        public float getWidth()
        {
            return tWidth;
        }
    }
}
