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
        GraphicsDeviceManager mGraphics;

        private Random rand = new Random();

        private static CharacterManager instance = null;
        public static CharacterManager Instance
        {
            get
            {
                return instance;
            }
        }

        public static CharacterManager GetInstance(ContentManager content, GraphicsDeviceManager graphics)
        {
            if (instance == null)
            {
                instance = new CharacterManager(content, graphics);
            }
            return instance;

        }
        private CharacterManager(ContentManager content, GraphicsDeviceManager graphics)
        {
            mGraphics = graphics;
            characterList = new List<Character>();
            mContent = content;
        }

        public void Load()
        {

        }

        public void addCharacter(string textureName,int frameCount)
        {
            AudioManager.Play(AudioManager.hi);
            characterList.Add(new Character(textureName, 
                              new Vector2(rand.Next(100, mGraphics.GraphicsDevice.Viewport.Width - 100), 
                                          rand.Next(200, mGraphics.GraphicsDevice.Viewport.Height - 100)),
                              mContent, frameCount));
        }

        public void addCharacter(string textureName, Vector2 pos, int frameCount)
        {
            characterList.Add(new Character(textureName, pos, mContent, frameCount));
        }

        public void Update(GameTime gameTime, List<Item> itemList)
        {
            List<Character> removeList = new List<Character>();
            mItemList = itemList;
            foreach (Character character in characterList)
            {
                character.Update(gameTime, mGraphics);
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
                    if (character.Collides(new Vector2(character1.BSphere.Center.X, character1.BSphere.Center.Y)))
                    {
                        if (character.OnFire())
                            character1.SetOnFire();
                        else if (character1.OnFire())
                            character.SetOnFire();

//                        if (character.OnFire() && !character.MultipleOfTwo) 
//                            character.Speed *= -2;
//                        else 
//                        {
//                            character.Speed *= -1/2;
//                            character.MultipleOfTwo = false;
//                        }

//                        if (character1.OnFire() && !character1.MultipleOfTwo)
//                            character1.Speed *= -2;
//                        else
//                        {
//                            character1.Speed *= -1/2;
//                            character1.MultipleOfTwo = false;
//                        }
                    }
                }

                if (character.Remove == true)
                {
                     removeList.Add(character);
                }
            }
            foreach (Character c in removeList)
            {
                characterList.Remove(c);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Character character in characterList)
            {
                character.Draw(spriteBatch);
            }
        }

        public void pointKill(Vector2 point)
        {
            foreach (Character character in characterList)
            {
                if (!character.OnFire() && character.Collides(point))
                {
                    character.kill();
                }
            }
        }

        public void Clear()
        {
            characterList.Clear();
        }

        public bool isEmpty()
        {
            return (characterList.Count == 0);
        }
    }
}