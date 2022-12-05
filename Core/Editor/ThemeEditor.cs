using AnimFlex.Editor;
using HandyUI.ThemeSystem;
using UnityEditor;

namespace HandyUI.Editor
{
	[CustomEditor( typeof(Theme) )]
	public class ThemeEditor : UnityEditor.Editor
	{
		private Theme instance;

		void OnEnable() {
			instance = target as Theme;
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			using (var check = new EditorGUI.ChangeCheckScope()) {
				using (new AFStyles.StyledGuiScope(this)) {
					base.OnInspectorGUI();
				}

				if ( check.changed ) {
					Style.ResolveStyles( instance.styles );
				}
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}