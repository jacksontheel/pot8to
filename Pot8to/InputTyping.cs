using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Pot8to
{
    /// <summary>
    /// An implementation of IInput that only adds an input to the IEnumerable to be returned once per
    /// input key release.
    /// </summary>
    public class InputTyping : IInput
    {
        public List<byte> oldInput { get; private set; }

        /// <summary>
        /// Creates a new list for the oldInput- that is, the input from the last frame.
        /// </summary>
        public InputTyping()
        {
            oldInput = new List<byte>();
        }

        /// <summary>
        /// Sends an input to the Chip-8 only when the key is released. This is useful for programs
        /// on the Chip-8 that utilize typing, where a single keystroke is expected to only yield a
        /// result once.
        /// </summary>
        /// <returns>A list of bytes representing the current input for the Chip-8 to process</returns>
        public IEnumerable<byte> input()
        {
            List<byte> input = new List<byte>();
            List<byte> passInput = new List<byte>();

            /* 0 */
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                input.Add(0);
            }
            else if(oldInput.Contains(0) && Keyboard.GetState().IsKeyUp(Keys.X))
            {
                passInput.Add(0);
            }

            /* 1 */
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                input.Add(1);
            }
            else if(oldInput.Contains(1) && Keyboard.GetState().IsKeyUp(Keys.D1))
            {
                passInput.Add(1);
            }

            /* 2 */
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                input.Add(2);
            }
            else if(oldInput.Contains(2) && Keyboard.GetState().IsKeyUp(Keys.D2))
            {
                passInput.Add(2);
            }

            /* 3 */
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                input.Add(3);
            }
            else if(oldInput.Contains(3) && Keyboard.GetState().IsKeyUp(Keys.D3))
            {
                passInput.Add(3);
            }

            /* 4 */
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                input.Add(4);
            }
            else if(oldInput.Contains(4) && Keyboard.GetState().IsKeyUp(Keys.Q))
            {
                passInput.Add(4);
            }

            /* 5 */
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                input.Add(5);
            }
            else if(oldInput.Contains(5) && Keyboard.GetState().IsKeyUp(Keys.W))
            {
                passInput.Add(5);
            }

            /* 6 */
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                input.Add(6);
            }
            else if(oldInput.Contains(6) && Keyboard.GetState().IsKeyUp(Keys.E))
            {
                passInput.Add(6);
            }

            /* 7 */
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                input.Add(7);
            }
            else if(oldInput.Contains(7) && Keyboard.GetState().IsKeyUp(Keys.A))
            {
                passInput.Add(7);
            }

            /* 8 */
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                input.Add(8);
            }
            else if(oldInput.Contains(8) && Keyboard.GetState().IsKeyUp(Keys.S))
            {
                passInput.Add(8);
            }

            /* 9 */
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                input.Add(9);
            }
            else if(oldInput.Contains(9) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                passInput.Add(9);
            }

            /* A */
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                input.Add(0xA);
            }
            else if(oldInput.Contains(0xA) && Keyboard.GetState().IsKeyUp(Keys.Z))
            {
                passInput.Add(0xA);
            }

            /* B */
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                input.Add(0xB);
            }
            else if(oldInput.Contains(0xB) && Keyboard.GetState().IsKeyUp(Keys.B))
            {
                passInput.Add(0xB);
            }

            /* C */
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                input.Add(0xC);
            }
            else if(oldInput.Contains(0xC) && Keyboard.GetState().IsKeyUp(Keys.D4))
            {
                passInput.Add(0xC);
            }

            /* D */
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                input.Add(0xD);
            }
            else if(oldInput.Contains(0xD) && Keyboard.GetState().IsKeyUp(Keys.R))
            {
                passInput.Add(0xD);
            }

            /* E */
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                input.Add(0xE);
            }
            else if(oldInput.Contains(0xE) && Keyboard.GetState().IsKeyUp(Keys.F))
            {
                passInput.Add(0xE);
            }

            /* F */
            if (Keyboard.GetState().IsKeyDown(Keys.V))
            {
                input.Add(0xF);
            }
            else if(oldInput.Contains(0xF) && Keyboard.GetState().IsKeyUp(Keys.V))
            {
                passInput.Add(0xF);
            }

            oldInput = input;
            return passInput;
        }
    }
}