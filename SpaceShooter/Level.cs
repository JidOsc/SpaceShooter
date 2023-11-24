using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Timers;

namespace SpaceShooter
{
    internal class Level
    {
        public static List<GameObject> gameObjects; 
        static List<GameObject> 
            tempGameObjects,
            toDeleteGameObjects;

        Timer spawnTimer;
        public float
            spawnDelay = 2000f;

        public int
            score;

        Label scoreText;

        public Level()
        {
            tempGameObjects = new List<GameObject>();
            toDeleteGameObjects = new();

            spawnTimer = new(spawnDelay);
            spawnTimer.Elapsed += OnSpawnTimerStop;
            spawnTimer.AutoReset = true;
            spawnTimer.Start();

            scoreText = new(new Vector2(10, 10), new Vector2(64, 64), "label", "SCORE: ");
            scoreText.font = Common.fonts["scoreFont"];

            gameObjects = new List<GameObject>()
            {
                new PlayerShip(new Vector2(500, 250), new Vector2(64, 64), "player", Vector2.Zero),
                new EnemyShip(new Vector2(300, 200), new Vector2(64, 64), "enemy", Vector2.Zero)
            };
        }

        public void Update(GameTime gameTime)
        {
            /*foreach(EnemyShip enemyShip in gameObjects){
                foreach(GameObject gameObject in gameObjects){
                    gameObject.Update(gameTime);

                    if(enemyShip != gameObject && enemyShip.hitbox.Intersects(gameObject.hitbox) && gameObject !is EnemyShip){
                        if(enemyShip.health <= 0){
                            EnemyDestroyed();
                            toDeleteGameObjects.Add(enemyShip);
                        }

                        toDeleteGameObjects.Add(gameObject);
                    }
                }
            }*/
            //((PlayerShip)gameObjects[0]).Update();
            foreach (GameObject gameObject in gameObjects)
            {
                if(gameObject is EnemyShip){
                    EnemyShip enemyShip = (EnemyShip)gameObject;

                    foreach(GameObject gameObject1 in gameObjects){
                        if(gameObject1 is Projectile){
                            Projectile projectile = (Projectile)gameObject1;

                            if(gameObject1 != gameObject 
                            && gameObject.hitbox.Intersects(gameObject1.hitbox) 
                            && !(gameObject1 is EnemyShip)){
                                enemyShip.health -= projectile.damage;
                                
                                if(enemyShip.health <= 0){
                                    EnemyDestroyed();
                                    toDeleteGameObjects.Add(gameObject);
                                }

                                toDeleteGameObjects.Add(gameObject1);
                            }
                        } 
                    }
                }
                gameObject.Update(gameTime);
            }
            foreach(GameObject tempGameObject in tempGameObjects){
                gameObjects.Add(tempGameObject);
            }
            foreach(GameObject gameObject in toDeleteGameObjects){
                gameObjects.Remove(gameObject);
            }
            toDeleteGameObjects = new();
            tempGameObjects = new();

            scoreText.text = "SCORE: " + score;
        }

        void OnSpawnTimerStop(Object source, ElapsedEventArgs e){
            //spawnTimer.Stop();
            SpawnEnemy();
            //spawnTimer.Start();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
            scoreText.Draw(spriteBatch);
        }

        public static Vector2 GetRelationToPlayer(EnemyShip enemy){
            return Vector2.Normalize(enemy.position - gameObjects[0].position);
        }

        void SpawnEnemy(){

            tempGameObjects.Add(
                new EnemyShip(
                    RandomPositionOutside(),
                    new Vector2(32, 32),
                    "enemy",
                    Vector2.Zero
                ));
        }

        void EnemyDestroyed(){
            score += 1;
            spawnTimer.Interval = spawnDelay * Math.Pow(0.98, score);
        }
        
        Vector2 RandomPositionOutside(){
            Vector2 tempPosition = Vector2.Zero;
            
            switch(Common.random.Next(1, 5)){
                case 1:
                    tempPosition.X = Game1.window.X - 10;
                    tempPosition.Y = Common.random.Next(Game1.window.Location.Y, Game1.window.Size.Y);
                    break;

                case 2:
                    tempPosition.X = Game1.window.Size.X + 10;
                    tempPosition.Y = Common.random.Next(Game1.window.Location.Y, Game1.window.Size.Y);
                    break;

                case 3:
                    tempPosition.Y = Game1.window.Y - 10;
                    tempPosition.X = Common.random.Next(Game1.window.Location.X, Game1.window.Size.X);
                    break;

                case 4:
                    tempPosition.Y = Game1.window.Size.Y + 10;
                    tempPosition.X = Common.random.Next(Game1.window.Location.X, Game1.window.Size.X);
                    break;
            }

            return tempPosition;
        }

        public static void SpawnProjectile(Projectile projectile){
            Debug.WriteLine("projektil skapad");
            tempGameObjects.Add(projectile);
        }
    }
}
