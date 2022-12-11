using System;
using HandyUI.ThemeSystem;
using UnityEditor;
using UnityEngine.Profiling;

namespace HandyUI.Editor
{
	[CustomEditor( typeof(StylePack) )]
	public class StylePackEditor : UnityEditor.Editor
	{
		private StylePack instance;
		private void OnEnable() {
			instance = target as StylePack;
		}

		public override void OnInspectorGUI() {
			Profiler.BeginSample( "Style Pack Editor" );
			serializedObject.Update();
			var stylesProp = serializedObject.FindProperty( nameof(instance.styles) );
			EditorGUILayout.PropertyField( stylesProp );
			serializedObject.ApplyModifiedProperties();
			Profiler.EndSample();
		}
	}
}