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
    internal class ParticleSystem : GameObject
    {
        public string
            type;

        public ParticleSystem(Vector2 position, Vector2 size, string identifier, string type) : base(position, size, identifier)
        {
            this.type = type;
            texture = Common.textures["projectile"];
        }

        void RunSimulation(){
            switch(type){
                case "explosion":

                break;
            }
        }

        void EmitParticle(){

        }
    }
}
