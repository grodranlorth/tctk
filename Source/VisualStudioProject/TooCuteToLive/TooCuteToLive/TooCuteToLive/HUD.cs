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
    class HUD
    {
        private Texture2D fluffyHUD;
        private Texture2D cursor;
        private Texture2D cupcakeIcon;
        private Texture2D rainbowIcon;
        private Texture2D cupcakeIconSel;
        private Texture2D rainbowIconSel;

        private Camera mHUDCam;

        private MouseState mouseStateCurr;

        public void Load(ContentManager content, Viewport viewPort)
        {
            fluffyHUD = content.Load<Texture2D>("HUD/FluffyHUD");
            cursor = content.Load<Texture2D>("Cursor/fluffycursor");
            cupcakeIcon = content.Load<Texture2D>("HUD/Cupcake_icon");
            cupcakeIconSel = content.Load<Texture2D>("HUD/Cupcake_icon_selected");
            rainbowIcon = content.Load<Texture2D>("HUD/rainbowmissle_icon");
            rainbowIconSel = content.Load<Texture2D>("HUD/rainbowmissle_icon_selected");

            mHUDCam = new Camera(viewPort);
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,
                    BlendState.AlphaBlend,
                    SamplerState.LinearClamp,
                    DepthStencilState.None,
                    RasterizerState.CullCounterClockwise,
                    null,
                    mHUDCam.get_transformation());

            spriteBatch.Draw(fluffyHUD, new Vector2(graphics.GraphicsDevice.Viewport.Width / 10, graphics.GraphicsDevice.Viewport.Height - 150.0f), Color.White);
            spriteBatch.Draw(cupcakeIcon, new Vector2(graphics.GraphicsDevice.Viewport.Width - 150, 50.0f), Color.White);
            spriteBatch.Draw(rainbowIconSel, new Vector2(graphics.GraphicsDevice.Viewport.Width - 250, 50.0f), Color.White);
            spriteBatch.Draw(cursor, new Vector2(mouseStateCurr.X, mouseStateCurr.Y), Color.White);

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            mouseStateCurr = Mouse.GetState();
        }
    }
}
