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
            if (itemList.Count < 1)
            {
		AudioManager.oooooh.Play();
                itemList.Add(new Item(textureName, position, mContent));
            }
        }

        public void Update(GameTime gameTime)
        {
            List<Item> removeList = new List<Item>();

            foreach (Item item in itemList)
            {
                item.Update(gameTime);

                if (item.Remove == true)
                    removeList.Add(item);
            }

            foreach (Item item in removeList)
            {
                itemList.Remove(item);
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
