using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Slutprojekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Map> mapList = new List<Map>();
        List<Tower> towerList = new List<Tower>();
        List<Enemy> enemyList = new List<Enemy>();
        string[] towersToLoad;
        string[] enemiesToLoad;
        string[] mapsToLoad;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
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
            IsMouseVisible = true;
            towersToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Towers", "*.xml");
            enemiesToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Enemies", "*.xml");
            mapsToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Maps", "*.xml");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mapList = LoadData.LoadMaps(mapsToLoad);
            towerList = LoadData.Load<Tower>(towersToLoad);
            enemyList = LoadData.Load<Enemy>(enemiesToLoad);
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            if(Menu.location == Menu.Location.MainMenu)
            {

            }
            else if(Menu.location == Menu.Location.Options)
            {

            }
            else if(Menu.location == Menu.Location.MapSelector)
            {

            }
            else if(Menu.location == Menu.Location.TowerSelector)
            {

            }
            // TODO: Add your update logic here

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
            mapList[0].Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
