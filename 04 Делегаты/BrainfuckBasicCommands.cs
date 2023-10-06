// Вставьте сюда финальное содержимое файла BrainfuckBasicCommands.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
        static char[] symbols = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
            vm.RegisterCommand('+', b => { unchecked { b.Memory[b.MemoryPointer]++; } });
            vm.RegisterCommand('-', b => { unchecked { b.Memory[b.MemoryPointer]--; } });
            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte)read());
           
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer == b.Memory.Length - 1)
                    b.MemoryPointer = 0;
                else
                    b.MemoryPointer++;
            });
            
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length - 1;
                else
                    b.MemoryPointer--;
            });           

            foreach (var vr in symbols)
                vm.RegisterCommand(vr, b => b.Memory[b.MemoryPointer] = (byte)vr);
        }
    }
}
