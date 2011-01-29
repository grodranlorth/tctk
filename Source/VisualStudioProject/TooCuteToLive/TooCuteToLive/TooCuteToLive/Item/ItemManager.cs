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
    class ItemManager
    {
        public List<Item> itemList;
        ContentManager mContent;

        public ItemManager(ContentManager content)
        {
            itemList = new List<Item>();
            mContent = content;
        }

        public void addItem(string textureName, Vector2 position)
        {
            itemList.Add(new Item(textureName, position, mContent));
        }

        public void Update(GameTime gameTime)
        {
            foreach (Item item in itemList)
            {
                item.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Item item in itemList)
            {
                item.Draw(spriteBatch);
            }
        }

        public bool Collides(Vector3 point)
        {
            return true;
        }
    }
}
