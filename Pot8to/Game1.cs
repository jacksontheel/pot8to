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

        private int cyclesPerSecond = 60;

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
            _graphics.PreferredBackBufferWidth = 64;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 32;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            // Construct chip8 and load bin file.
            cpu = new Cpu();
            loadBin("Bins/test.ch8");

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
            secondsSinceLastUpdate += gameTime.ElapsedGameTime.TotalSeconds;
            if(secondsSinceLastUpdate > (1/cyclesPerSecond))
            {
                secondsSinceLastUpdate = 0;

                cpu.cycle();

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 64, 32);

            Color[] data = new Color[64*32];
            for(int i=0; i < data.Length; ++i)
            {
                if(cpu.display[i])
                {
                    data[i] = Color.SkyBlue;
                }
            }
            rect.SetData(data);

            Vector2 coor = new Vector2(0, 0);
            _spriteBatch.Draw(rect, coor, Color.White);
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
