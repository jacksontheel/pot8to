using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Pot8to
{
    /// <summary>
    /// An implementation of IInput that continues to send an input as active for as long as its
    /// respective key is pressed.
    /// </summary>
    public class InputGame: IInput
    {
        /// <summary>
        /// Sends an input to the Chip-8 for any key that is currently pressed, for as long as its
        /// pressed. This is useful for Chip-8 games where holding a key ought to send continuous
        /// input.
        /// </summary>
        /// <returns>A list of bytes representing the current input for the Chip-8 to process</returns>
        public IEnumerable<byte> input()
        {
            List<byte> input = new List<byte>();
            
            /* 0 */
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                input.Add(0);
            }

            /* 1 */
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                input.Add(1);
            }

            /* 2 */
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                input.Add(2);
            }

            /* 3 */
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                input.Add(3);
            }

            /* 4 */
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                input.Add(4);
            }

            /* 5 */
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                input.Add(5);
            }

            /* 6 */
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                input.Add(6);
            }

            /* 7 */
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                input.Add(7);
            }

            /* 8 */
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                input.Add(8);
            }

            /* 9 */
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                input.Add(9);
            }

            /* A */
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                input.Add(0xA);
            }

            /* B */
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                input.Add(0xB);
            }

            /* C */
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                input.Add(0xC);
            }

            /* D */
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                input.Add(0xD);
            }

            /* E */
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                input.Add(0xE);
            }

            /* F */
            if (Keyboard.GetState().IsKeyDown(Keys.V))
            {
                input.Add(0xF);
            }

            return input;
        }
    }
}