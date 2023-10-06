// Вставьте сюда финальное содержимое файла VirtualMachine.cs
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

        Dictionary<char, Action<IVirtualMachine>> operations = new Dictionary<char, Action<IVirtualMachine>>();

        public VirtualMachine(string program, int memorySize)
		{
			Memory = new byte[memorySize];
			Instructions = program;
			MemoryPointer = 0;
			InstructionPointer = 0;
        }

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			operations.Add(symbol, execute);
		}

		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				var command = Instructions[InstructionPointer];
				if (operations.ContainsKey(command))
					operations[command](this);
			}
		}
	}
}
