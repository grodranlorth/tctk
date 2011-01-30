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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private CharacterManager mCharacterManager;
        private ItemManager mItemManager;

        MouseState mouseStateCurr, mouseStatePrev;
        KeyboardState keystate, prevKeyState;

        WeaponManager wM;

        private Texture2D cursor;

        private Texture2D level;

        private HUD hud;

        Camera mCam;

        private GameStates mCurrentState = GameStates.MENU;

        private Menu mMenu;
        private Item mItem;

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

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            graphics.ToggleFullScreen();

            this.Window.Title = "Too Cute To Live";

            graphics.ApplyChanges();

            Class1.graph = graphics;

            mCharacterManager = CharacterManager.GetInstance(Content, graphics);
            mCharacterManager.addCharacter("charMedium", Frames.CHAR_MED_FRAMES);

            mMenu = new Menu(this);

            mItemManager = new ItemManager(Content);

            mCam = new Camera(graphics.GraphicsDevice.Viewport);

            hud = new HUD();
            wM = new WeaponManager(Content, graphics);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.boy
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

//            mCharacterManager.Load();
            mMenu.Load(Content);

            hud.Load(Content, graphics.GraphicsDevice.Viewport);
            cursor = Content.Load<Texture2D>("Cursor/fluffycursor");
            level = Content.Load<Texture2D>("Levels/Level_ver02");
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
                    hud.Update(gameTime);

                    mCam.Zoom += (mouseStatePrev.ScrollWheelValue - mouseStateCurr.ScrollWheelValue)/100;

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
                        mCharacterManager.addCharacter("charMedium", Frames.CHAR_MED_FRAMES);
                        //mCharacterManager.addCharacter("charMedium", new Vector2(300, 300), Frames.CHAR_MED_FRAMES);
                    }

                    mItemManager.Update(gameTime);
                    mCharacterManager.Update(gameTime, mItemManager.itemList);
                    wM.Update(gameTime);

                    break;

                case GameStates.MENU:
                    mMenu.Update(gameTime, ref mCurrentState);
                    break;

//                case gameStates.PAUSE:
                    /* TODO - Add pause state */
//                    break;

                case GameStates.SCORING:
                    /* TODO - Add score state */
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
                    Console.WriteLine("Width: " + (float)graphics.GraphicsDevice.Viewport.Width / (float)level.Width);
                    Console.WriteLine("Height: " + (float)graphics.GraphicsDevice.Viewport.Height / (float)level.Height);

                    spriteBatch.Draw(level, new Vector2(0.0f, 0.0f), null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 
                                     new Vector2((float)graphics.GraphicsDevice.Viewport.Width / (float)level.Width, 
                                                 (float)graphics.GraphicsDevice.Viewport.Height / (float)level.Height),
                                     SpriteEffects.None, 0.0f);
                    //spriteBatch.Draw(level, Vector2.Zero, Color.White);
                    mCharacterManager.Draw(spriteBatch);
                    mItemManager.Draw(spriteBatch);
                    wM.Draw(spriteBatch);
                    hud.Draw(spriteBatch, graphics);

                    break;

                case GameStates.MENU:
                    mMenu.Draw(spriteBatch);
                    spriteBatch.Draw(cursor, new Vector2(mouseStateCurr.X, mouseStateCurr.Y), Color.White);
                    break;

//                case gameStates.PAUSE:
                    /* TODO - Add pause draw stuff */
//                    break;

                case GameStates.SCORING:
                    /* TODO - Add score draw stuff */
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
