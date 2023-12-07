using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;


namespace SpaceShooter
{
    internal class PlayerShip : KineticObject
    {
        Vector2 relationToMouse;

        public ShipStats stats = new();

        Timer shotDelay;

        public PlayerShip(Vector2 position, Vector2 size, string identifier, Vector2 velocity) : base(position, size, identifier, velocity)
        {
            shotDelay = new Timer(stats.fireSpeed);
            shotDelay.Elapsed += OnTimerStop;

            layer = 0;

            texture = Common.textures["player2"];
        }

        override public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = Game1.mouseState.Position.ToVector2();
            relationToMouse = new Vector2(position.X - mousePosition.X, position.Y - mousePosition.Y);

            if(Game1.mouseState.LeftButton == ButtonState.Pressed && !shotDelay.Enabled){
                shotDelay.Start();

                if(stats.projectilesPerShot > 1)
                {
                    for(int i = 0; i < stats.projectilesPerShot; i++)
                    {
                        SpawnProjectile();
                    }
                }
                else
                {
                    SpawnProjectile();
                }
            }


            if (Game1.mouseState.RightButton == ButtonState.Pressed || Game1.keyboardState.IsKeyDown(Keys.Space))
            {
                velocity = 1f * velocity + -1 * stats.speed * Vector2.Normalize(relationToMouse); //new Vector2(Math.Clamp(relationToMouse.X, -2, 2), Math.Clamp(relationToMouse.Y, -2, 2)) * -1;
            }
            else
            {
                velocity /= 1.02f;
            }

            if (Game1.keyboardState.IsKeyDown(Keys.Q))
            {
                Upgrade upgrade = new Upgrade(
                    new ShipStats(
                        0,
                        0,
                        0,
                        1,
                        1,
                        0,
                        0,
                        0
                        ));

                upgrade.ModifyStats(this);
            }

            position += velocity;
            hitbox.Location = position.ToPoint();

            rotation = Common.VectorToRotation(relationToMouse);

            /*float newestRotation = Common.VectorToRotation(relationToMouse);

            if(newRotation != newestRotation){
                newRotation = newestRotation;
                t = 0;
                valueToLerp = rotation;
            }
            if(t < 1){
                t += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }*/

            //rotation = ((valueToLerp + 1) * (1 - t) + (newRotation + 1) * t) - 1;
        }

        void OnTimerStop(Object source, ElapsedEventArgs e)
        {
            shotDelay.Stop();
        }

        void SpawnProjectile()
        {
            Projectile newProjectile = new(
                                            this.position, 
                                            new Vector2(stats.projectileSize, stats.projectileSize),
                                            "playerProjectile",
                                            Vector2.Normalize(relationToMouse) * -5,
                                            Color.Red,
                                            stats.damage
                                            );

            Level.SpawnProjectile(newProjectile);
        }
    }
}
