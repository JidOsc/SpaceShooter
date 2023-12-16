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
    internal class EnemyShip : KineticObject
    {
        public ShipStats stats = new("enemy");

        public EnemyShip(Vector2 position, Vector2 size, string identifier, Vector2 velocity) : base(position, size, identifier, velocity)
        {
            texture = Common.textures["enemy2"];
            layer = 0.3f;

            size = new Vector2(stats.mass * 64, stats.mass * 64);
            hitbox.Size = size.ToPoint();
        }

        public override void Update(GameTime gameTime)
        {
            velocity = Level.GetRelationToPlayer(this) * -1 * stats.speed; //tar riktning mot spelare och färdas dit
            position += velocity;
            
            hitbox.Location = (position - size/2).ToPoint();
            rotation = Common.VectorToRotation(velocity * -1);
            
        }
    }
}
