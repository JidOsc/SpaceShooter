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
    internal abstract class GameObject
    {
        public Vector2
            position,
            size;

        public string
            identifier;

        public Texture2D
            texture;

        public float
            rotation = 0.1f,
            layer;

        public Color
            color = Color.White;

        public Rectangle
            hitbox;

        public bool
            debug = false;

        public GameObject(Vector2 position, Vector2 size, string identifier)
        {
            this.position = position;
            this.size = size;
            this.identifier = identifier;

            hitbox = new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture, 
                new Rectangle(position.ToPoint(), size.ToPoint()),
                null, 
                color, 
                rotation, 
                new Vector2(texture.Width / 2, texture.Height / 2), 
                SpriteEffects.None, 
                layer
                );

            if(debug){
                spriteBatch.Draw(
                    Common.textures["projectile"], 
                    hitbox.Location.ToVector2(), 
                    new Rectangle(hitbox.Location, hitbox.Size),
                    Color.Red * 0.5f);
            }
            /*spriteBatch.Draw(
                texture,
                new Rectangle(position.ToPoint(), size.ToPoint()),
                Color.White
                );*/
        }
    }
}
