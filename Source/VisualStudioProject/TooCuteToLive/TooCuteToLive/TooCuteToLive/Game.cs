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
    public static class HackRandom
    {
        public static Random rand = new Random();
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private SpriteFont font;

        private CharacterManager mCharacterManager;
        private ItemManager mItemManager;

        MouseState mouseStateCurr, mouseStatePrev;
        KeyboardState keystate, prevKeyState;

        WeaponManager wM;

        private Texture2D cursor;

        private Texture2D level;

        private HUD hud;

        private float timer;

        Camera mCam;

        private GameStates mCurrentState = GameStates.MENU;

        private Menu mMenu;
        private Scoring mScoring;
        private Intro mIntro;

        Random rand = new Random();

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            //graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 1024;

            //graphics.ToggleFullScreen();

            this.Window.Title = "Too Cute To Live";

            graphics.ApplyChanges();

            Class1.graph = graphics;

            mCharacterManager = CharacterManager.GetInstance(Content, graphics);

            mMenu = new Menu(this);
            mScoring = new Scoring();
            mIntro = new Intro();

            mItemManager = new ItemManager(Content);

            mCam = new Camera(graphics.GraphicsDevice.Viewport);

            hud = new HUD();
            wM = new WeaponManager(Content, graphics);

            /* Number of seconds the level will run */
            timer = 60.0f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.boy
        /// </summary>
        protected override void LoadContent()
        {
            AudioManager.Load(Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

//            mCharacterManager.Load();
            mMenu.Load(Content);
            mIntro.Load(Content);
            mScoring.Load(Content);

            hud.Load(Content, graphics.GraphicsDevice.Viewport);
            cursor = Content.Load<Texture2D>("Cursor/fluffycursor");
            level = Content.Load<Texture2D>("Levels/Level_ver02");
            font = Content.Load<SpriteFont>("Fonts/kootenay");

            for (int i = 0; i < 5; i++)
                mCharacterManager.addBaby();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for colr
        /// lisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exitsor
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.C))
                mCharacterManager.Clear();

            mouseStateCurr = Mouse.GetState();
            keystate = Keyboard.GetState();

            switch (mCurrentState)
            {
                case GameStates.GAME:
                    mCharacterManager.respawn(timer, gameTime);
                    if (AudioManager.chillin.State != SoundState.Playing)
                    {
                        AudioManager.chillin2.Stop();
                        AudioManager.Play(AudioManager.chillin);
                    }

                    hud.Update(gameTime, wM);

                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer <= 0 || mCharacterManager.isEmpty())
                    {
                        mScoring.Score = (int)timer;
                        mCurrentState = GameStates.SCORING;
                    }
//                    mCam.Zoom += (mouseStatePrev.ScrollWheelValue - mouseStateCurr.ScrollWheelValue)/100;

                    if (mouseStateCurr.LeftButton == ButtonState.Pressed && 
                        mouseStatePrev.LeftButton == ButtonState.Released)
                    {
                        Vector2 pos = new Vector2(mouseStateCurr.X, mouseStateCurr.Y);
                        wM.UseCur(pos, mouseStateCurr.Y);
                    }
                    else if (mouseStateCurr.RightButton == ButtonState.Pressed &&
                        mouseStatePrev.RightButton == ButtonState.Released)
                    {
                        Vector2 pos = new Vector2(mouseStateCurr.X, mouseStateCurr.Y);
                        mItemManager.addItem("cupcake", pos);
                    }
                    else if (keystate.IsKeyDown(Keys.A) && prevKeyState.IsKeyUp(Keys.A))
                    {
                        mCharacterManager.addBaby();
                    }

                    mItemManager.Update(gameTime);
                    mCharacterManager.Update(gameTime, mItemManager.itemList);
                    wM.Update(gameTime);

                    break;

                case GameStates.MENU:
                    if (AudioManager.chillin2.State != SoundState.Playing)
                    {
                        AudioManager.chillin.Stop();
                        AudioManager.Play(AudioManager.chillin2);
                    }

                    mMenu.Update(gameTime, ref mCurrentState);
                    break;

                case GameStates.INTRO:
                    mIntro.Update(gameTime, ref mCurrentState);
                    break;

                case GameStates.SCORING:
                    mScoring.Update(gameTime, ref mCurrentState);
                    break;

                case GameStates.EXIT:
                    this.Exit();
                    break;
            }
            mouseStatePrev = mouseStateCurr;
            prevKeyState = keystate;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

//            spriteBatch.Begin(SpriteSortMode.Immediate,
//                                BlendState.AlphaBlend,
//                                SamplerState.LinearClamp,
//                                DepthStencilState.None,
//                                RasterizerState.CullCounterClockwise,
//                                null,
//                                mCam.get_transformation());

            spriteBatch.Begin();

            switch (mCurrentState)
            {
                case GameStates.GAME:
                    spriteBatch.Draw(level, new Vector2(0.0f, 0.0f), null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 
                                     new Vector2((float)graphics.GraphicsDevice.Viewport.Width / (float)level.Width, 
                                                 (float)graphics.GraphicsDevice.Viewport.Height / (float)level.Height),
                                     SpriteEffects.None, 0.0f);
                    spriteBatch.DrawString(font, "Time: " + (int)timer, new Vector2(100.0f, 30.0f), Color.Black);


                    mItemManager.Draw(spriteBatch);
                    mCharacterManager.Draw(spriteBatch);
                    wM.Draw(spriteBatch);
                    hud.Draw(spriteBatch, graphics);

                    break;

                case GameStates.MENU:
                    mMenu.Draw(spriteBatch, graphics);
                    spriteBatch.Draw(cursor, new Vector2(mouseStateCurr.X, mouseStateCurr.Y), Color.White);
                    break;

                case GameStates.INTRO:
                    mIntro.Draw(spriteBatch);
                    break;

                case GameStates.SCORING:
                    mScoring.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
