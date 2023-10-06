// Вставьте сюда финальное содержимое файла BrainfuckLoopCommands.cs
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        static readonly Dictionary<int, int> StartLoop = new Dictionary<int, int>();
        static readonly Dictionary<int, int> EndLoop = new Dictionary<int, int>();
        static readonly Stack<int> Stack = new Stack<int>();

        static void StartEndPositionsLoop(IVirtualMachine vm)
        {
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                switch (vm.Instructions[i])
                {
                    case '[':
                        Stack.Push(i);
                        break;
                    case ']':
                        EndLoop[i] = Stack.Peek();
                        StartLoop[Stack.Pop()] = i;
                        break;
                }
            }
		}

        public static void RegisterTo(IVirtualMachine vm)
        {
            StartEndPositionsLoop(vm);

            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = StartLoop[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = EndLoop[b.InstructionPointer];
            });
        }
    }
}
