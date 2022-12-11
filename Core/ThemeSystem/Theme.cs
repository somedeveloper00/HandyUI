using System;
using System.Collections;
using System.Collections.Generic;
using AnimFlex.Tweening;
using UnityEngine;
using UnityEngine.Profiling;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks every <see cref="ThemedElement"/> child with it's own theme</summary>
	[ExecuteAlways]
	public sealed class Theme : MonoBehaviour
	{
		public StylePack stylePack;
		private ThemedElement[] _elements = Array.Empty<ThemedElement>();

		private Action _onDestroy;

		private void OnEnable() {
			UpdateTheme();
#if UNITY_EDITOR
			if (!Application.isPlaying) return;
#endif
			foreach (var element in _elements) element.PlayInAnim();
		}

		public void DestroyWithAnim() => DestroyWithAnim( null ); 
		public void DestroyWithAnim(Action onDestroy) {
			float maxDuration = 0;
			Tweener lastTweener = null;
			foreach (var element in _elements) {
				element.PlayOutAnim(out Tweener tweener, out float d);
				if ( d > maxDuration ) {
					maxDuration = d;
					lastTweener = tweener;
				}
			}

			_onDestroy = onDestroy;
			lastTweener.onComplete += () => Destroy( gameObject );
		}

		private void OnDestroy() => _onDestroy?.Invoke();

		private void OnValidate() => UpdateTheme();

		/// <summary>Updates the theme of all child elements</summary>
		public void UpdateTheme(bool refreshElements = true) {
			Profiler.BeginSample( "Theme Update" );
			if ( refreshElements ) this.refreshElements();
			foreach (var element in _elements) {
				UpdateStyle( element );
			}
			Profiler.EndSample();

			void UpdateStyle(ThemedElement element) {
				if ( stylePack == null || stylePack.styles == null ) return;
				for (int i = 0; i < stylePack.styles.Length; i++) {
					if ( stylePack.styles[i].name != element.styleName ) continue;
					element.UpdateTheme( stylePack.styles[i] );
					return;
				}
			}
		}

		private void refreshElements() {
			var results = new List<ThemedElement>(100);
			add_children( transform );
			_elements = results.ToArray();

			void add_children(Transform trans) {
				if ( trans != transform && trans.TryGetComponent<Theme>( out _ ) ) return;
				if ( trans.TryGetComponent<ThemedElement>( out var elem ) ) results.Add( elem );
				foreach (Transform child in trans.transform) 
					add_children( child );
			}
		}
	}
}