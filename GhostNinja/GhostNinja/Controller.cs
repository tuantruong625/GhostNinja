﻿using System;
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
    class Controller
    {
        public List<Log> logs = new List<Log>();
        public double timer = 2D;
        public double maxTime = 2D;
        public int nextSpeed = 240;
        public float totalTime = 0f;

        public double defaultTimer = 2D;
        public double defaultMaxTime = 2D;
        public int defaultNextSpeed = 240;
        public float defaultTotalTime = 0f;


        public bool inGame = false;

        public void Update(GameTime gameTime)
        {
            if(inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if(kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    totalTime = defaultTotalTime;
                    timer = defaultTimer;
                    maxTime = defaultMaxTime;
                    nextSpeed = defaultNextSpeed;
                }
            }
            
            if(timer <= 0)
            {
                logs.Add(new Log(nextSpeed));
                timer = maxTime;
                if(maxTime > 0.5)
                {
                    maxTime -= 0.1D;
                }
                if(nextSpeed < 720)
                {
                    nextSpeed += 4;
                } 
            }
        }
    }
}
