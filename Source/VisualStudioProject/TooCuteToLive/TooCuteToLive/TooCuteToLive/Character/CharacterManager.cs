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
    class CharacterManager
    {
        private List<Character> characterList;
        ContentManager mContent;
        private List<Item> mItemList;
        
        private static CharacterManager instance = null;
        public static CharacterManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static CharacterManager GetInstance(ContentManager content)
        {
            if (instance == null)
            {
                instance = new CharacterManager(content);
            }
            return instance;

        }
        private CharacterManager(ContentManager content)
        {
            characterList = new List<Character>();
            mContent = content;
        }

        public void Load()
        {

        }

        public void addCharacter(string textureName, Vector2 position, int frameCount)
        {
            characterList.Add(new Character(textureName, position, mContent, frameCount));
        }

        public void Update(GameTime gameTime, List<Item> itemList)
        {
            mItemList = itemList;
            foreach (Character character in characterList)
            {
                character.Update(gameTime);
                if (mItemList.Count > 0)
                {
                    character.setSeek(true);
                    foreach (Item item in mItemList)
                    {

                        if (character.Distance > Math.Sqrt((double)(character.Position.X - item.Position.X) +
                                                           (double)(character.Position.X - item.Position.Y)))
                        {
                            character.Distance = (int)Math.Sqrt((double)(character.Position.X - item.Position.X) +
                                                           (double)(character.Position.X - item.Position.Y));
                            character.Destination = item.Position;
                        }
                    }
                }
                else if (mItemList.Count == 0)
                    character.setSeek(false);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Character character in characterList)
            {
                character.Draw(spriteBatch);
            }
        }

        public bool Collides(Vector3 point)
        {
            return true;
        }

        public void pointKill(Vector2 point)
        {
            foreach (Character character in characterList)
            {
                if (character.Collides(point))
                {
//                    character.changeImage("charMediumOnFire");
                    character.kill();
                }
            }
        }
    }
}
