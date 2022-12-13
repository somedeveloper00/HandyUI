using System;
using System.Threading.Tasks;
using UnityEngine;

namespace HandyUI.ThemeSystem
{
	[AddComponentMenu( "Handy UI/Themed Element Anim Player" )]
	public class ThemedElementAnimPlayer : MonoBehaviour
	{
		public bool playOnStart = true;

		private void Start() {
			if (playOnStart) PlayInAnimAsync();
		}

		public async Task PlayInAnimAsync() {
			await ThemeUtility.PlayInAnimsAsync( gameObject );
		}

		public async Task PlayOutAnimAsync() {
			await ThemeUtility.PlayOutAnimsAsync( gameObject );
		}

		public async void DestroyWithAnim() {
			await ThemeUtility.DestroyThemedElementsWithAnimAsync( gameObject );
		}
	}
}