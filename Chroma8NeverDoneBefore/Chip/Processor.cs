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

        private Chip8 _context;

        public Processor(Chip8 context)
        {
            _context = context;
        }

        public void Initialize(ushort PcStart = 0x000)
        {
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

                    if(!failed)
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
            throw new NotImplementedException();
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
        public void SkipIfEqual(ushort instruction)
        {
            var r = (byte) ((instruction & 0x0F00) >> 8);
            if (Registers[r] == (byte) (instruction & 0x00FF))
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

        [Instruction("Annn")]
        public void LoadAddress(ushort instruction)
        {
            MemRegister = (ushort) (instruction & 0x0FFF);
        }
    }
}