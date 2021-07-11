using System;
using System.Linq;
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

            // I hate this a lot again
            var str = instruction.SwapBytes().ToString("X4");
            
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

        [Instruction("6xkk")]
        public void LoadRegister(ushort instruction)
        {
            throw new NotImplementedException();
        }
    }
}