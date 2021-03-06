using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Chip8;
using System.IO;
using System.Collections.Generic;

namespace Pot8to
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        private RenderTarget2D _nativeRenderTarget;

        private string[] arguments;

        private int cyclesPerSecond = 600;

        private double secondsSinceLastUpdate = 0;

        private IInput inputManager;

        private Cpu cpu;

        public Game1(string[] args)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            arguments = args;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, 64, 32);

            _graphics.PreferredBackBufferWidth = 640;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 320;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            // Construct chip8 and load bin file.
            cpu = new Cpu();

            if(arguments.Length >= 2)
            {
                if(arguments[1] == "t")
                {
                    inputManager = new InputTyping();
                }
                else
                {
                    inputManager = new InputGame();
                }
            }
            else
            {
                inputManager = new InputGame();
            }

            loadBin(arguments[0]);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            secondsSinceLastUpdate += 1;
            if(secondsSinceLastUpdate > (1/cyclesPerSecond))
            {
                secondsSinceLastUpdate = 0;

                for(int i = 0; i < 10; i++)
                {
                    cpu.cycle(inputManager.input());
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            _spriteBatch.Begin();
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 64, 32);

            Color[] data = new Color[64*32];
            for(int i=0; i < data.Length; ++i)
            {
                if(cpu.display[i])
                {
                    data[i] = Color.HotPink;
                }
                else
                {
                    data[i] = Color.MidnightBlue;
                }
            }
            rect.SetData(data);
            Vector2 coor = new Vector2(0, 0);
            _spriteBatch.Draw(rect, coor, Color.White);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            Point p = new Point(0,0);
            Point size = new Point(640, 320);
            Rectangle scaledRect = new Rectangle(p, size);
            _spriteBatch.Draw(_nativeRenderTarget, scaledRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Finds a file and loads it into the Chip-8 emulator.
        /// </summary>
        /// <param name="fileName">The path to the file to load.</param>
        private void loadBin(string fileName)
        {
            var byteList = new List<byte>();
            if(File.Exists(fileName))
            {
                using(BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    while(reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        byteList.Add(reader.ReadByte());
                    }
                }
                cpu.loadBin(byteList);
            }
        }
    }
}
