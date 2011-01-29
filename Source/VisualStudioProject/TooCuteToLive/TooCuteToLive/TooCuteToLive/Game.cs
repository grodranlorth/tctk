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

        Weapon mRb;
        float mWepTime;

        private Texture2D fluffyHUD;
        private Texture2D cursor;
        private Texture2D cupcakeIcon;
        private Texture2D rainbowIcon;
        private Texture2D cupcakeIconSel;
        private Texture2D rainbowIconSel;

        private GameStates mCurrentState = GameStates.MENU;

        private Menu mMenu;
        private Item mItem;

        private const int CHAR_MED_FRAMES = 16;
        private const int CHAR_MED_ON_FIRE_FRAMES = 12;

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
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            //graphics.ToggleFullScreen();

            this.Window.Title = "Too Cute To Live";

            graphics.ApplyChanges();

            mCharacterManager = CharacterManager.GetInstance(Content);
            mCharacterManager.addCharacter("charMediumOnFire", Vector2.Zero, CHAR_MED_ON_FIRE_FRAMES);

            mRb = new Weapon("rainbow", Content);
            mWepTime = 0;

            mMenu = new Menu(this);

            mItemManager = new ItemManager(Content);

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

            fluffyHUD = Content.Load<Texture2D>("HUD/FluffyHUD");
            cursor = Content.Load<Texture2D>("Cursor/fluffycursor");
            cupcakeIcon = Content.Load<Texture2D>("HUD/Cupcake_icon");
            cupcakeIconSel = Content.Load<Texture2D>("HUD/Cupcake_icon_selected");
            rainbowIcon = Content.Load<Texture2D>("HUD/rainbowmissle_icon");
            rainbowIconSel = Content.Load<Texture2D>("HUD/rainbowmissle_icon_selected");
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

            mouseStateCurr = Mouse.GetState();
            keystate = Keyboard.GetState();
            
            switch (mCurrentState)
            {
                case GameStates.GAME:
                    if (mouseStateCurr.LeftButton == ButtonState.Pressed && 
                        mouseStatePrev.LeftButton == ButtonState.Released)
                    {
                        Vector2 pos = new Vector2(mouseStateCurr.X - mRb.getWidth()/2, mouseStateCurr.Y);
                        mRb.Strike(pos, 1, mouseStateCurr.Y);
                        mWepTime = 1;
                    }
                    else if (mouseStateCurr.RightButton == ButtonState.Pressed &&
                        mouseStatePrev.RightButton == ButtonState.Released)
                    {
                        Vector2 pos = new Vector2(mouseStateCurr.X, mouseStateCurr.Y);
                        mItemManager.addItem("cupcake", pos);
                    }
                    else if (keystate.IsKeyDown(Keys.A) && prevKeyState.IsKeyUp(Keys.A))
                    {
                        mCharacterManager.addCharacter("charMedium", Vector2.Zero, CHAR_MED_FRAMES);
                    }

                    mItemManager.Update(gameTime);
                    mCharacterManager.Update(gameTime, mItemManager.itemList);
                    mRb.Update(gameTime, mouseStateCurr.Y);

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

            spriteBatch.Begin();

            switch (mCurrentState)
            {
                case GameStates.GAME:
                    mCharacterManager.Draw(spriteBatch);
                    mItemManager.Draw(spriteBatch);
                    mRb.Draw(spriteBatch);

                    spriteBatch.Draw(fluffyHUD, new Vector2(graphics.GraphicsDevice.Viewport.Width / 10, graphics.GraphicsDevice.Viewport.Height - 150.0f), Color.White);
                    spriteBatch.Draw(cupcakeIcon, new Vector2(graphics.GraphicsDevice.Viewport.Width - 150, 50.0f), Color.White);
                    spriteBatch.Draw(rainbowIconSel, new Vector2(graphics.GraphicsDevice.Viewport.Width - 250, 50.0f), Color.White); 

                    break;

                case GameStates.MENU:
                    mMenu.Draw(spriteBatch);
                    break;

//                case gameStates.PAUSE:
                    /* TODO - Add pause draw stuff */
//                    break;

                case GameStates.SCORING:
                    /* TODO - Add score draw stuff */
                    break;
            }

            spriteBatch.Draw(cursor, new Vector2(mouseStateCurr.X, mouseStateCurr.Y), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
