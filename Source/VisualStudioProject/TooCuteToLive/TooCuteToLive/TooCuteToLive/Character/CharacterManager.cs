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
        
        public CharacterManager(ContentManager content)
        {
            characterList = new List<Character>();
            mContent = content;
        }

        public void addCharacter(string textureName, Vector2 position)
        {
            characterList.Add(new Character(textureName, position, mContent));
        }

        public void Update(GameTime gameTime)
        {
            foreach (Character character in characterList)
            {
                character.Update(gameTime);
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
                    character.kill();
                }
            }
        }
    }
}
