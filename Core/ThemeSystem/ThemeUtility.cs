using System;
using System.Threading.Tasks;
using AnimFlex.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HandyUI.ThemeSystem
{
	public static class ThemeUtility
	{
		public static void DestroyThemedElementsWithAnim(GameObject root, Action onDestroy) {
			float maxDuration = 0;
			Tweener lastTweener = null;
			foreach (var element in root.GetComponentsInChildren<ThemedElement>()) {
				element.PlayOutAnim( out var tweener, out var d );
				if ( d > maxDuration ) {
					maxDuration = d;
					lastTweener = tweener;
				}
			}

			if ( lastTweener != null )
				lastTweener.onComplete += () => {
					Object.Destroy( root );
					onDestroy?.Invoke();
				};
			else {
				Object.Destroy( root );
				onDestroy?.Invoke();
			}
		}

		public static async Task DestroyThemedElementsWithAnimAsync(GameObject root) {
			bool done = false;
			DestroyThemedElementsWithAnim( root, () => done = true );
			while (!done) await Task.Delay( 10 );
		}
	}
}