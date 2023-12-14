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
        public static Dictionary<string, List<GameObject>> 
            gameObjects;
        
        static List<GameObject> 
            tempGameObjects,
            toDeleteGameObjects;

        static PlayerShip 
            player;

        Timer
            spawnTimer;

        public float
            spawnDelay = 2000f;

        public int
            score;

        bool
            isChecking = false,
            isPaused = false;

        Label 
            scoreText,
            enemyAmount,
            projectileAmount;

        public Level()
        {
            tempGameObjects = new List<GameObject>();
            toDeleteGameObjects = new();

            StartSpawnTimer();

            scoreText = new(new Vector2(10, 10), new Vector2(64, 64), "scoreText", "SCORE: ");
            scoreText.font = Common.fonts["scoreFont"];

            enemyAmount = new(new Vector2(300, 10), new Vector2(64, 64), "scoreText", "ENEMIES: ");
            enemyAmount.font = Common.fonts["scoreFont"];

            projectileAmount = new(new Vector2(300, 30), new Vector2(64, 64), "scoreText", "PROJECTILES: ");
            projectileAmount.font = Common.fonts["scoreFont"];

            player = new PlayerShip(new Vector2(500, 250), new Vector2(64, 64), "player", Vector2.Zero);

            gameObjects = new Dictionary<string, List<GameObject>>()
            {
                {"enemies", new List<GameObject>()},

                {"projectiles", new List<GameObject>()},

                {"upgrades", new List<GameObject>()}
            };
        }

        public void Update(GameTime gameTime)
        {
            if (!isChecking && !isPaused)
            {
                isChecking = true;
                player.Update(gameTime);

                foreach (List<GameObject> lists in gameObjects.Values)
                {
                    foreach (GameObject gameObject in lists)
                    {
                        gameObject.Update(gameTime);
                    }
                }

                foreach (EnemyShip enemyShip in gameObjects["enemies"])
                {
                    enemyShip.color = Color.White;

                    if (enemyShip.hitbox.Intersects(player.hitbox))
                    {
                        player.stats.health -= 0.5f;

                        if(player.stats.health <= 0)
                        {
                            GameOver();
                            break;
                        }
                    }

                    foreach (Projectile projectile in gameObjects["projectiles"])
                    {
                        if (projectile.IsOutOfBounds())
                        {
                            toDeleteGameObjects.Add(projectile);
                            break;
                        }

                        if (enemyShip.hitbox.Intersects(projectile.hitbox))
                        {
                            EnemyShot(enemyShip, projectile);
                        }
                    }
                }
                foreach (GameObject tempGameObject in tempGameObjects)
                {
                    if (tempGameObject is EnemyShip)
                    {
                        gameObjects["enemies"].Add(tempGameObject);
                    }
                    else if (tempGameObject is Projectile)
                    {
                        gameObjects["projectiles"].Add(tempGameObject);
                    }
                }

                tempGameObjects.Clear();

                foreach (GameObject gameObject in toDeleteGameObjects)
                {
                    if (gameObject is EnemyShip)
                    {
                        gameObjects["enemies"].Remove(gameObject);
                    }
                    else if (gameObject is Projectile)
                    {
                        gameObjects["projectiles"].Remove(gameObject);
                    }
                }

                toDeleteGameObjects.Clear();

                if(score == 5)
                {
                    //ChooseUpgrade();
                }

                isChecking = false;
            }

            enemyAmount.text = "ENEMIES: " + gameObjects["enemies"].Count;
            projectileAmount.text = "PROJECTILES: " + gameObjects["projectiles"].Count;

            scoreText.text = "SCORE: " + score;
        }

        void ChooseUpgrade()
        {
            isPaused = true;

            short numberOfChoices = 3;

            for(int i = 0; i < numberOfChoices; i++)
            {
                gameObjects["upgrades"].Add(new Upgrade(
                    new Vector2((Game1.window.Width - 40) / numberOfChoices * i, 300),
                    new Vector2(200, 400),
                    "upgrade"
                    ));
            }

            foreach (Upgrade upgrade in gameObjects["upgrades"])
            {
                upgrade.ChangeRandomStat();
            }
        }

        void GameOver()
        {
            Game1.GameOver(score);
        }

        void StartSpawnTimer()
        {
            spawnTimer = new(spawnDelay);
            spawnTimer.Elapsed += OnSpawnTimerStop;
            spawnTimer.AutoReset = true;
            spawnTimer.Start();
        }

        void EnemyShot(EnemyShip enemyShip, Projectile projectile)
        {
            enemyShip.stats.health -= projectile.damage;
            enemyShip.color = Color.Red;

            if (enemyShip.stats.health <= 0)
            {
                EnemyDestroyed();
                toDeleteGameObjects.Add(enemyShip);
            }

            toDeleteGameObjects.Add(projectile);
        }

        void OnSpawnTimerStop(Object source, ElapsedEventArgs e){
            SpawnEnemy();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            foreach(List<GameObject> lists in gameObjects.Values)
            {
                foreach(GameObject gameObject in lists)
                {
                    gameObject.Draw(spriteBatch);
                }
            }

            scoreText.Draw(spriteBatch);
            enemyAmount.Draw(spriteBatch);
            projectileAmount.Draw(spriteBatch);
        }

        public static Vector2 GetRelationToPlayer(EnemyShip enemy){
            return Vector2.Normalize(enemy.position - player.position);
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

        void EnemyDestroyed()
        {
            score += 1;
            spawnTimer.Interval = spawnDelay * Math.Pow(0.99, score);
        }
        
        Vector2 RandomPositionOutside()
        {
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

        public static void SpawnProjectile(Projectile projectile)
        {
            Debug.WriteLine("projektil skapad");
            tempGameObjects.Add(projectile);
        }
    }
}
