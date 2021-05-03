using System.Collections.Generic;

namespace Pot8to
{
    /// <summary>
    /// An interface For packaging currently active inputs together to pass to a Chip-8 emulator.
    /// </summary>
    public interface IInput {
        /// <summary>
        /// Gets a list of input to send to a Chip-8 emulator. Bytes should be between 0 and 15, the
        /// supported inputs of an original Chip-8. 
        /// </summary>
        /// <returns>A list of bytes representing the current input for the Chip-8 to process</returns>
        IEnumerable<byte> input();
    }
}