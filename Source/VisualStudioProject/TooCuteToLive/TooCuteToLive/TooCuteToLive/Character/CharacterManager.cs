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
        private bool deleteCupcake;

        private Random rand = new Random();

        private float respawnRate;
        private float respawnTimer;

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
            respawnRate = 2.0f;
            respawnTimer = 0.0f;
            deleteCupcake = false;
        }

        public void Load()
        {

        }

        public void addBaby()
        {
            float xPos = (float)rand.Next(0, 800);
            float yPos = (float)rand.Next(0, 600);
            Vector2 babyPos = new Vector2(xPos, yPos);
            addCharacter("babyspawn", Frames.CHAR_BABY_SPAWN_FRAMES);

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
            bool noeat = false;

            /* Dirty hacks! */
            if (itemList.Count == 0)
                    noeat = true;


            foreach (Character character in characterList)
            {
                character.Update(gameTime, mGraphics);
                foreach (Item i in itemList)
                {
                    character.Destination = i.Position;

                    if ((Math.Abs(i.Position.X - character.Position.X) < 10) &&
                        Math.Abs(i.Position.Y - character.Position.Y) < 10)
                        i.eating = true;
                }

                if (noeat)
                    character.CeaseEating();

                states state = character.getState();
                character.Update(gameTime, mGraphics);
                if (state == states.WALKING)
                {

                    if (mItemList.Count > 0)
                    {
                        character.setSeek(true);
                        foreach (Item item in mItemList)
                        {

                            double xS = (double)(character.Position.X - item.Position.X);
                            double yS = (double)(character.Position.Y - item.Position.Y);

                            if (character.Distance > Math.Sqrt(xS * xS + yS * yS))
                            {
                                character.Distance = (int)Math.Sqrt(Math.Sqrt(xS * xS + yS * yS));
                                character.Destination = item.Position;
                                // Console.WriteLine(item.Position);
                            }
                        }
                    }
                    else if (mItemList.Count == 0)
                    {

                        character.setSeek(false);
                    }
                    //         }
                    foreach (Character character1 in characterList)
                    {
                        if (character.Collides(character1.BSphere))
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
                 //   Console.WriteLine("Killing at point " + point.X + " " + point.Y);
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

        public void respawn(float time, GameTime gameTime)
        {
            if (time <= 45 && time > 30)
            {
                respawnRate = 1.5f;
            }
            else if (time <= 30 && time > 15)
            {
                respawnRate = 1.0f;
            }
            else if (time <= 15)
            {
                respawnRate = 0.7f;
            }

            respawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (respawnTimer >= respawnRate)
            {
                addBaby();
                respawnTimer = 0;
            }
        }
    }
}
