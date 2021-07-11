using System;
using System.Reflection;
using Chroma8NeverDoneBefore.Helpers;

namespace Chroma8NeverDoneBefore.Chip
{
    public class Processor
    {
        public ushort MemRegister;
        public byte[] Registers;

        public ushort ProgramCounter;
        public int StackPointer;
        public ushort[] Stack;

        private Random _rand;
        private Chip8 _context;

        public Processor(Chip8 context)
        {
            _context = context;
        }

        public void Initialize(ushort PcStart = 0x000)
        {
            _rand = new Random();
            Registers = new byte[16];
            Stack = new ushort[16];
            ProgramCounter = PcStart;
            StackPointer = 0;
        }

        public void Clock()
        {
            var instruction = _context.Memory.ReadUShort(ProgramCounter);
            // Make sure we're always working with big endian
            instruction = (BitConverter.IsLittleEndian ? instruction.SwapBytes() : instruction);

            // I hate this a lot again
            var str = instruction.ToString("X4");

            var methods = this.GetType().GetMethods(BindingFlags.Public
                                                    | BindingFlags.NonPublic
                                                    | BindingFlags.Instance
                                                    | BindingFlags.Static);
            foreach (var info in methods)
            {
                if (info.GetCustomAttribute(typeof(InstructionAttribute)) is InstructionAttribute attr)
                {
                    var failed = false;
                    for (var i = 0; i < str.Length; i++)
                    {
                        if (InstructionAttribute.ACCEPTED_CHARS.Contains(attr.MatchStr[i]))
                        {
                            if (attr.MatchStr[i] != str[i])
                            {
                                failed = true;
                                break;
                            }
                        }
                    }

                    if (!failed)
                    {
                        info.Invoke(this, new object[] {instruction});
                        if (attr.IncrementPc)
                            ProgramCounter += 0x002;
                        return;
                    }
                }
            }
        }

        [Instruction("00E0")]
        public void Clear(ushort instruction)
        {
            _context.Renderer.Clear();
        }

        [Instruction("00EE")]
        public void Return(ushort instruction)
        {
            ProgramCounter = Stack[StackPointer];
            StackPointer--;
        }

        [Instruction("1nnn")]
        public void SetProgramCounter(ushort instruction)
        {
            ProgramCounter = (ushort) (instruction & 0x0FFF);
        }

        [Instruction("2nnn")]
        public void CallSubroutine(ushort instruction)
        {
            StackPointer++;
            Stack[StackPointer] = ProgramCounter;
            ProgramCounter = (ushort) (instruction & 0x0FFF);
        }

        [Instruction("3xkk")]
        public void SkipIfEqualConstant(ushort instruction)
        {
            var r = (byte) ((instruction & 0x0F00) >> 8);
            if (Registers[r] == (byte) (instruction & 0x00FF))
                ProgramCounter += 2;
        }

        [Instruction("4xkk")]
        public void SkipIfNotEqualConstant(ushort instruction)
        {
            var r = (byte) ((instruction & 0x0F00) >> 8);
            if (Registers[r] != (byte) (instruction & 0x00FF))
                ProgramCounter += 2;
        }

        [Instruction("5xy0")]
        public void SkipIfEqual(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            if (Registers[x] == Registers[y])
                ProgramCounter += 2;
        }

        [Instruction("6xkk")]
        public void LoadRegister(ushort instruction)
        {
            var r = (byte) ((instruction & 0x0F00) >> 8);
            Registers[r] = (byte) (instruction & 0x00FF);
        }

        [Instruction("7xkk")]
        public void AddRegister(ushort instruction)
        {
            var r = (byte) ((instruction & 0x0F00) >> 8);
            Registers[r] = (byte) (Registers[r] + (byte) (instruction & 0x00FF));
        }

        [Instruction("8xy0")]
        public void SetRegister(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = Registers[y];
        }

        [Instruction("8xy1")]
        public void BitwiseOr(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] | Registers[y]);
        }

        [Instruction("8xy2")]
        public void BitwiseAnd(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] & Registers[y]);
        }

        [Instruction("8xy3")]
        public void BitwiseXor(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] ^ Registers[y]);
        }

        [Instruction("8xy4")]
        public void AddWithCarry(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] + Registers[y]);
            Registers[0xF] = Registers[x] + Registers[y] > 255 ? (byte) 0x1 : (byte) 0x0;
        }

        [Instruction("8xy5")]
        public void SubtractPositive(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] - Registers[y]);
            Registers[0xF] = Registers[x] > Registers[y] ? (byte) 0x1 : (byte) 0x0;
        }

        [Instruction("8xy6")]
        public void Divide(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] / 2);
            Registers[0xF] = Registers[x] >> 7 != 0 ? (byte) 0x1 : (byte) 0x0;
        }

        [Instruction("8xy7")]
        public void SubtractNegative(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[y] - Registers[x]);
            Registers[0xF] = Registers[y] > Registers[x] ? (byte) 0x1 : (byte) 0x0;
        }

        [Instruction("8xyE")]
        public void Multiply(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            Registers[x] = (byte) (Registers[x] * 2);
            Registers[0xF] = Registers[x] >> 7 != 0 ? (byte) 0x1 : (byte) 0x0;
        }

        [Instruction("9xy0")]
        public void SkipIfNotEqual(ushort instruction)
        {
            var x = (byte) ((instruction & 0x0F00) >> 8);
            var y = (byte) ((instruction & 0x00F0) >> 4);
            if (Registers[x] != Registers[y])
                ProgramCounter += 2;
        }

        [Instruction("Annn")]
        public void LoadAddress(ushort instruction)
        {
            MemRegister = (ushort) (instruction & 0x0FFF);
        }

        [Instruction("Bnnn")]
        public void LoadAddAddress(ushort instruction)
        {
            MemRegister = (ushort) ((ushort) (instruction & 0x0FFF) + Registers[0]);
        }

        [Instruction("Cxkk")]
        public void Random(ushort instruction)
        {
            Registers[(instruction & 0x0F00) >> 8] = (byte) (_rand.Next(0, 256) & (instruction & 0x00FF));
        }

        [Instruction("Dxyn")]
        public void DrawSprite(ushort instruction)
        {
            _context.Renderer.DrawSprite(
                (byte) ((instruction & 0x0F00) >> 8),
                (byte) ((instruction & 0x00F0) >> 4),
                (byte) (instruction & 0x000F)
            );
        }
    }
}