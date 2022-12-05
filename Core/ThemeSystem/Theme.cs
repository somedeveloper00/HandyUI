using UnityEngine;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks every <see cref="ThemedElement"/> child with it's own theme</summary>
	public sealed class Theme : MonoBehaviour
	{
		[SerializeField] internal Style[] styles;
		private ThemedElement[] _elements;

		private void OnValidate() {
			Style.ResolveStyles(styles);
			_elements = GetComponentsInChildren<ThemedElement>();
			UpdateTheme();
		}

		/// <summary>Updates the theme of all child elements</summary>
		public void UpdateTheme() {
			foreach (var element in _elements) 
				UpdateStyle( element );

			void UpdateStyle(ThemedElement element) {
				for (int i = 0; i < styles.Length; i++) {
					if ( styles[i].name != element.styleName ) continue;
					element.UpdateTheme( styles[i] );
					return;
				}

				Debug.LogWarning( $"style not recognized: {element.styleName}" );
			}
		}
	}
}