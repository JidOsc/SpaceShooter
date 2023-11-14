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
    internal class KineticObject : GameObject
    {
        public Vector2
            velocity;

        public KineticObject(Vector2 position, Vector2 size, string identifier, Vector2 velocity) : base(position, size, identifier)
        {
            this.velocity = velocity;
        }
    }
}
