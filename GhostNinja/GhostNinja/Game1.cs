/* Program : PROG2370 - Sec 5
 * Purpose : To create a monogame/xna final project.
 * Written by
 *      Tuan Truong
 *  
 *  Version History - The Ghost Ninja
 *  
 *  Reference
 *  
 *  RbWhitaker - Animation
 *  http://rbwhitaker.wikidot.com/monogame-texture-atlases-2
 *  
 *  Dec 29 -  1.0
 *      - concept of game
 *      - downloaded sprites and edit
 *      - loaded image sprites to content
 *      - create player class
 *      - move ninja
 *      
 *  Dec 30 - 1.1
 *      - stop ninja continuously moving
 *      - keyboard input
 *      - animate ninja
 *      
 *  Jan 3 - 1.2
 *      - added logs at random and speed
 *      - Bug.a - keep player from moving off the screen
 *  
 *  Jan 4 - 1.3
 *      - Bug.a fixed
 *      - added collision
 *      - added music
 *      
 *  Jan 5 - 1.4
 *      - Add timer
 *      - created menu
 *      
 *  Jan 7 - 1.5
 *      - polish game 
 *     
 * 
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GhostNinja
{
    enum Direction
    {
        Right,
        Left
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ghostNinjaSprite;
        Texture2D ghostRightSprite;
        Texture2D ghostLeftSprite;
        Texture2D logSprite;
        Texture2D backgroundSprite;
        Texture2D ghostNinjaMenu;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Song backgroundMusic;

        Player player = new Player();

        Controller gameController = new Controller();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ghostNinjaSprite = Content.Load<Texture2D>("ghostNinja");
            ghostRightSprite = Content.Load<Texture2D>("ghostRight");
            ghostLeftSprite = Content.Load<Texture2D>("ghostLeft");

            logSprite = Content.Load<Texture2D>("log");
            backgroundSprite = Content.Load<Texture2D>("background");
            ghostNinjaMenu = Content.Load<Texture2D>("ghostNinjaMenu");

            gameFont = Content.Load<SpriteFont>("ninjaFont");
            timerFont = Content.Load<SpriteFont>("timerFont");

            
            player.animations[0] = new AnimatedSprite(ghostRightSprite, 1, 4);
            player.animations[1] = new AnimatedSprite(ghostLeftSprite, 1, 4);

            //player.animate = new AnimatedSprite(ghostLeftSprite, 1, 4);
            //player.animate = new AnimatedSprite(ghostRightSprite, 1, 4);

            backgroundMusic = Content.Load<Song>("japaneseDrums");
            MediaPlayer.Play(backgroundMusic);

            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, gameController);
           
            gameController.Update(gameTime);

            for(int i = 0; i < gameController.logs.Count; i++)
            {
                if(gameController.logs[i].position.X < (0 - gameController.logs[i].radius))
                {
                    gameController.logs[i].offscreen = true;
                }

                int logHitBox = 30;

                gameController.logs[i].Update(gameTime);
                int sum = gameController.logs[i].radius + logHitBox;
                if(Vector2.Distance(gameController.logs[i].position, player.position) < sum)
                {
                    gameController.inGame = false;
                    player.position = Player.defaultPosition;
                    i = gameController.logs.Count; //Exit out of the loop aka generate logs
                    gameController.logs.Clear();
                }

            }


            gameController.logs.RemoveAll(l => l.offscreen == true);
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);

            if(gameController.inGame == false)
            {
                string timeMessage = "Time Elasped : " + Math.Floor(gameController.totalTime).ToString();
                Vector2 sizeOfText = gameFont.MeasureString(timeMessage);
                spriteBatch.Draw(ghostNinjaMenu, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(timerFont, timeMessage, new Vector2(800 - sizeOfText.X / 2, 360), Color.White);
            }

            for (int i = 0; i < gameController.logs.Count; i++)
            {
                Vector2 tempPosition = gameController.logs[i].position;
                int tempRadius = gameController.logs[i].radius;
                spriteBatch.Draw(logSprite, new Vector2(tempPosition.X - tempRadius, tempPosition.Y - tempRadius), Color.White);
            }

            spriteBatch.DrawString(timerFont, "Time : " + Math.Floor(gameController.totalTime).ToString(), new Vector2(3, 3), Color.White);
            spriteBatch.End();

            player.animate.Draw(spriteBatch, new Vector2(player.Position.X - 34, player.Position.Y - 50));

            base.Draw(gameTime);
        }
    }
}
