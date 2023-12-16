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

        public float
            originSpawnDelay = 2f,
            spawnDelay = 2f,
            currentSecondsPassed = 0f;

        public int
            score;

        bool
            isChecking = false,
            isPaused = false;

        Label
            scoreText;

        short
            previousUpgradeScore = 0;

        public Level()
        {
            tempGameObjects = new List<GameObject>();
            toDeleteGameObjects = new();

            scoreText = new(new Vector2(10, 10), new Vector2(64, 64), "scoreText", "SCORE: ");

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
            if (!isChecking && !isPaused) //om vi inte redan kollar listor SAMT spelet inte är pausat
            {
                isChecking = true; //vi kollar, så den inte startar flera updates samtidigt

                if(currentSecondsPassed + spawnDelay < gameTime.TotalGameTime.TotalSeconds)
                {
                    //när sekunder som gått är större än förra gången + delayen mellan, skapa en fiende
                    SpawnEnemy();
                    currentSecondsPassed = (float)gameTime.TotalGameTime.TotalSeconds;
                }


                player.Update(gameTime);

                foreach (List<GameObject> lists in gameObjects.Values)
                {
                    foreach (GameObject gameObject in lists)
                    {
                        gameObject.Update(gameTime); //uppdatera alla spelobjekt
                    }
                }

                foreach (EnemyShip enemyShip in gameObjects["enemies"]) //loopa igenom fiender
                {
                    enemyShip.color = Color.White;

                    if (enemyShip.hitbox.Intersects(player.hitbox))
                    {
                        player.stats.health -= 0.5f; //om kolliderar med spelare

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
                foreach (GameObject tempGameObject in tempGameObjects) //lägger till objekt så de inte läggs till samtidigt som vi kollar
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

                foreach (GameObject gameObject in toDeleteGameObjects) //tar bort objekt så de inte raderas samtidigt som vi kollar
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

                if(score % 10 == 0 && previousUpgradeScore != score) //var femte poäng väljer du en uppgradering
                {
                    ChooseUpgrade();
                    previousUpgradeScore = (short)score;
                }

                isChecking = false;
            }

            else if(isPaused && !isChecking)
            {
                foreach(Upgrade upgrade in gameObjects["upgrades"])
                {
                    if (upgrade.MouseInside(Game1.mouseState.Position))
                    {
                        if (upgrade.IsPressed())
                        {
                            upgrade.ModifyStats(player);

                            gameObjects["upgrades"].Clear();
                            isPaused = false;
                            break;
                        }
                    }
                }
            }

            scoreText.text = "SCORE: " + score;
        }

        void ChooseUpgrade()
        {
            isPaused = true;

            short numberOfChoices = 3;
            short margin = 200;
            short width = 200;

            for(int i = 0; i < numberOfChoices; i++) //skapar x antal uppgraderingar
            {
                gameObjects["upgrades"].Add(new Upgrade(
                    new Vector2( 
                        ((Game1.window.Width - margin * 2) 
                        / numberOfChoices
                        - width / 2)
                        * (i + 1), 
                        300),
                    new Vector2(width, 400),
                    "upgradeText",
                    "Upgrade: " + i
                    ));
            }

            foreach (Upgrade upgrade in gameObjects["upgrades"])
            {
                upgrade.ChangeRandomStat(); //just nu ändras uppgraderingarna slumpartat, i framtiden kanske det ändras
            }
        }

        void GameOver()
        {
            Game1.GameOver(score);
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
            spawnDelay = (float)(originSpawnDelay * Math.Pow(0.99, score));
        }
        
        Vector2 RandomPositionOutside()
        {
            Vector2 tempPosition = Vector2.Zero;
            
            switch(Common.random.Next(1, 5)){ //4 scenarion som bestämmer var fienden skapas
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
