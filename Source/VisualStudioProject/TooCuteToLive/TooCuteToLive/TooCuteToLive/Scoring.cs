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
    class Scoring
    {
        MouseState mouseCurrState, mousePrevState;
        private int score;
        private SpriteFont font;
        private float timer;

        public Scoring()
        {
            timer = 5.0f;
            score = 0;
        }

        public void Load(ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/kootenay");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(100.0f, 100.0f), Color.Black);
            if (score == 0)
            {
                if (timer >= 2 && timer <= 4)
                    spriteBatch.DrawString(font, "You Failed to kill the Sparkles", new Vector2(100.0f, 200.0f), Color.Black);
                else if (timer <= 1)
                    spriteBatch.DrawString(font, "Goodbye", new Vector2(100.0f, 200.0f), Color.Black);
            }
            else
            {
                if (timer >= 2 && timer <= 4)
                    spriteBatch.DrawString(font, "Congratulations!!! You killed all the Sparkles", new Vector2(100.0f, 200.0f), Color.Black);
                else if (timer < 1)
                    spriteBatch.DrawString(font, "You will play again", new Vector2(100.0f, 200.0f), Color.Black);
            }
        }

        public void Update(GameTime gameTime, ref GameStates gameState)
        {
            mouseCurrState = Mouse.GetState();

            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0.0f)
                gameState = GameStates.EXIT;

            mousePrevState = mouseCurrState;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
