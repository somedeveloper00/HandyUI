using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks the gameObject as a specific element for theme system</summary>
	internal sealed class ThemedElement : MonoBehaviour
	{
		public string styleName;
		private Image image;
		private TMP_Text text;

		private void OnValidate() {
			image = GetComponent<Image>();
			text = GetComponent<TMP_Text>();
		}

		internal void UpdateTheme(Style style) {
			if ( image != null ) {
				if( style.TryGetSprite( out var sprite ) ) image.sprite = sprite;
				if ( style.TryGetColor( out var color ) ) image.color = color;
			}

			if ( text != null ) {
				if ( style.TryGetColor( out var color ) ) text.color = color;
				if ( style.TryGetFontSize( out var fontSize ) ) text.fontSize = fontSize;
				if ( style.TryGetFontStyle( out var fontStyle ) ) text.fontStyle = fontStyle;
			}
		}
	}
}