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

        public ShipStats stats = new("player");

        float currentSecondsPassed = 0f;

        public PlayerShip(Vector2 position, Vector2 size, string identifier, Vector2 velocity) : base(position, size, identifier, velocity)
        {
            layer = 0.2f;
            texture = Common.textures["player2"];
        }

        override public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = Game1.mouseState.Position.ToVector2();
            relationToMouse = new Vector2(position.X - mousePosition.X, position.Y - mousePosition.Y);

            if(Game1.mouseState.LeftButton == ButtonState.Pressed && currentSecondsPassed + stats.fireSpeed/1000 < gameTime.TotalGameTime.TotalSeconds){
                //firespeed divideras på 1000 för att värdet anges i millisekunder

                if(stats.projectilesPerShot > 1)
                {
                    for(int i = 0; i < stats.projectilesPerShot; i++) //skapar flera projektiler om den borde
                    {
                        SpawnProjectile();
                    }
                }
                else
                {
                    SpawnProjectile();
                }
                currentSecondsPassed = (float)gameTime.TotalGameTime.TotalSeconds;
            }


            if (Game1.mouseState.RightButton == ButtonState.Pressed || Game1.keyboardState.IsKeyDown(Keys.Space))
            {
                velocity = 1f * velocity + -1 * Math.Max(0.1f, stats.speed) * Vector2.Normalize(relationToMouse); //new Vector2(Math.Clamp(relationToMouse.X, -2, 2), Math.Clamp(relationToMouse.Y, -2, 2)) * -1;
            }   //invertera hastigheten (eller 0.1, så den inte åker bakåt) multiplicerat med riktningen
            else
            {
                velocity /= 1.03f;
            }

            position += velocity;
            hitbox.Location = position.ToPoint();

            rotation = Common.VectorToRotation(relationToMouse);

            //kod för annorlunda rotation, pensionerad tills vidare
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

        void SpawnProjectile()
        {
            //skapar en projektil med massor av siffror
            Projectile newProjectile = new(
                                            this.position, 
                                            new Vector2(stats.projectileSize, stats.projectileSize),
                                            "playerProjectile", 
                                            Vector2.Normalize(relationToMouse 
                                                + new Vector2(Common.random.Next((int)-Math.Abs(stats.spread), (int)Math.Abs(stats.spread)), //absolutvärde för att förhindra krasch om spread blir negativ
                                                                Common.random.Next((int)-Math.Abs(stats.spread), (int)Math.Abs(stats.spread))) 
                                                * (float)Common.random.Next(-500, 0)),
                                            Color.Red,
                                            Math.Max(0.1f, stats.damage), //maxvärde för att damage inte ska kunna bli 0 eller lägre och göra spelet omöjligt
                                            stats.projectileSpeed
                                            );

            Level.SpawnProjectile(newProjectile);
        }
    }
}
