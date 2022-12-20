using System;
using AnimFlex.Tweening;
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
		[SerializeField] private OverridableOption<Color> _outlineColor;
		[SerializeField] private OverridableOption<float> _height;
		[SerializeField] private OverridableOption<float> _width;
		[SerializeField] private OverridableOption<TMP_FontAsset> _font;
		[SerializeField] private OverridableOption<float> _fontSize;
		[SerializeField] private OverridableOption<FontStyles> _fontStyle;
		[SerializeField] private OverridableOption<bool> _inAnim;
		[SerializeField] private OverridableOption<Ease> _inEase;
		[SerializeField] private OverridableOption<float> _inDuration;
		[SerializeField] private OverridableOption<bool> _outAnim;
		[SerializeField] private OverridableOption<Ease> _outEase;
		[SerializeField] private OverridableOption<float> _outDuration;
		
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
		
		public bool TryGetInEase( out Ease ease ) {
			if ( valid ) {
				if ( _inEase.enabled ) {
					ease = _inEase.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetInEase( out ease );
			}
			ease = Ease.Linear;
			return false;
		}
		public bool TryGetOutEase( out Ease ease ) {
			if ( valid ) {
				if ( _outEase.enabled ) {
					ease = _outEase.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetOutEase( out ease );
			}
			ease = Ease.Linear;
			return false;
		}
		public bool TryGetInAnim( out bool value ) {
			if ( valid ) {
				if ( _inAnim.enabled ) {
					value = _inAnim.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetInAnim( out value );
			}
			value = false;
			return false;
		}
		public bool TryGetOutAnim( out bool value ) {
			if ( valid ) {
				if ( _outAnim.enabled ) {
					value = _outAnim.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetOutAnim( out value );
			}
			value = false;
			return false;
		}
		public bool TryGetInDuration( out float duration ) {
			if ( valid ) {
				if ( _inDuration.enabled ) {
					duration = _inDuration.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetInDuration( out duration );
			}
			duration = 1;
			return false;
		}
		public bool TryGetOutDuration( out float duration ) {
			if ( valid ) {
				if ( _outDuration.enabled ) {
					duration = _outDuration.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetOutDuration( out duration );
			}
			duration = 1;
			return false;
		}
		
		public bool TryGetHeight( out float height ) {
			if ( valid ) {
				if ( _height.enabled ) {
					height = _height.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetHeight( out height );
			}
			height = 30;
			return false;
		}

		public bool TryGetWidth( out float width ) {
			if ( valid ) {
				if ( _width.enabled ) {
					width = _width.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetWidth( out width );
			}
			width = 30;
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
		public bool TryGetOutlineColor( out Color color ) {
			if ( valid ) {
				if ( _outlineColor.enabled ) {
					color = _outlineColor.value;
					return true;
				}
				if ( parent != null )
					return parent.TryGetOutlineColor( out color );
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