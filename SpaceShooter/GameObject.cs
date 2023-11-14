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
    internal class GameObject
    {
        public Vector2
            position,
            size;

        public string
            identifier;

        public Texture2D
            texture;

        public float
            rotation = 0.1f;

        public GameObject(Vector2 position, Vector2 size, string identifier)
        {
            this.position = position;
            this.size = size;
            this.identifier = identifier;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture, 
                new Rectangle(position.ToPoint(), size.ToPoint()),
                null, 
                Color.White, 
                rotation, 
                new Vector2(texture.Width / 2, texture.Height / 2), 
                SpriteEffects.None, 
                1
                );

            /*spriteBatch.Draw(
                texture,
                new Rectangle(position.ToPoint(), size.ToPoint()),
                Color.White
                );*/
        }
    }
}
