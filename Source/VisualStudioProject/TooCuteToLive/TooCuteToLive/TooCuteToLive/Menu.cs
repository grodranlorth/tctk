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

        private Texture2D menuScreen;
        private Texture2D title;
        private Texture2D help;
        private Texture2D credits;

        private Texture2D backUnsel;
        private Texture2D creditsUnsel;
        private Texture2D helpUnsel;
        private Texture2D startUnsel;

        private AnimatedSprite backSel;
        private AnimatedSprite creditsSel;
        private AnimatedSprite helpSel;
        private AnimatedSprite startSel;
        
        private Rectangle startRect, instructRect, creditRect, backRect;

        private const int NUM_ITEMS = 3;
        private const int NUM_CREDIT_ITEMS = 1;
        private const int NUM_INSTRUCT_ITEMS = 1;

        /* TODO - ADD TITLE */

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

            backUnsel = content.Load<Texture2D>("Menu/backbutton");
            creditsUnsel = content.Load<Texture2D>("Menu/creditsbutton");
            helpUnsel = content.Load<Texture2D>("Menu/helpbutton");
            startUnsel = content.Load<Texture2D>("Menu/startbutton");

            backSel = new AnimatedSprite();
            creditsSel = new AnimatedSprite();
            helpSel = new AnimatedSprite();
            startSel = new AnimatedSprite();

            backSel.Load(content, "AnimatedSprites/backbuttonspritesheet", 8, 0.05f, 435.75f, 193, false);
            creditsSel.Load(content, "AnimatedSprites/creditsbuttonspritesheet", 8, 0.05f, 435.75f, 193, false);
            helpSel.Load(content, "AnimatedSprites/helpbuttonspritesheet", 8, 0.05f, 435.75f, 193, false);
            startSel.Load(content, "AnimatedSprites/startbuttonspritesheet", 8, 0.05f, 435.75f, 193, false);

            menuScreen = content.Load<Texture2D>("Menu/menubackground");
            title = content.Load<Texture2D>("Menu/logo");

            credits = content.Load<Texture2D>("Menu/credits");
            help = content.Load<Texture2D>("Menu/help");

            startRect = new Rectangle(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350, startUnsel.Width, startUnsel.Height);
            instructRect = new Rectangle(g.GraphicsDevice.Viewport.Width / 8, (g.GraphicsDevice.Viewport.Height) - 250, helpUnsel.Width, helpUnsel.Height);
            creditRect = new Rectangle(g.GraphicsDevice.Viewport.Width / 6, (g.GraphicsDevice.Viewport.Height) - 150, creditsUnsel.Width, creditsUnsel.Height);
            backRect = new Rectangle(g.GraphicsDevice.Viewport.Width - 550, (g.GraphicsDevice.Viewport.Height) - 150, backUnsel.Width, backUnsel.Height);
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
                        startSel.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                            mMouseStatePrev.LeftButton == ButtonState.Released)
                        {
                            gameState = GameStates.INTRO;
                        }
                    }
                    else if (mMouseStateCurr.X >= instructRect.X && mMouseStateCurr.X < instructRect.X + instructRect.Width &&
                             mMouseStateCurr.Y >= instructRect.Y && mMouseStateCurr.Y < instructRect.Y + instructRect.Height)
                    {
                        helpSel.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                            mMouseStatePrev.LeftButton == ButtonState.Released)
                        {
                            mState = states.INSTRUCT;
                        }
                    }
                    else if (mMouseStateCurr.X >= creditRect.X && mMouseStateCurr.X < creditRect.X + creditRect.Width &&
                             mMouseStateCurr.Y >= creditRect.Y && mMouseStateCurr.Y < creditRect.Y + creditRect.Height)
                    {
                        creditsSel.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                            mMouseStatePrev.LeftButton == ButtonState.Released)
                        {
                            mState = states.CREDITS;
                        }
                    }
                    break;

                case states.INSTRUCT:
                    if (mMouseStateCurr.X >= backRect.X && mMouseStateCurr.X < backRect.X + backRect.Width &&
                        mMouseStateCurr.Y >= backRect.Y && mMouseStateCurr.Y < backRect.Y + backRect.Height)
                    {
                        backSel.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed)
                        {
                            mState = states.TITLE;
                        }
                    }

                    break;

                case states.CREDITS:
                    if (mMouseStateCurr.X >= backRect.X && mMouseStateCurr.X < backRect.X + backRect.Width &&
                        mMouseStateCurr.Y >= backRect.Y && mMouseStateCurr.Y < backRect.Y + backRect.Height)
                    {
                        backSel.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                        if (mMouseStateCurr.LeftButton == ButtonState.Pressed)
                        {
                            mState = states.TITLE;
                        }
                    }

                    break;
            }
            mMouseStatePrev = mMouseStateCurr;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Draw(menuScreen, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero,
                new Vector2((float)graphics.GraphicsDevice.Viewport.Width / (float)menuScreen.Width,
                            (float)graphics.GraphicsDevice.Viewport.Height / (float)menuScreen.Height), SpriteEffects.None, 0.0f);
            spriteBatch.Draw(title, new Vector2(graphics.GraphicsDevice.Viewport.Width / 20, graphics.GraphicsDevice.Viewport.Height / 30), null, Color.White, 0.0f, Vector2.Zero, 
                0.4f, SpriteEffects.None, 0.0f);

            switch (mState)
            {
                case states.TITLE:
                    if (mMouseStateCurr.X >= startRect.X && mMouseStateCurr.X < startRect.X + startRect.Width &&
                        mMouseStateCurr.Y >= startRect.Y && mMouseStateCurr.Y < startRect.Y + startRect.Height)

                        startSel.Draw(spriteBatch, new Vector2(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350));

                    else
                        spriteBatch.Draw(startUnsel, new Vector2(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
    
                    spriteBatch.Draw(helpUnsel, new Vector2(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 250), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(creditsUnsel, new Vector2(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 150), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);

                    break;

                case states.INSTRUCT:
                    spriteBatch.Draw(help, new Vector2(-100.0f, -10.0f), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(backUnsel, new Vector2(g.GraphicsDevice.Viewport.Width - 550, (g.GraphicsDevice.Viewport.Height) - 150), Color.White); 
                    break;

                case states.CREDITS:
                    spriteBatch.Draw(credits, new Vector2(-100.0f, -10.0f), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(backUnsel, new Vector2(g.GraphicsDevice.Viewport.Width - 550, (g.GraphicsDevice.Viewport.Height) - 150), Color.White); 
                    break;
            }
        }
    }
}
