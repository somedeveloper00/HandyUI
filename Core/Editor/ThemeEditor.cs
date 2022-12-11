using System;
using AnimFlex.Editor;
using HandyUI.ThemeSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace HandyUI.Editor
{
	[CustomEditor( typeof(Theme) )]
	public class ThemeEditor : UnityEditor.Editor
	{
		private Theme instance;
		private UnityEditor.Editor _editor;
		private bool _expanded = true;

		void OnEnable() {
			instance = target as Theme;
			instance.UpdateTheme();
		}

		public override void OnInspectorGUI() {
			Profiler.BeginSample( "Theme Editor" );
			serializedObject.Update();
			var stylePackProp = serializedObject.FindProperty( nameof(instance.stylePack) );

			using (var check = new EditorGUI.ChangeCheckScope()) {
				using (new AFStyles.StyledGuiScope( this )) {
					var rect = EditorGUILayout.GetControlRect( false, AFStyles.Height );
					float w = rect.width;
					rect.width = EditorGUIUtility.labelWidth;
					_expanded = EditorGUI.Foldout( rect, _expanded,
						new GUIContent( stylePackProp.displayName, stylePackProp.tooltip ), true );
					rect.x += rect.width;
					rect.width = w - rect.width;
					EditorGUI.PropertyField( rect, stylePackProp, GUIContent.none );

					if ( _expanded ) {
						if ( _editor == null && instance.stylePack != null ) {
							_editor = CreateEditor( instance.stylePack );
						}

						if ( _editor != null ) {
							EditorGUI.indentLevel++;
							_editor.OnInspectorGUI();
							EditorGUI.indentLevel--;
						}
					}
				}

				if ( check.changed ) {
					Profiler.BeginSample( "change" );
					serializedObject.ApplyModifiedProperties();
					instance.UpdateTheme();
					Profiler.EndSample();
				}
			}
			Profiler.EndSample();
		}
	}
}