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
    class Intro
    {
        private MouseState mouseCurrState, mousePrevState;
        private SpriteFont font;
        private float countDown;

        public Intro()
        {
            countDown = 6.0f;
        }

        public void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/kootenay");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Objective: Defeat all the sparkles before time runs out.", new Vector2( 25.0f, 100.0f), Color.Black);
            spriteBatch.DrawString(font, "Use any of the supplied weapons to accomplish your goal", new Vector2(25.0f, 150.0f), Color.Black);
            spriteBatch.DrawString(font, "Game Starts in: " + (int)countDown, new Vector2(25.0f, 200.0f), Color.Black);
        }

        public void Update(GameTime gameTime, ref GameStates gameState)
        {
            mouseCurrState = Mouse.GetState();

            countDown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (countDown <= 1)
                gameState = GameStates.GAME;
            mousePrevState = mouseCurrState;
        }
    }
}