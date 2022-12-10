using System;
using TMPro;
using UnityEngine;

namespace HandyUI.ThemeSystem
{
	/// <summary>used for Theme System to recognize an element as a specific group</summary>
	[Serializable]
	internal sealed class Style
	{
		public string parentName;
		public string name;
		
		[SerializeField] private OverridableOption<Sprite> _sprite;
		[SerializeField] private OverridableOption<Color> _color;
		[SerializeField] private OverridableOption<TMP_FontAsset> _font;
		[SerializeField] private OverridableOption<float> _fontSize;
		[SerializeField] private OverridableOption<FontStyles> _fontStyle;
		
		[NonSerialized] private Style parent;
		[NonSerialized] internal bool valid = false;

		public bool TryGetFont( out TMP_FontAsset font ) {
			if ( valid ) {
				if ( _font.enabled ) {
					font = _font.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetFont( out font );
			}
			font = null;
			return false;
		}

		public bool TryGetFontSize( out float fontSize ) {
			if ( valid ) {
				if ( _fontSize.enabled ) {
					fontSize = _fontSize.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetFontSize( out fontSize );
			}
			fontSize = 0;
			return false;
		}

		public bool TryGetSprite( out Sprite sprite ) {
			if ( valid ) {
				if ( _sprite.enabled ) {
					sprite = _sprite.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetSprite( out sprite );
			}
			sprite = null;
			return false;
		}

		public bool TryGetColor( out Color color ) {
			if ( valid ) {
				if ( _color.enabled ) {
					color = _color.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetColor( out color );
			}
			color = Color.white;
			return false;
		}

		public bool TryGetFontStyle( out FontStyles fontStyle ) {
			if ( valid ) {
				if ( _fontStyle.enabled ) {
					fontStyle = _fontStyle.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetFontStyle( out fontStyle );
			}
			fontStyle = FontStyles.Normal;
			return false;
		}
		
		public static void ResolveStyles(Style[] styles) {
			if ( styles == null ) return;
			// assign parents blindly & validating root styles
			for (int i = 0; i < styles.Length; i++) {
				// root style
				if ( string.IsNullOrEmpty( styles[i].parentName ) ) {
					styles[i].parent = null;
					styles[i].valid = true;
					continue;
				}
				// nested style
				else {
					for (int j = 0; j < styles.Length; j++) {
						if ( i != j && styles[j].name == styles[i].parentName ) {
							styles[i].parent = styles[j];
							break;
						}
					}
				}
			}
			
			// validating
			int depth = 0;
			for (int i = 0; i < styles.Length; i++) {
				depth = 0;
				styles[i].valid = is_valid( styles[i] );
			}

			bool is_valid(Style style) {
				depth++;
				if ( depth > 32 ) return false; // preventing cross child/parent relations
				if ( style.valid ) return true;
				if ( style.parent != null ) {
					return is_valid( style.parent );
				}
				return false;
			}
		}



		
		[Serializable] public abstract class OverridableOption { } // for easier editor
		[Serializable] public sealed class OverridableOption<T> : OverridableOption
		{
			public bool enabled = false;
			public T value;
		} 
		
		
	}
}