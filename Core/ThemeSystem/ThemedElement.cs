using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks the gameObject as a specific element for theme system</summary>
	internal sealed class ThemedElement : MonoBehaviour
	{
		public string styleName;
		[SerializeField] internal bool applySprite = true;
		[SerializeField] internal bool applyColor = true;
		[SerializeField] internal bool applyHeight = true;
		[SerializeField] internal bool applyFont = true;
		[SerializeField] internal bool applyFontSize = true;
		[SerializeField] internal bool applyFontStyle = true;

		private Image _image;
		private TMP_Text _text;
		private LayoutElement _layoutElement;

		private void OnValidate() {
			_image = GetComponent<Image>();
			_text = GetComponent<TMP_Text>();
			_layoutElement = GetComponent<LayoutElement>();
		}

		internal void UpdateTheme(Style style) {
			if ( _layoutElement != null ) {
#if UNITY_EDITOR
				EditorUtility.SetDirty( _layoutElement );
#endif
				if ( applyHeight && style.TryGetHeight( out var height ) ) _layoutElement.preferredHeight = height;
			}

			if ( _image != null ) {
#if UNITY_EDITOR
				EditorUtility.SetDirty( _image );
#endif
				if ( applySprite && style.TryGetSprite( out var sprite ) ) _image.sprite = sprite;
				if ( applyColor && style.TryGetColor( out var color ) ) _image.color = color;
			}

			if ( _text != null ) {
#if UNITY_EDITOR
				EditorUtility.SetDirty( _text );
#endif
				if ( applyFont && style.TryGetFont( out var font ) ) _text.font = font;
				if ( applyColor && style.TryGetColor( out var color ) ) _text.color = color;
				if ( applyFontSize && style.TryGetFontSize( out var fontSize ) ) _text.fontSize = fontSize;
				if ( applyFontStyle && style.TryGetFontStyle( out var fontStyle ) ) _text.fontStyle = fontStyle;
			}
		}
	}
}