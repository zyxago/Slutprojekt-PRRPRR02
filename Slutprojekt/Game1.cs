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
        SpriteFont font;
        KeyboardState KeyState, prevKeyState;
        public static MouseState mouseState, prevMouseState;
        List<Map> mapList = new List<Map>();
        List<Tower> towerList = new List<Tower>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Tower> towers = new List<Tower>();
        List<Enemy> enemies = new List<Enemy>();
        public static Texture2D ErrorTex;
        Texture2D hotbarTex;
        string[] towersToLoad;
        string[] enemiesToLoad;
        string[] mapsToLoad;
        public static int MapPlaying { get; private set; }
        bool waveOngoing = false;
        Rectangle nextWave = new Rectangle()//TODO bestäm plats på den

        public enum GameState
        {
            Menu,
            InGame,
            Quit
        }

        public static GameState State;
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
            State = GameState.Menu;
            Menu.location = Menu.Location.MainMenu;
            Spell.LoadSpells();
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            ErrorTex = Content.Load<Texture2D>("ErrorTexture");
            hotbarTex = Content.Load<Texture2D>("TowerBar");
            Menu.Load();
            mapList = LoadData.Load(mapsToLoad);
            towerList = LoadData.Load<Tower>(towersToLoad);
            enemyList = LoadData.Load<Enemy>(enemiesToLoad);
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
            KeyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (State == GameState.Menu)
            {
                Menu.Update();
            }
            else if (State == GameState.InGame)
            {
                if(mouseState.LeftButton != prevMouseState.LeftButton && )//TODO fixa rectangle
                if(waveOngoing == true)
                {
                    EntitySpawner.SpawnEnemies();
                }
            }
            else if (State == GameState.Quit)
            {
                Exit();
            }
            prevKeyState = Keyboard.GetState();
            prevMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        public static void StartGame(int mapIndex)
        {
            MapPlaying = mapIndex-1;
            State = GameState.InGame;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            if (State == GameState.Menu)
            {
                Menu.Draw(spriteBatch, mapList);
            }
            else if (State == GameState.InGame)
            {
                mapList[MapPlaying].Draw(spriteBatch);
                spriteBatch.Draw(hotbarTex, new Rectangle(0, 560, 800, 80), Color.White);
                for(int i = 0; i < towerList.Count && i <= 8; i++)
                {
                    spriteBatch.Draw(towerList[i].Texture, new Rectangle((10 + 100 * i), 570, 80, 60), Color.White);//FLYTTA TILL HUD KLASSEN
                }
            }
            spriteBatch.DrawString(font, $"{mouseState.Position}", new Vector2(50, 50), Color.Black);//Ta bort sen!
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
