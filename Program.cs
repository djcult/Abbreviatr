﻿using System;
using System.Collections.Generic;
using ManyConsole;

namespace Abbreviatr
{
	class Program
	{
		static int Main(string[] args)
		{
			return ConsoleCommandDispatcher.DispatchCommand(GetCommands(), args, Console.Out);
		}

		public static IEnumerable<ConsoleCommand> GetCommands()
		{
			return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
		}
	}
}
