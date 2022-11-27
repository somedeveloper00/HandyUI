using System;
using System.Linq;

namespace UIFlex
{
	internal static class LinqExtensions
	{
		
		public static void ForEach<T>(this T[] array, Action<T> callback)
		{
			for (int i = 0; i < array.Length; i++) 
				callback(array[i]);
		}
	}
}