using System;
using System.Collections.Generic;

namespace Chip8
{
    public class Cpu
    {
        private byte[] memory;
        public bool[] display { get; private set; }
        private ushort pc;
        private ushort indexRegister;
        private Stack<ushort> stack;
        private byte delayTimer;
        private byte soundTimer;
        private byte[] registers;

        public Cpu()
        {
            memory = new byte[4096];
            display = new bool[64 * 32];
            stack = new Stack<ushort>();
            registers = new byte[16];

            loadFonts();
            pc = 512;
        }

        private void loadFonts()
        {
            var fontBytes = new byte[]
            {
                0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
                0x20, 0x60, 0x20, 0x20, 0x70, // 1
                0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
                0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
                0x90, 0x90, 0xF0, 0x10, 0x10, // 4
                0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
                0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
                0xF0, 0x10, 0x20, 0x40, 0x40, // 7
                0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
                0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
                0xF0, 0x90, 0xF0, 0x90, 0x90, // A
                0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
                0xF0, 0x80, 0x80, 0x80, 0xF0, // C
                0xE0, 0x90, 0x90, 0x90, 0xE0, // D
                0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
                0xF0, 0x80, 0xF0, 0x80, 0x80  // F
            };

            for(int i = 0; i < fontBytes.Length; i++)
            {
                memory[0x050 + i] = fontBytes[i];
            }
        }

        public void cycle()
        {
            var nextOp = BitConverter.ToUInt16(new byte[] { memory[pc + 1], memory[pc] });
            delayTimer--;
            soundTimer--;
            pc += 2;

            switch((nextOp & 0xF000) >> 12)
            {
                case 0x0:
                    op_0x00E0();
                    break;
                case 0x1:
                    op_0x1NNN(nextOp);
                    break;
                case 0x3:
                    op_0x3XNN(nextOp);
                    break;
                case 0x4:
                    op_0x4XNN(nextOp);
                    break;
                case 0x5:
                    op_0x5XY0(nextOp);
                    break;
                case 0x6:
                    op_0x6XNN(nextOp);
                    break;
                case 0x7:
                    op_0x7XNN(nextOp);
                    break;
                case 0x9:
                    op_0x9XY0(nextOp);
                    break;
                case 0xA:
                    op_0xANNN(nextOp);
                    break;
                case 0xD:
                    op_0xDXYN(nextOp);
                    break;
                case 0xF:
                    switchOp_F(nextOp);
                    break;
            }
        }

        private void switchOp_0()
        {
            
        }

        private void switchOp_F(ushort nextOp)
        {
            switch(nextOp & 0x00FF)
            {
                case 0x29:
                    op_0xFX29(nextOp);
                    break;
            }
        }

        public void loadBin(IEnumerable<byte> bin)
        {
            int i = 512;
            foreach(byte b in bin)
            {
                memory[i] = b;
                i++;
            }
        }

        private void op_0x00E0()
        {
            for(int i = 0; i < 64*32; i++)
            {
                display[i] = false;
            }
        }
        
        private void op_0x00EE()
        {
            pc = stack.Pop();
        }

        private void op_0x1NNN(ushort opCode)
        {
            opCode = (ushort) (opCode & 0x0FFF);
            pc = (ushort) opCode;
        }

        private void op_0x2NNN(ushort opCode)
        {
            stack.Push(pc);
            op_0x1NNN(opCode);
        }

        

        private void op_0x3XNN(ushort opCode)
        {
            if(compareRegisterAndValue(opCode))
            {
                pc += 2;
            }
        }

        private void op_0x4XNN(ushort opCode)
        {
            if(!compareRegisterAndValue(opCode))
            {
                pc += 2;
            }
        }

        /// <summary>
        /// Used by op_0x3XNN and op_0x4XNN
        /// </summary>
        /// <param name="opCode"></param>
        /// <returns></returns>
        private bool compareRegisterAndValue(ushort opCode)
        {
            var registerToCheck = ((opCode & 0x0F00) >> 8);
            var numberToCheck = (opCode & 0x00FF);

            return (registers[registerToCheck] == numberToCheck);
        }

        private void op_0x5XY0(ushort opCode)
        {
            if(compareRegisterAndRegister(opCode))
            {
                pc += 2;
            }
        }

