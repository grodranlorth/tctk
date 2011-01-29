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
    class Menu
    {
        Game g;

        private MouseState mMouseStateCurr, mMouseStatePrev;

        private Texture2D[] selMenuItems;
        private Texture2D[] unSelMenuItems;
        private Texture2D[] menuItems;

        private Texture2D[] selCreditItems;
        private Texture2D[] unSelCreditItems;
        private Texture2D[] creditItems;

        private Texture2D[] selInstructItems;
        private Texture2D[] unSelInstructItems;
        private Texture2D[] instructItems;

        private Rectangle startRect, instructRect, creditRect, backRect;

        private const int NUM_ITEMS = 3;
        private const int NUM_CREDIT_ITEMS = 1;
        private const int NUM_INSTRUCT_ITEMS = 1;

        /* TODO - ADD TITLE */
//        private Texture2D mTitle;
        private Texture2D mStartSel, mStartUnsel, mCreditSel, mCreditUnsel, mInstructSel, mInstructUnsel;
        private Texture2D mBackSel, mBackUnsel;

        enum states
        {
            TITLE,
            INSTRUCT,
            CREDITS
        };

        states mState;

        public Menu(Game game)
        {
            g = game;
        }

        public void Load(ContentManager content)
        {
            mState = states.TITLE;

            mStartSel = content.Load<Texture2D>("Menu/startbutton_active");
            mStartUnsel = content.Load<Texture2D>("Menu/startbutton_nonactive");

            mCreditSel = content.Load<Texture2D>("Menu/credits_active");
            mCreditUnsel = content.Load<Texture2D>("Menu/credits_nonactive");

            mInstructSel = content.Load<Texture2D>("Menu/howtoplay_active");
            mInstructUnsel = content.Load<Texture2D>("Menu/howtoplay_nonactive");

            mBackSel = content.Load<Texture2D>("Menu/backtomainmenu_active");
            mBackUnsel = content.Load<Texture2D>("Menu/backtomainmenu_nonactive");

            selMenuItems = new Texture2D[NUM_ITEMS];
            unSelMenuItems = new Texture2D[NUM_ITEMS];
            menuItems = new Texture2D[NUM_ITEMS];

            selCreditItems = new Texture2D[NUM_CREDIT_ITEMS];
            unSelCreditItems = new Texture2D[NUM_CREDIT_ITEMS];
            creditItems = new Texture2D[NUM_CREDIT_ITEMS];

            selInstructItems = new Texture2D[NUM_INSTRUCT_ITEMS];
            unSelInstructItems = new Texture2D[NUM_INSTRUCT_ITEMS];
            instructItems = new Texture2D[NUM_INSTRUCT_ITEMS];

            selMenuItems[0] = mStartSel;
            selMenuItems[1] = mInstructSel;
            selMenuItems[2] = mCreditSel;

            unSelMenuItems[0] = mStartUnsel;
            unSelMenuItems[1] = mInstructUnsel;
            unSelMenuItems[2] = mCreditUnsel;

            menuItems[0] = mStartUnsel;
            menuItems[1] = mInstructUnsel;
            menuItems[2] = mCreditUnsel;

            selCreditItems[0] = mBackSel;
            unSelCreditItems[0] = mBackUnsel;
            creditItems[0] = mBackUnsel;

            selInstructItems[0] =  mBackSel;
            unSelInstructItems[0] = mBackUnsel;
            instructItems[0] = mBackUnsel;

            startRect = new Rectangle(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350, mStartSel.Width, mStartSel.Height);
            instructRect = new Rectangle(g.GraphicsDevice.Viewport.Width / 8, (g.GraphicsDevice.Viewport.Height) - 250, mInstructSel.Width, mStartSel.Height);
            creditRect = new Rectangle(g.GraphicsDevice.Viewport.Width / 6, (g.GraphicsDevice.Viewport.Height) - 150, mCreditSel.Width, mStartSel.Height);
            backRect = new Rectangle(g.GraphicsDevice.Viewport.Width - 550, (g.GraphicsDevice.Viewport.Height) - 150, mBackSel.Width, mBackSel.Height);
        }

        public void Update(GameTime gameTime, ref GameStates gameState)
        {
            mMouseStateCurr = Mouse.GetState();

            switch (mState)
            {
                case states.TITLE:
                    if (mMouseStateCurr.X >= startRect.X && mMouseStateCurr.X < startRect.X + startRect.Width &&
                        mMouseStateCurr.Y >= startRect.Y && mMouseStateCurr.Y < startRect.Y + startRect.Height)
                    {
                        menuItems[0] = mStartSel;
                        menuItems[1] = mInstructUnsel;
                        menuItems[2] = mCreditUnsel;

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                            mMouseStatePrev.LeftButton == ButtonState.Released)
                        {
                            gameState = GameStates.GAME;
                        }
                    }
                    else if (mMouseStateCurr.X >= instructRect.X && mMouseStateCurr.X < instructRect.X + instructRect.Width &&
                             mMouseStateCurr.Y >= instructRect.Y && mMouseStateCurr.Y < instructRect.Y + instructRect.Height)
                    {
                        menuItems[0] = mStartUnsel;
                        menuItems[1] = mInstructSel;
                        menuItems[2] = mCreditUnsel;

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                            mMouseStatePrev.LeftButton == ButtonState.Released)
                        {
                            mState = states.INSTRUCT;
                        }
                    }
                    else if (mMouseStateCurr.X >= creditRect.X && mMouseStateCurr.X < creditRect.X + creditRect.Width &&
                             mMouseStateCurr.Y >= creditRect.Y && mMouseStateCurr.Y < creditRect.Y + creditRect.Height)
                    {
                        menuItems[0] = mStartUnsel;
                        menuItems[1] = mInstructUnsel;
                        menuItems[2] = mCreditSel;

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                            mMouseStatePrev.LeftButton == ButtonState.Released)
                        {
                            mState = states.CREDITS;
                        }
                    }
                    else
                    {
                        menuItems[0] = mStartUnsel;
                        menuItems[1] = mInstructUnsel;
                        menuItems[2] = mCreditUnsel;
                    }
                    break;

                case states.INSTRUCT:
                    if (mMouseStateCurr.X >= backRect.X && mMouseStateCurr.X < backRect.X + backRect.Width &&
                        mMouseStateCurr.Y >= backRect.Y && mMouseStateCurr.Y < backRect.Y + backRect.Height)
                    {
                        instructItems[0] = mBackSel;

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed)
                        {
                            mState = states.TITLE;
                        }
                    }
                    else
                        instructItems[0] = mBackUnsel;

                    break;

                case states.CREDITS:
                    if (mMouseStateCurr.X >= backRect.X && mMouseStateCurr.X < backRect.X + backRect.Width &&
                        mMouseStateCurr.Y >= backRect.Y && mMouseStateCurr.Y < backRect.Y + backRect.Height)
                    {
                        creditItems[0] = mBackSel;

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed)
                        {
                            mState = states.TITLE;
                        }
                    }
                    else
                        creditItems[0] = mBackUnsel;

                    break;
            }
            mMouseStatePrev = mMouseStateCurr;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (mState)
            {
                case states.TITLE:
                    spriteBatch.Draw(menuItems[0], new Vector2(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350), Color.White);
                    spriteBatch.Draw(menuItems[1], new Vector2(g.GraphicsDevice.Viewport.Width / 8, (g.GraphicsDevice.Viewport.Height) - 250), Color.White);
                    spriteBatch.Draw(menuItems[2], new Vector2(g.GraphicsDevice.Viewport.Width / 6, (g.GraphicsDevice.Viewport.Height) - 150), Color.White);

                    break;

                case states.INSTRUCT:
                    spriteBatch.Draw(instructItems[0], new Vector2(g.GraphicsDevice.Viewport.Width - 550, (g.GraphicsDevice.Viewport.Height) - 150), Color.White); 
                    break;

                case states.CREDITS:
                    spriteBatch.Draw(creditItems[0], new Vector2(g.GraphicsDevice.Viewport.Width - 550, (g.GraphicsDevice.Viewport.Height) - 150), Color.White); 
                    break;
            }
        }
    }
}
