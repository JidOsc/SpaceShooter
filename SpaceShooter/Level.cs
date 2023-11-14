using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    internal class Level
    {
        List<GameObject> gameObjects; 

        public Level()
        {
            gameObjects = new List<GameObject>()
            {
                new PlayerShip(new Vector2(500, 250), new Vector2(64, 64), "player", Vector2.Zero)
            };
        }

        public void Update()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}
