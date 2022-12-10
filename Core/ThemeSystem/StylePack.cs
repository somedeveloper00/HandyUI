using System;
using UnityEngine;

namespace HandyUI.ThemeSystem
{
	[CreateAssetMenu( fileName = "StylePack", menuName = "HandyUI/Style Pack", order = 0 )]
	public class StylePack : ScriptableObject
	{
		[SerializeField] internal Style[] styles = Array.Empty<Style>();
		
		private void OnValidate() {
			styles ??= Array.Empty<Style>();
			foreach (var style in styles) style.name = style.name.ToLower();
			Style.ResolveStyles(styles);
		}

	}
}