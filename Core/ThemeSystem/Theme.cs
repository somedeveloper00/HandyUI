using System;
using UnityEngine;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks every <see cref="ThemedElement"/> child with it's own theme</summary>
	[ExecuteAlways]
	public sealed class Theme : MonoBehaviour
	{
		public StylePack stylePack;
		private ThemedElement[] _elements = Array.Empty<ThemedElement>();

		private void OnEnable() => UpdateTheme();

		private void OnValidate() => UpdateTheme();

		/// <summary>Updates the theme of all child elements</summary>
		public void UpdateTheme(bool refreshElements = true) {
			if (refreshElements)
				_elements = GetComponentsInChildren<ThemedElement>();
			foreach (var element in _elements) {
				UpdateStyle( element );
			}

			void UpdateStyle(ThemedElement element) {
				if ( stylePack == null || stylePack.styles == null ) return;
				for (int i = 0; i < stylePack.styles.Length; i++) {
					if ( stylePack.styles[i].name != element.styleName ) continue;
					element.UpdateTheme( stylePack.styles[i] );
					return;
				}
			}
		}
	}
}