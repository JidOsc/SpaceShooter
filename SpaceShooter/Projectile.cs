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
        public float damage;

        public Projectile(Vector2 position, Vector2 size, string identifier, Vector2 velocity, Color color, float damage) : base(position, size, identifier, velocity)
        {
            layer = 0;
            texture = Common.textures["projectile"];
            this.color = color;
            this.damage = damage;
        }
    }
}
