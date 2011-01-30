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
        private float mFPS, mScale;

        /* The current frame to show */
        private int mFrame;

        private Texture2D mTexture;

        /* Elapsed time */
        private float mElapsed;

        public float Scale
        {
            get {return mScale;}
            set {mScale = value;}
        }

        private float tWidth;
        private float tHeight;

        private bool runOneTime;
        

        public AnimatedSprite() { }

        public void LoadEnum(ContentManager content, spriteText st, bool runOnce)
        {
            
            switch (st)
            {
                case spriteText.BABYDEATH:
                    Load(content, "AnimatedSprites/babydeath", 17, 0.05f, 132.0f, 123.0f, runOnce);
                    break;
                case spriteText.BABYEAT:
                    Load(content, "AnimatedSprites/babyeat", 16, 0.05f, 94.0f, 114.0f, runOnce);
                    break;
                case spriteText.BABYJUMP:
                    Load(content, "AnimatedSprites/babyjump", 16, 0.05f, 93.0f, 122.0f, runOnce);
                    break;
                case spriteText.BABYONFIRE:
                    Load(content, "AnimatedSprites/babyonfire", 16, 0.05f, 146.0f, 155.0f, runOnce);
                    break;
                case spriteText.BABYSPAWN:
                    Load(content, "AnimatedSprites/babyspawn", 8, 0.05f, 128.0f, 128.0f, runOnce);
                    break;
                case spriteText.FATTYDEATH:
                    Load(content, "AnimatedSprites/fattydeath", 28, 0.05f, 171.0f, 153.0f, runOnce);
                    break;
                case spriteText.FATTYEAT:
                    Load(content, "AnimatedSprites/fattyeat", 16, 0.05f, 127.0f, 135.0f, runOnce);
                    break;
                case spriteText.FATTYJUMP:
                    Load(content, "AnimatedSprites/fattyjump", 64, 0.025f, 131.0f, 139.0f, runOnce);
                    break;
                case spriteText.FATTYONFIRE:
                    Load(content, "AnimatedSprites/fattyonfire", 16, 0.05f, 174.0f, 152.0f, runOnce);
                    break;
                case spriteText.MIDDEATH:
                    Load(content, "AnimatedSprites/middeath", 37, 0.05f, 149.0f, 139.0f, runOnce);
                    break;
                case spriteText.MIDEAT:
                    Load(content, "AnimatedSprites/mideat", 16, 0.05f, 123.0f, 128.0f, runOnce);
                    break;
                case spriteText.MIDJUMP:
                    Load(content, "AnimatedSprites/midjump", 16, 0.05f, 120.0f, 128.0f, runOnce);
                    break;
                case spriteText.MIDONFIRE:
                    Load(content, "AnimatedSprites/midonfire", 12, 0.05f, 145.0f, 162.0f, runOnce);
                    break;
            } 

        }

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
            mTexture = content.Load<Texture2D>(name);
            mFPS = FPS;
            mFrame = 0;
            mElapsed = 0.0f;

            mScale = 0.5f;

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
            string n = mTexture.Name;

           /* if (n == "fattyonfire" ||
                n == "babyonfire" || 
                n == "fattydeath" ||
                n == "fattyeat" ||
                n == "fattyjump" ||
                n == "midonfire" ||
                n == "babyeat")
            {
                int tW = mTexture.Width;
                int fpr = (int)(tW / tWidth);
                int row = (int)(((frameNum) * tWidth) / tW);
           
                int xpos = (frameNum - (row * fpr)) * ((int)tWidth - 3);
                int ypos = row * (int)tHeight;
                Rectangle myRect = new Rectangle(xpos, ypos, (int)tWidth, (int)tHeight);
                Console.WriteLine(myRect); 
                return myRect;
            } else { */
                int tW = mTexture.Width;
                int fpr = (int)(tW / tWidth);
                int gap = tW - (int)(tWidth * fpr);
                int row = (int)((((frameNum + 1) * tWidth) + (gap * ((frameNum + 1) * tWidth) / tW)) / tW);
           
                int xpos = (frameNum - (row * fpr)) * (int)tWidth;
                int ypos = row * (int)tHeight;
                Rectangle myRect = new Rectangle(xpos, ypos, (int)tWidth, (int)tHeight);
               // Console.WriteLine(myRect); 
                return myRect;
          //  }

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

            spriteBatch.Draw(mTexture, position, sourcerect, Color.White, 0.0f, Vector2.Zero, mScale, SpriteEffects.None, 0.0f);
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
            return mTexture.Height*mScale;
        }

        public float getWidth()
        {
            return (mTexture.Width / mFrameCount)*mScale;
        }
    }
}
