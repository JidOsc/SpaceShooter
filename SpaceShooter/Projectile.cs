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
    internal class Projectile : KineticObject
    {
        public bool outsideScreen = false;
        public float 
            damage,
            speed;

        public Projectile(Vector2 position, Vector2 size, string identifier, Vector2 velocity, Color color, float damage, float speed) : base(position, size, identifier, velocity)
        {
            layer = 0.4f;
            texture = Common.textures["projectile"];
            this.color = color;
            this.damage = damage;
            this.speed = speed;
        }

        override public void Update(GameTime gameTimev)
        {
            position += velocity * speed;
            hitbox.Location = (position - size / 2).ToPoint();
        }

        public bool IsOutOfBounds(){
            return 
                position.X < Game1.window.Location.X - size.X || //är den utanför skärmen på någon sida
                position.X > Game1.window.Width + size.X ||
                position.Y < Game1.window.Location.Y - size.Y ||
                position.Y > Game1.window.Height + size.Y;
        }
    }
}