        private void op_0x9XY0(ushort opCode)
        {
            if(!compareRegisterAndRegister(opCode))
            {
                pc += 2;
            }
        }

        /// <summary>
        /// Used by op_0x5XY0 and op_0x9XY0
        /// </summary>
        /// <param name="opCode"></param>
        /// <returns></returns>
        private bool compareRegisterAndRegister(ushort opCode)
        {
            var register1 = ((opCode & 0x0F00) >> 8);
            var register2 = ((opCode & 0x00F0) >> 4);

            return (registers[register1] == registers[register2]);
        }

        private void op_0x6XNN(ushort opCode)
        {
            var registerToSet = ((opCode & 0x0F00) >> 8);
            var valueToSet = (opCode & 0x00FF);

            registers[registerToSet] = (byte) valueToSet;
        }

        private void op_0x7XNN(ushort opCode)
        {
            var registerToSet = ((opCode & 0x0F00) >> 8);
            var valueToAdd = (opCode & 0x00FF);

            int valueToSet = registers[registerToSet] + valueToAdd;
            if(valueToAdd < 0xFF)
            {
                registers[registerToSet] = (byte) valueToSet;
            }
            else
            {
                registers[registerToSet] = 0xFF;
            }
        }

        private void op_0x8XY0(ushort opCode)
        {
            var register1 = ((opCode & 0x0F00) >> 8);
            var register2 = ((opCode & 0x00F0) >> 4);

            registers[register1] = registers[register2];
        }

        private void op_0x8XY1(ushort opCode)
        {
            var register1 = ((opCode & 0x0F00) >> 8);
            var register2 = ((opCode & 0x00F0) >> 4);

            registers[register1] = (byte) (registers[register1] | registers[register2]);
        }

        private void op_0x8XY2(ushort opCode)
        {
            var register1 = ((opCode & 0x0F00) >> 8);
            var register2 = ((opCode & 0x00F0) >> 4);

            registers[register1] = (byte) (registers[register1] & registers[register2]);
        }

        private void op_0x8XY3(ushort opCode)
        {
            var register1 = ((opCode & 0x0F00) >> 8);
            var register2 = ((opCode & 0x00F0) >> 4);

            registers[register1] = (byte) (registers[register1] ^ registers[register2]);
        }

        private void op_0x8XY4(ushort opCode)
        {
            var register1 = ((opCode & 0x0F00) >> 8);
            var register2 = ((opCode & 0x00F0) >> 4);

            var registerAddResult = registers[register1] + registers[register2];
            registers[register1] = (byte) (registerAddResult % 256);
        }

        private void op_0xANNN(ushort opCode)
        {
            opCode = (ushort) (opCode & 0x0FFF);
            indexRegister = opCode;
        }

        private void op_0xDXYN(ushort opCode)
        {
            var xCoord = registers[((opCode & 0x0F00) >> 8)] % 64;
            var yCoord = registers[((opCode & 0x00F0) >> 4)] % 32;
            registers[15] = 0;
            var height = (opCode & 0x000F); // Height is anywhere between 0 and 15 pixels.

            for(int i = 0; i < height; i++)
            {
                byte spriteData = memory[indexRegister + i];
                if((yCoord % 64) <= ((yCoord + i) % 32)) // Checks if the vertical edge of the screen has been written to.
                {
                    for(int j = 0; j < 8; j++) //Sprites are always 8 pixels wide
                    {
                        bool spriteBit = (((spriteData & (0x80 >> j)) >> (7 - j)) == 1)? true : false;

                        if((xCoord % 64) <= ((xCoord + j) % 64)) // Checks if the horizontal edge of the screen has been written to.
                        {
                            if(spriteBit)
                            {
                                var affectNum = (xCoord + j) + ((yCoord + i) * 64);
                                display[affectNum] = !display[affectNum]; //flip bool
                                if(!display[affectNum]) //If bool was turned off (collision)
                                {
                                    registers[15] = 1;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
    
                    }
                }
                else
                {
                    break;
                }
            }


        }

        private void op_0xFX29(ushort opCode)
        {
            var registerValue = registers[((opCode & 0x0F00) >> 8)] & 0x00FF; //Only take the last nibble of the register value

            indexRegister = (ushort)  (50 + (registerValue * 5));
        }
    }
}
