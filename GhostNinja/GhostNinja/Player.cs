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
    class Player
    {
        
        public int speed = 180;
        private Direction direction = Direction.Right;
        static public Vector2 defaultPosition = new Vector2(550, 360);
        public Vector2 position = defaultPosition;

        public AnimatedSprite animate;
        public AnimatedSprite[] animations = new AnimatedSprite[2];
        

        public Vector2 Position
        {
            get { return position; }
        }


        public void Update(GameTime gameTime, Controller gameController)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; // Will protect user form dropped framerates

            if(gameController.inGame)
            {
                if (kState.IsKeyDown(Keys.Right) && position.X < 1280)
                {
                    direction = Direction.Right;
                    position.X += speed * dt;
                }
                if (kState.IsKeyDown(Keys.Left) && position.X > 0)
                {
                    direction = Direction.Left;
                    position.X -= speed * dt;
                }
                if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
                {
                    position.Y -= speed * dt;
                }
                if (kState.IsKeyDown(Keys.Down) && position.Y < 720)
                {
                    position.Y += speed * dt;
                }
            }
            //if(kState.IsKeyDown(Keys.Space))
            //{
            //    gameController.inGame = false;
            //}
 
            switch (direction)
            {
                case Direction.Right:
                    animate = animations[0];
                    break;
                case Direction.Left:
                    animate = animations[1];
                    break;
            }
            animate.Update(gameTime);
        }
    }
}
