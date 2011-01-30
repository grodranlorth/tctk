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

        private Texture2D rainbowIconSel;
        private Texture2D rainbowIconUnsel;
        private Texture2D rainbowIcon;

        private Texture2D bombIconSel;
        private Texture2D bombIconUnsel;
        private Texture2D bombIcon;

        private Texture2D missileIconSel;
        private Texture2D missileIconUnsel;
        private Texture2D missileIcon;

        private Camera mHUDCam;

        private MouseState mouseStateCurr;

        public void Load(ContentManager content, Viewport viewPort)
        {
            fluffyHUD = content.Load<Texture2D>("HUD/hudnewshiny");
            cursor = content.Load<Texture2D>("Cursor/fluffycursor");

            rainbowIconUnsel = content.Load<Texture2D>("HUD/rainbowmissle_icon");
            rainbowIconSel = content.Load<Texture2D>("HUD/rainbowmissle_icon_selected");
            
            bombIconUnsel = content.Load<Texture2D>("HUD/Bombheart_icon");
            bombIconSel = content.Load<Texture2D>("HUD/BombHeart_icon_selected");

            missileIconUnsel = content.Load<Texture2D>("HUD/Lollirocket_icon");
            missileIconSel = content.Load<Texture2D>("HUD/Lollirocket_icon_selected");

            rainbowIcon = rainbowIconSel;
            bombIcon = bombIconUnsel;
            missileIcon = missileIconUnsel;

            mHUDCam = new Camera(viewPort);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
//            spriteBatch.Begin(SpriteSortMode.Immediate,
//                    BlendState.AlphaBlend,
//                    SamplerState.LinearClamp,
//                    DepthStencilState.None,
//                    RasterizerState.CullCounterClockwise,
//                    null,
//                    mHUDCam.get_transformation());


            spriteBatch.Draw(bombIcon, new Vector2(graphics.GraphicsDevice.Viewport.Width - 250, 50.0f), Color.White);
            spriteBatch.Draw(missileIcon, new Vector2(graphics.GraphicsDevice.Viewport.Width - 150, 50.0f), Color.White);
            spriteBatch.Draw(rainbowIcon, new Vector2(graphics.GraphicsDevice.Viewport.Width - 350, 50.0f), Color.White);
            spriteBatch.Draw(fluffyHUD, new Vector2(graphics.GraphicsDevice.Viewport.Width / 20, graphics.GraphicsDevice.Viewport.Height - 125.0f), Color.White);
            spriteBatch.Draw(cursor, new Vector2(mouseStateCurr.X, mouseStateCurr.Y), Color.White);
//            spriteBatch.End();
        }

        public void Update(GameTime gameTime, WeaponManager wManager)
        {
            mouseStateCurr = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.D1) && wManager.checkReady())
            {
                if (rainbowIcon != rainbowIconSel)
                {
                    wManager.CurrentWeapon = 0;
                    rainbowIcon = rainbowIconSel;
                    bombIcon = bombIconUnsel;
                    missileIcon = missileIconUnsel;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D2) && wManager.checkReady())
            {
                if (bombIcon != bombIconSel)
                {
                    wManager.CurrentWeapon = 1;

                    rainbowIcon = rainbowIconUnsel;
                    bombIcon = bombIconSel;
                    missileIcon = missileIconUnsel;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D3) && wManager.checkReady())
            {
                if (missileIcon != missileIconSel)
                {
                    //wManager.CurrentWeapon = 2;

                    rainbowIcon = rainbowIconUnsel;
                    bombIcon = bombIconUnsel;
                    missileIcon = missileIconSel;
                }
            }
        }
    }
}
