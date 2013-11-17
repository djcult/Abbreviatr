namespace Abbreviatr
{
	class TakeNthOrLastLetter : IWordAbbreviationStrategy
	{
		private int _n;

		public TakeNthOrLastLetter(int n)
		{
			_n = n;
		}

		public string Abbreviate(string token)
		{
			return token.Substring(_n > token.Length ? token.Length - 1 : _n - 1, 1);
		}
	}
}