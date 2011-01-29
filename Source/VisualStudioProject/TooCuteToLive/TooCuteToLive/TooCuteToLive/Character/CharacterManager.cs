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
            List<Character> removeList = new List<Character>();
            mItemList = itemList;
            foreach (Character character in characterList)
            {
                character.Update(gameTime);
                if (mItemList.Count > 0)
                {
//                    character.setSeek(true);
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
//                else if (mItemList.Count == 0)
//                    character.setSeek(false);

                foreach (Character character1 in characterList)
                {
                    if (character.Collides(character1.BSphere))
                    {
                        character.Speed *= -1;
                        character1.Speed *= -1;
                    }
                }

                if (character.Remove == true)
                {
                     removeList.Add(character);
                }
            }
            if (removeList.Count > 0)
            {
                characterList.Remove(removeList.ElementAt(0));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Character character in characterList)
            {
                character.Draw(spriteBatch);
            }
        }

 //       public bool Collides(Vector3 point)
 //       {
 //           return true;
 //       }

        public void pointKill(Vector2 point)
        {
            foreach (Character character in characterList)
            {
                if (character.Collides(point))
                {
                    character.kill();
                }
            }
        }
    }
}