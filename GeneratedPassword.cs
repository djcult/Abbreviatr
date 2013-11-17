using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using edu.stanford.nlp.ling;
using java.util;

namespace Abbreviatr
{
	public class GeneratedPassword
	{
		public ArrayList TokenizedSentence { get; private set; }
		public int? MinPasswordLength { get; set; }
		public int? MaxPasswordLength { get; set; }
		public string Password { get; private set; }
		public string GenerationNarrative { get; private set; }
		private readonly IWordAbbreviationStrategy _wordAbbreviationStrategy;

		public GeneratedPassword(ArrayList tokenizedSentence, int? minPasswordLength, int? maxPasswordLength, IWordAbbreviationStrategy wordAbbreviationStrategy)
		{
			_wordAbbreviationStrategy = wordAbbreviationStrategy;
			TokenizedSentence = tokenizedSentence;
			MinPasswordLength = minPasswordLength;
			MaxPasswordLength = maxPasswordLength;
			
			Generate();
			EnforceMinPasswordLength();
			EnforceMaxPasswordLength();
		}

		private void Generate()
		{
			var generatedPassword = new StringBuilder();
			var startsWithAWordCharacter = new Regex(@"^\w");

			foreach (var token in TokenizedSentence.Cast<CoreLabel>().Select(c => c.toString()).Where(t => startsWithAWordCharacter.IsMatch(t)))
				generatedPassword.Append(_wordAbbreviationStrategy.Abbreviate(token));

			Password = generatedPassword.ToString();
		}

		private void EnforceMinPasswordLength()
		{
			if (!MinPasswordLength.HasValue || Password.Length >= MinPasswordLength) return;

			Password = null;
			GenerationNarrative = string.Format("Sentence not long enough for a {0} character password", MinPasswordLength);
		}

		private void EnforceMaxPasswordLength()
		{
			if (Password == null) return;
			if (!MaxPasswordLength.HasValue || Password.Length <= MaxPasswordLength) return;

			Password = Password.Substring(0, MaxPasswordLength.Value);
			GenerationNarrative = string.Format("Password truncated to {0} characters", MaxPasswordLength);
		}

		public bool Valid
		{
			get { return Password != null; }
		}

		public override string ToString()
		{
			return Password;
		}
	}
}