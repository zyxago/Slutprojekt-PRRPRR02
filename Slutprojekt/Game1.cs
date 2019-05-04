using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slutprojekt.GameObjects.Towers;
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
        public static SpriteFont font;
        KeyboardState KeyState, prevKeyState;
        public static MouseState mouseState, prevMouseState;
        List<Map> mapList = new List<Map>();
        List<Tower> towerList = new List<Tower>();
        List<Enemy> enemyList = new List<Enemy>();
        List<Tower> Towers = new List<Tower>();
        List<Enemy> enemies = new List<Enemy>();
        public static Texture2D ErrorTex;
        string[] towersToLoad;
        string[] enemiesToLoad;
        string[] mapsToLoad;
        public static int MapPlaying { get; private set; }
        bool waveOngoing = false;
        bool spawning = false;

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
            try
            {
                towersToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Towers", "*.xml");
                enemiesToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Enemies", "*.xml");
                mapsToLoad = Directory.GetFiles($"{Environment.CurrentDirectory}\\Maps", "*.xml");
            }
            catch(DirectoryNotFoundException e)
            {
                throw e;
            }
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
            Menu.Load();
            mapList = LoadData.Load(mapsToLoad);
            towerList = LoadData.Load<Tower>(towersToLoad);
            enemyList = LoadData.Load<Enemy>(enemiesToLoad);
            Hud.Load(towerList);
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
        /// 
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public bool CheckCollision(GameObject object1, GameObject object2)
        {
            float CollisonLength = object1.Radius + object2.Radius;
            if(Vector2.Distance(object1.Drawbox.Center.ToVector2(), object2.Drawbox.Center.ToVector2()) <= CollisonLength)
            {
                return true;
            }
            return false;
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
                bool Collision = false;
                if (Hud.TowerSelected != null)
                {
                    foreach(Tower PlacedTower in Towers)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed && CheckCollision(PlacedTower, Hud.TowerSelected))
                            Collision = true;
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed && Collision == false)
                    {
                        Hud.Money -= Hud.TowerSelected.Cost;
                        if (Hud.TowerSelected is SpellTower)
                        {
                            Towers.Add(new SpellTower(new Rectangle(Hud.TowerSelected.Drawbox.X, Hud.TowerSelected.Drawbox.Y, Hud.TowerSelected.Drawbox.Width, Hud.TowerSelected.Drawbox.Height), Hud.TowerSelected.Texture, Hud.TowerSelected.Radius, Hud.TowerSelected.Cost, (Hud.TowerSelected as SpellTower).Spell, (Hud.TowerSelected as SpellTower).SpellCooldown, (Hud.TowerSelected as SpellTower).SpellRadius));
                        }
                        else if (Hud.TowerSelected is RoadTower)
                        {
                            Towers.Add(new RoadTower(new Rectangle(Hud.TowerSelected.Drawbox.X, Hud.TowerSelected.Drawbox.Y, Hud.TowerSelected.Drawbox.Width, Hud.TowerSelected.Drawbox.Height), Hud.TowerSelected.Texture, Hud.TowerSelected.Radius, Hud.TowerSelected.Cost, (Hud.TowerSelected as RoadTower).ProjectileTexture, (Hud.TowerSelected as RoadTower).AttackRange, (Hud.TowerSelected as RoadTower).AttackSpeed, (Hud.TowerSelected as RoadTower).AttackDMG, (Hud.TowerSelected as RoadTower).Ptype.ToString(), (Hud.TowerSelected as RoadTower).Hp));
                        }
                        else if (Hud.TowerSelected is ClassicTower)
                        {
                            Towers.Add(new ClassicTower(new Rectangle(Hud.TowerSelected.Drawbox.X, Hud.TowerSelected.Drawbox.Y, Hud.TowerSelected.Drawbox.Width, Hud.TowerSelected.Drawbox.Height), Hud.TowerSelected.Texture, Hud.TowerSelected.Radius, Hud.TowerSelected.Cost, (Hud.TowerSelected as ClassicTower).ProjectileTexture, (Hud.TowerSelected as ClassicTower).AttackRange, (Hud.TowerSelected as ClassicTower).AttackSpeed, (Hud.TowerSelected as ClassicTower).AttackDMG, (Hud.TowerSelected as ClassicTower).Ptype.ToString()));
                        }
                        Hud.TowerSelected = null;
                    }
                }
                Hud.Update();
                if(mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed && Hud.NextWaveBox.Contains(mouseState.Position) && waveOngoing == false)
                {
                    waveOngoing = true;
                    spawning = true;
                    EntitySpawner.NextWave(enemyList, mapList[MapPlaying].PathQueue);
                }
                for(int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Update(gameTime);
                    if (enemies[i].IsDead && enemies[i].ExploTime <= gameTime.TotalGameTime)
                    {
                        enemies.RemoveAt(i);
                        i--;
                    }
                }
                if (waveOngoing)
                {
                    if (spawning == true)
                    {
                        if (!EntitySpawner.EnemiesToSpawn.IsEmpty())
                            EntitySpawner.SpawnNextEnemy(enemies);
                        else
                            spawning = false;
                    }
                    if (enemies.Count == 0 && spawning == false)
                    {
                        waveOngoing = false;
                    }
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
                foreach(Tower tower in Towers)
                {
                    tower.Draw(spriteBatch);
                }
                foreach(Enemy enemy in enemies)
                {
                    enemy.Draw(spriteBatch);
                }
                Hud.Draw(spriteBatch);
            }
            spriteBatch.DrawString(font, $"{mouseState.Position}", new Vector2(50, 50), Color.Black);//Ta bort sen!
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
