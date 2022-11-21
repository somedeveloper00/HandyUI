using System.Text;

namespace UIFlex.Elements.Utils
{
	internal static class StringHelpers
	{
		/// <summary>
		/// Returns the first found float number sequence from the given string
		/// </summary>
		public static float GetFloatFromStr(string value)
		{
			var sb = new StringBuilder();
			var chars = value.ToCharArray();
			for (int i = 0; i < chars.Length; i++)
			{
				if (chars[i] == '.' || (chars[i] > '0' && chars[i] < '9'))
				{
					sb.Append(chars[i]);
					continue;
				}
				if(sb.Length == 0)
					continue;
				break; // we already have some numbers
			}

			if (sb.Length == 0) return 0;
			return float.Parse(sb.ToString());
		}
	}
}