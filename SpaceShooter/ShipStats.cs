using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    internal class ShipStats
    {
        public float
            mass,
            health,
            speed,

            projectilesPerShot, //om den skjuter flera skott samtidigt
            damage, //skada som skott gör
            projectileSize, //storlek på skott
            fireSpeed, //antal millisekunder mellan skott
            spread; //spridning av skott
        
        public ShipStats() //konstruktor för spelare
        {
            this.mass = 1f;
            this.health = 5f;
            this.speed = 0.04f;

            this.projectilesPerShot = 1f;
            this.damage = 1f;
            this.projectileSize = 4f;
            this.fireSpeed = 250f;
            this.spread = 0f;
        }

        //name??? vad sjutton
        public ShipStats(string name) //konstruktor för fiende 
        {
            this.mass = Math.Clamp(Common.random.Next(1, 4) * (float)Common.random.NextDouble(), 1.5f, 8);
            this.speed = 2 / this.mass;
            this.health = this.mass;

            this.projectilesPerShot = 0f;
            this.damage = 0f;
            this.projectileSize = 0f;
            this.fireSpeed = 0f;
            this.spread = 0f;
        }

        public ShipStats(float mass, float speed, float health, float projectilesPerShot, float damage, float projectileSize, float fireSpeed, float spread)
        {
            this.mass = mass;
            this.speed = speed;
            this.health = health;

            this.projectilesPerShot = projectilesPerShot;
            this.damage = damage;
            this.projectileSize = projectileSize;
            this.fireSpeed = fireSpeed;
            this.spread = spread;
        }
    }
}
