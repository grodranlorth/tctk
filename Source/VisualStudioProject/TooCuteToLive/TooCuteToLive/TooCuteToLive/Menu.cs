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
    class Button
    {
        private AnimatedSprite mSprite;
        private Texture2D mTex;
        private Vector2 mpos;
        private bool mover;
        private float mTexscale;
        private Rectangle rect;
        private string name;

        public float texScale
        {
            get { return mTexscale; }
            set { mTexscale = value; }
        }

        public float Spritescale
        {
            get { return mSprite.Scale; }
            set { mSprite.Scale = value; }
        }

        public bool Mover
        {
            get { return mover; }
        }

        public string Name
        {
            get { return name; }
        }

        public Button(Game g, ContentManager cm, string texname, string tname, float x,
            float y, int fc, float fps, float width, float height, bool once, string s)
        {
            mSprite = new AnimatedSprite();
            mSprite.Load(cm, tname, fc, fps, width, height, once);

            mTex = cm.Load<Texture2D>(texname);
            mTexscale = 0.5f;
            mpos = new Vector2(x, y);

            rect = new Rectangle(g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350, mTex.Width, mTex.Height);
            name = s;

            mover = false;
        }

        public bool MouseOver(MouseState mousepos)
        {
            return mousepos.X > mpos.X && mousepos.X < mpos.X + mTex.Width*mTexscale &&
                mousepos.Y > mpos.Y && mousepos.Y < mpos.Y + mTex.Height*mTexscale;
        }

        public void Draw(SpriteBatch sb)
        {
            if (mover)
                mSprite.Draw(sb, mpos);
            else
                sb.Draw(mTex, mpos, null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
        }

        public void Update(GameTime gt, MouseState ms)
        {
            mSprite.Update((float)gt.ElapsedGameTime.TotalSeconds);

            if (MouseOver(ms))
                mover = true;
            else
                mover = false;
        }
    }

    class Menu
    {
        Game g;

        private MouseState mMouseStateCurr, mMouseStatePrev;

        private Texture2D menuScreen;
        private Texture2D title;
        private Texture2D help;
        private Texture2D credits;

        LinkedList<Button> mBlist;
        Button back;

        private const int NUM_ITEMS = 3;
        private const int NUM_CREDIT_ITEMS = 1;
        private const int NUM_INSTRUCT_ITEMS = 1;

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

            mBlist = new LinkedList<Button>();
            mBlist.AddLast(new Button(g, content, "Menu/startbutton", "AnimatedSprites/startbuttonspritesheet",
                g.GraphicsDevice.Viewport.Width / 10, (g.GraphicsDevice.Viewport.Height) - 350, 8, 0.05f, 435.5f, 193, false, "play"));
            mBlist.Last.Value.texScale = 0.5f;
            mBlist.Last.Value.Spritescale = 0.67f;

            mBlist.AddLast(new Button(g, content, "Menu/helpbutton", "AnimatedSprites/helpbuttonspritesheet", g.GraphicsDevice.Viewport.Width / 8,
                g.GraphicsDevice.Viewport.Height - 250, 8, 0.05f, 438f, 283, false, "help"));
            mBlist.Last.Value.texScale = 0.5f;
            mBlist.Last.Value.Spritescale = 0.5f;

            mBlist.AddLast(new Button(g, content, "Menu/creditsbutton", "AnimatedSprites/creditsbuttonspritesheet", g.GraphicsDevice.Viewport.Width / 6,
                g.GraphicsDevice.Viewport.Height - 150, 8, 0.05f, 508f, 196, false, "credit"));
            mBlist.Last.Value.texScale = 0.5f;
            mBlist.Last.Value.Spritescale = .67f;

            back = new Button(g, content, "Menu/backbutton", "AnimatedSprites/backbuttonspritesheet", g.GraphicsDevice.Viewport.Width*0.23f,
                (g.GraphicsDevice.Viewport.Height) - 150, 8, 0.05f, 455f, 234, false, "back");

            menuScreen = content.Load<Texture2D>("Menu/menubackground");
            title = content.Load<Texture2D>("Menu/logo");

            credits = content.Load<Texture2D>("Menu/credits");
            help = content.Load<Texture2D>("Menu/help");
        }

        public void Update(GameTime gameTime, ref GameStates gameState)
        {
            mMouseStateCurr = Mouse.GetState();
            bool click = false;

            if (mMouseStateCurr.LeftButton == ButtonState.Pressed &&
                mMouseStatePrev.LeftButton == ButtonState.Released)
                click = true;

            switch (mState)
            {
                case states.TITLE:
                    foreach (Button b in mBlist)
                    {
                        b.Update(gameTime, mMouseStateCurr);
                        if (b.Mover && click)
                        {
                            /* XXX: Awesome hack */
                            if (b.Name == "play")
                                gameState = GameStates.INTRO;
                            else if (b.Name == "help")
                                mState = states.INSTRUCT;
                            else if (b.Name == "credit")
                                mState = states.CREDITS;
                        }
                    }
                    break;

                case states.INSTRUCT:

                    back.Update(gameTime, mMouseStateCurr);
                    if (back.Mover && click)
                        mState = states.TITLE;

                    break;

                case states.CREDITS:

                    back.Update(gameTime, mMouseStateCurr);
                    if (back.Mover && click)
                        mState = states.TITLE;

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
                    foreach (Button b in mBlist)
                    {
                        b.Draw(spriteBatch);
                    }
                    break;

                case states.INSTRUCT:
                    spriteBatch.Draw(help, new Vector2(g.GraphicsDevice.Viewport.Width*0.35f, -10.0f), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
                    back.Draw(spriteBatch);
                    break;

                case states.CREDITS:
                    spriteBatch.Draw(credits, new Vector2(g.GraphicsDevice.Viewport.Width * 0.35f, -10.0f), null, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.0f);
                    back.Draw(spriteBatch);
                    break;
            }
        }
    }
}
