using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using zadatak3;

namespace GamePong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 900,
                PreferredBackBufferWidth = 500
            };
            Content.RootDirectory = " Content ";
        }

        /// <summary >
        /// Bottom paddle object
        /// </ summary >
        public Paddle PaddleBottom { get; private set; }
        /// <summary >
        /// Top paddle object
        /// </ summary >
        public Paddle PaddleTop { get; private set; }
        /// <summary >
        /// Ball object
        /// </ summary >
        public Ball Ball { get; private set; }
        /// <summary >
        /// Background image
        /// </ summary >
        public Background Background { get; private set; }
        /// <summary >
        /// Sound when ball hits an obstacle .
        /// SoundEffect is a type defined in Monogame framework
        /// </ summary >
        public SoundEffect HitSound { get; private set; }
        /// <summary >
        /// Background music . Song is a type defined in Monogame framework
        /// </ summary >
        public Song Music { get; private set; }
        /// <summary >
        /// Generic list that holds Sprites that should be drawn on screen
        /// </ summary >
        private GenericList<Sprite> SpritesForDrawList = new GenericList<Sprite>();

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Screen bounds details . Use this information to set up game objects positions.
            var screenBounds = GraphicsDevice.Viewport.Bounds;
            PaddleBottom = new Paddle(GameConstants.PaddleDefaultWidth,
            GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleBottom.X = 130;
            PaddleBottom.Y = 830;
            PaddleTop = new Paddle(GameConstants.PaddleDefaultWidth,
            GameConstants.PaddleDefaulHeight, GameConstants.PaddleDefaulSpeed);
            PaddleTop.X = 170;
            PaddleTop.Y = 30;
            Ball = new Ball(GameConstants.DefaultBallSize,
            GameConstants.DefaultInitialBallSpeed,
            GameConstants.DefaultBallBumpSpeedIncreaseFactor)
            {
                X = 200,
            Y = 400
            };
            Background = new Background( 500 , 900);
            // Add our game objects to the sprites that should be drawn collection .. you ’ll see why in a second
            SpritesForDrawList.Add(Background);
            SpritesForDrawList.Add(PaddleBottom);
            SpritesForDrawList.Add(PaddleTop);
            SpritesForDrawList.Add(Ball);
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

            // TODO: use this.Content to load your game content here
            {
                // Initialize new SpriteBatch object which will be used to draw textures .
                spriteBatch = new SpriteBatch(GraphicsDevice);
                // Set textures
                Texture2D paddleTexture = Content.Load<Texture2D>("paddle.jpg");
                PaddleBottom.Texture = paddleTexture;
                PaddleTop.Texture = paddleTexture;
                Ball.Texture = Content.Load<Texture2D>(" ball ");
                Background.Texture = Content.Load<Texture2D>(" background ");
                // Load sounds
                // Start background music
                HitSound = Content.Load<SoundEffect>(" hit");
                Music = Content.Load<Song>(" music ");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(Music);
            }
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Start drawing .
            spriteBatch.Begin();
            for (int i = 0; i < SpritesForDrawList.Count; i++)
            {
                SpritesForDrawList.GetElement(i).DrawSpriteOnScreen(spriteBatch);
            }
            // End drawing .
            // Send all gathered details to the graphic card in one batch .
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public abstract class Sprite
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary >
        /// Represents the texture of the Sprite on the screen .
        /// Texture2D is a type defined in Monogame framework .
        /// </ summary >
        public Texture2D Texture { get; set; }
        protected Sprite(int width, int height, float x = 0, float y = 0)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
        /// <summary >
        /// Base draw method
        /// </ summary >
        public virtual void DrawSpriteOnScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0,
            Width, Height), Color.White);
        }
    }

    /// <summary >
    /// Game background representation
    /// </ summary >
    public class Background : Sprite
    {
        public Background(int width, int height) : base(width, height)
        {

        }
    }

    /// <summary >
    /// Game ball object representation
    /// </ summary >
    public class Ball : Sprite
    {
        /// <summary >
        /// Defines current ball speed in time .
        /// </ summary >
        public float Speed { get; set; }
        public float BumpSpeedIncreaseFactor { get; set; }
        /// <summary >
        /// Defines ball direction .
        /// Valid values ( -1 , -1) , (1 ,1) , (1 , -1) , ( -1 ,1).
        /// Using Vector2 to simplify game calculation . Potentially
        /// dangerous because vector 2 can swallow other values as well .
        /// OPTIONAL TODO : create your own , more suitable type
        /// </ summary >
        public Vector2 Direction { get; set; }
        public Ball(int size, float speed, float
        defaultBallBumpSpeedIncreaseFactor) : base(size, size)
        {
            Speed = speed;
            BumpSpeedIncreaseFactor = defaultBallBumpSpeedIncreaseFactor;
            // Initial direction
            Direction = new Vector2(1, 1);
        }


        /// <summary >
        /// Represents player paddle .
        /// </ summary >


    }

    /// <summary >
    /// Represents player paddle .
    /// </ summary >
    public class Paddle : Sprite
    {
        /// <summary >
        /// Current paddle speed in time
        /// </ summary >
        public float Speed { get; set; }
        public Paddle(int width, int height, float initialSpeed) : base(width,
        height)
        {
            Speed = initialSpeed;
        }
        /// <summary >
        /// Overriding draw method . Masking paddle texture with black color .
        /// </ summary
        public override void DrawSpriteOnScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0,
            Width, Height), Color.GhostWhite);
        }
    }

    public class Vector2new
    {
        private int _x;
        private int _y;

        public Vector2new(int _x, int _y)
        {
            this._x = _x;
            this._y = _y;
        }

        public int getX()
        {
            return _x;
        }

        public int getY()
        {
            return _y;
        }
    
        public void setX(int x)
        {
            this._x = checkCorrect(x);
        }

        public void setY(int y)
        {
            this._y = checkCorrect(y);
        }

        public static int checkCorrect(int z)
        {
            if(z<0)
            { return -1; }
            if(z>0)
            { return 1; }
            throw new ArgumentException();
        }

        public static Vector2 operator *(Vector2new vec, Vector2 v)
        {
            return new Vector2(vec._x * v.X, vec._y * v.Y);
        }
        public static Vector2 operator *(Vector2new vec, float v)
        {
            return new Vector2(vec._x * v, vec._y * v);
        }
    }

    public class GameConstants
    {
        public const float PaddleDefaulSpeed = 0.9f ;
        public const int PaddleDefaultWidth = 200;
        public const int PaddleDefaulHeight = 20;
        public const float DefaultInitialBallSpeed = 0.4f ;
        public const float DefaultBallBumpSpeedIncreaseFactor = 1.05f ;
        public const int DefaultBallSize = 40;
    }


}