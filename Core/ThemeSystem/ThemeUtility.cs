using System;
using System.Threading.Tasks;
using AnimFlex.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HandyUI.ThemeSystem
{
	public static class ThemeUtility
	{
		public static async Task DestroyThemedElementsWithAnimAsync(GameObject root) {
			await PlayOutAnimsAsync( root );
			Object.Destroy( root );
		}

		public static void DestroyThemedElementsWithAnim(GameObject root, Action onDestroy) {
			PlayOutAnims( root, () => {
				Object.Destroy( root );
				onDestroy?.Invoke();
			} );
		}

		public static void PlayOutAnims(GameObject root, Action onComplete) {
			float maxDuration = 0;
			Tweener lastTweener = null;
			foreach ( var element in root.GetComponentsInChildren<ThemedElement>() ) {
				element.PlayOutAnim( out var tweener, out var d );
				if ( d > maxDuration ) {
					maxDuration = d;
					lastTweener = tweener;
				}
			}

			if ( lastTweener != null )
				lastTweener.onComplete += () => { onComplete?.Invoke(); };
			else {
				onComplete?.Invoke();
			}
		}

		public static async Task PlayOutAnimsAsync(GameObject root) {
			bool done = false;
			PlayOutAnims( root, () => done = true );
			while (!done) await Task.Yield();
		}

		public static void PlayInAnims(GameObject root, Action onComplete) {
			float maxDuration = 0;
			Tweener lastTweener = null;
			foreach ( var element in root.GetComponentsInChildren<ThemedElement>() ) {
				element.PlayInAnim( out var tweener, out var d );
				if ( d > maxDuration ) {
					maxDuration = d;
					lastTweener = tweener;
				}
			}

			if ( lastTweener != null )
				lastTweener.onComplete += () => { onComplete?.Invoke(); };
			else {
				onComplete?.Invoke();
			}
		}

		public static async Task PlayInAnimsAsync(GameObject root) {
			bool done = false;
			PlayInAnims( root, () => done = true );
			while (!done) await Task.Yield();
		}
	}
}