using System;
using System.Collections.Generic;
using ManyConsole;
using edu.stanford.nlp.process;
using java.util;

namespace Abbreviatr
{
	public class GeneratePasswordsCommand : ConsoleCommand
	{
		private string _inputFile;
		private int? _numberOfPasswords;
		private int? _minPasswordLength = 6;
		private int? _maxPasswordLength;
		private bool _verbose;
		private int _nthCharacter = 1;

		public GeneratePasswordsCommand()
		{
			IsCommand("GeneratePasswords", "Generate passwords by abbreviating the sentences in a plain text file.");
			HasRequiredOption<string>("i|inputFile=", "The plain text input file", o => _inputFile = o);
			HasOption<int>("n|number=", "Number of passwords; default is do as many as possible", o => _numberOfPasswords = o);
			HasOption<int>("mi|minLength=", "Minimum required password length; default is 6", o => _minPasswordLength = o);
			HasOption<int>("ma|maxLength=", "Maximum password length", o => _maxPasswordLength = o);
			HasOption<int>("c|char=", "Take nth character from each word (or the last character if n > length); default is 1", o => _nthCharacter = o);
			HasOption("v|verbose", "Verbose output", o => _verbose = true);
		}

		public override int Run(string[] remainingArguments)
		{
			var passwords = GeneratePasswords(_inputFile);

			foreach (var password in passwords)
			{
				if (_verbose)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(password.TokenizedSentence);
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.Write(string.Format("\t => {0}", password.Password));

					if (!string.IsNullOrEmpty(password.GenerationNarrative))
					{
						Console.ForegroundColor = ConsoleColor.Magenta;
						Console.Write(string.Format(" ({0})", password.GenerationNarrative));
					}

					Console.WriteLine();
					Console.ResetColor();
					continue;
				}
				
				Console.WriteLine(password);
			}

			return 0;
		}

		private IEnumerable<GeneratedPassword> GeneratePasswords(string inputFile)
		{
			var sentences = new DocumentPreprocessor(inputFile, DocumentPreprocessor.DocType.Plain).iterator();

			var i = 0;
			while (sentences.hasNext() && (!_numberOfPasswords.HasValue || i < _numberOfPasswords.Value))
			{
				var password = new GeneratedPassword((ArrayList)sentences.next(), _minPasswordLength, _maxPasswordLength, new TakeNthOrLastLetter(_nthCharacter));

				if (password.Valid)
					i++;

				yield return password;
			}
		}
	}
}