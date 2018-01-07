using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GhostNinja
{
    class Log
    {
        public Vector2 position = new Vector2(600, 300);
        public int speed;
        public int radius = 59;
        public int windowHeight = 1280;
        public int windowWidth = 721;

        public bool offscreen = false;

        static Random rand = new Random();

        public Log(int newSpeed)
        {
            speed = newSpeed;

            

            position = new Vector2(windowHeight + radius, rand.Next(0, windowWidth));
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
