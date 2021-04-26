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

        private int cyclesPerSecond = 600;

        private double secondsSinceLastUpdate = 0;

        private Cpu cpu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            //loadBin("Pot8to/bin/Debug/netcoreapp3.1/roms/Coin Flipping [Carmelo Cortez, 1978].ch8");
            loadBin("Bins/BC_test.ch8");

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

                cpu.cycle();
                cpu.cycle();

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
                    data[i] = Color.SkyBlue;
                }
                else
                {
                    data[i] = Color.Black;
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

        void loadBin(string fileName)
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
            }
            cpu.loadBin(byteList);
        }
    }
}
