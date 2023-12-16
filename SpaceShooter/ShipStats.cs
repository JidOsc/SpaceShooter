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
            projectileSpeed, //hur snabbt skott färdas
            spread; //spridning av skott
        
        public ShipStats() //normal konstruktor, används för uppgradering
        {
            this.mass = 0f;
            this.health = 0f;
            this.speed = 0f;

            this.projectilesPerShot = 0f;
            this.damage = 0f;
            this.projectileSize = 0f;
            this.fireSpeed = 0f;
            this.projectileSpeed = 0f;
            this.spread = 0f;
        }

        //name??? vad sjutton
        public ShipStats(string name) //konstruktor för fiende samt spelare
        {
            switch (name)
            {
                case "enemy":
                    this.mass = Math.Clamp(Common.random.Next(1, 4) * (float)Common.random.NextDouble(), 1.5f, 8);
                    this.speed = 2 / this.mass;
                    this.health = this.mass;

                    this.projectilesPerShot = 0f;
                    this.damage = 0f;
                    this.projectileSize = 0f;
                    this.fireSpeed = 0f;
                    this.projectileSpeed = 0f;
                    this.spread = 0f;
                    break;

                case "player":
                    this.mass = 1f;
                    this.health = 1f;
                    this.speed = 0.06f;

                    this.projectilesPerShot = 1f;
                    this.damage = 0.8f;
                    this.projectileSize = 4f;
                    this.fireSpeed = 250f;
                    this.projectileSpeed = 1f;
                    this.spread = 0f;
                    break;
            }
        }
    }
}
