using System;
using System.Collections.Generic;
using System.Linq;
using AnimFlex.Editor;
using HandyUI.ThemeSystem;
using UnityEditor;
using UnityEngine;

namespace HandyUI.Editor
{
	[CustomEditor( typeof(ThemedElement) )]
	public class ThemedElementEditor : UnityEditor.Editor
	{
		private ThemedElement instance;
		private Theme _parentTheme;
		private List<string> _options;

		private void OnEnable() {
			instance = target as ThemedElement;
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			if ( _parentTheme == null ) updateOptions();
			var styleNameProp = serializedObject.FindProperty( nameof(instance.styleName) );

			using (var check = new EditorGUI.ChangeCheckScope()) {
				using (new AFStyles.StyledGuiScope( this )) {
					using (new EditorGUI.DisabledScope( true )) {
						using (new GUILayout.HorizontalScope()) {
							EditorGUILayout.ObjectField( "Theme Object", _parentTheme, null, true );
							if ( _parentTheme != null ) {
								using (new AFStyles.GuiForceActive()) {
									if ( GUILayout.Button( "select", GUILayout.Width( 80 ) ) ) {
										serializedObject.ApplyModifiedProperties();
										Selection.activeObject = _parentTheme;
									}
								}
							}
						}
					}

					var selectedIndex = _options.IndexOf( styleNameProp.stringValue );
					if ( selectedIndex == -1 && _options.Count > 0 ) {
						styleNameProp.stringValue = _options[0];
						selectedIndex = 0;
					}

					using (var c = new EditorGUI.ChangeCheckScope()) {
						selectedIndex = EditorGUILayout.Popup( styleNameProp.displayName, selectedIndex,
							_options.ToArray() );
						if ( c.changed ) {
							styleNameProp.stringValue = _options[selectedIndex];
						}
					}
				}

				if ( check.changed ) {
					updateOptions();
				}
			}

			serializedObject.ApplyModifiedProperties();
		}

		private void updateOptions() {
			_parentTheme = instance.gameObject.GetComponentInParent<Theme>();
			_options ??= new();
			_options.Clear();
			foreach (var style in _parentTheme.styles) _options.Add( style.name );
		}
	}
}