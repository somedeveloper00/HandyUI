using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks the gameObject as a specific element for theme system</summary>
	internal sealed class ThemedElement : MonoBehaviour
	{
		public string styleName;
		[SerializeField] internal bool applySprite = true;
		[SerializeField] internal bool applyColor = true;
		[SerializeField] internal bool applyFont = true;
		[SerializeField] internal bool applyFontSize = true;
		[SerializeField] internal bool applyFontStyle = true;
		
		private Image image;
		private TMP_Text text;

		private void OnValidate() {
			image = GetComponent<Image>();
			text = GetComponent<TMP_Text>();
		}

		internal void UpdateTheme(Style style) {
			if ( image != null ) {
				if( applySprite && style.TryGetSprite( out var sprite ) ) image.sprite = sprite;
				if ( applyColor && style.TryGetColor( out var color ) ) image.color = color;
			}

			if ( text != null ) {
				if ( applyFont && style.TryGetFont( out var font ) ) text.font = font;
				if ( applyColor && style.TryGetColor( out var color ) ) text.color = color;
				if ( applyFontSize && style.TryGetFontSize( out var fontSize ) ) text.fontSize = fontSize;
				if ( applyFontStyle && style.TryGetFontStyle( out var fontStyle ) ) text.fontStyle = fontStyle;
			}
		}
	}
}