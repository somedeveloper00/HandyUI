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
			var applySpriteProp = serializedObject.FindProperty( nameof(instance.applySprite) );
			var applyColorProp = serializedObject.FindProperty( nameof(instance.applyColor) );
			var applyFontSizeProp = serializedObject.FindProperty( nameof(instance.applyFontSize) );
			var applyFontStyleProp = serializedObject.FindProperty( nameof(instance.applyFontStyle) );
			var applyFontProp = serializedObject.FindProperty( nameof(instance.applyFont) );

			using (var check = new EditorGUI.ChangeCheckScope()) {
				using (new AFStyles.StyledGuiScope( this )) {
					if ( _parentTheme == null ) {
						EditorGUILayout.HelpBox( "Element cannot find themed parent", MessageType.Info );
					}
					else {
						drawParentField();
					}
					drawBody();
					drawOverrideButtons();
				}

				if ( check.changed ) {
					updateOptions();
				}
			}

			serializedObject.ApplyModifiedProperties();


			void drawBody() {
				// style selection
				if ( _parentTheme != null && _parentTheme.styles != null && 
				     !_parentTheme.styles.Any( s => s.name == styleNameProp.stringValue ) ) {
					EditorGUILayout.HelpBox(
						$"Style \'{styleNameProp.stringValue}\' not found in parent theme game object {_parentTheme.name}",
						MessageType.Warning );
				}

				using (var check = new EditorGUI.ChangeCheckScope()) {
					EditorGUILayout.PropertyField( styleNameProp, new GUIContent( "Style name", styleNameProp.tooltip ) );
					if ( check.changed )
						styleNameProp.stringValue = styleNameProp.stringValue.ToLower();
				}
			}

			void drawOverrideButtons() {
				EditorGUILayout.PropertyField( applySpriteProp );
				EditorGUILayout.PropertyField( applyColorProp );
				EditorGUILayout.PropertyField( applyFontSizeProp );
				EditorGUILayout.PropertyField( applyFontStyleProp );
				EditorGUILayout.PropertyField( applyFontProp );
				using (new GUILayout.HorizontalScope()) {
					if ( GUILayout.Button( "select all", GUILayout.Width( 100 ) ) ) {
						applySpriteProp.boolValue = applyColorProp.boolValue =
							applyFontSizeProp.boolValue = applyFontStyleProp.boolValue = true;
					}

					if ( GUILayout.Button( "deselect all", GUILayout.Width( 100 ) ) ) {
						applySpriteProp.boolValue = applyColorProp.boolValue =
							applyFontSizeProp.boolValue = applyFontStyleProp.boolValue = false;
					}
				}
			}

			void drawParentField() {
				using (new EditorGUI.DisabledScope( true )) {
					using (new GUILayout.HorizontalScope()) {
						EditorGUILayout.ObjectField( "Theme Object", _parentTheme, typeof(Theme), true );
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
			}
		}


		private void updateOptions() {
			_parentTheme = instance.gameObject.GetComponentInParent<Theme>();
			_options ??= new();
			_options.Clear();
			if ( _parentTheme == null ) return;
			foreach (var style in _parentTheme.styles) _options.Add( style.name );
		}
	}
}