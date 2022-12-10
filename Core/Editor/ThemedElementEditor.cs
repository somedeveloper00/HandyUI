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

		private void OnEnable() {
			instance = target as ThemedElement;
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();
			if ( _parentTheme == null ) updateOptions();
			var styleNameProp = serializedObject.FindProperty( nameof(instance.styleName) );
			var applySpriteProp = serializedObject.FindProperty( nameof(instance.applySprite) );
			var applyColorProp = serializedObject.FindProperty( nameof(instance.applyColor) );
			var applyHeightProp = serializedObject.FindProperty( nameof(instance.applyHeight) );
			var applyFontSizeProp = serializedObject.FindProperty( nameof(instance.applyFontSize) );
			var applyFontStyleProp = serializedObject.FindProperty( nameof(instance.applyFontStyle) );
			var applyFontProp = serializedObject.FindProperty( nameof(instance.applyFont) );
			var applyInEaseProp = serializedObject.FindProperty( nameof(instance.applyInEase) );
			var applyOutEaseProp = serializedObject.FindProperty( nameof(instance.applyOutEase) );
			var applyInDurationProp = serializedObject.FindProperty( nameof(instance.applyInDuration) );
			var applyOutDurationProp = serializedObject.FindProperty( nameof(instance.applyOutDuration) );
			
			var TweenersInProp = serializedObject.FindProperty( nameof(instance._tweenersIn) );
			var TweenersOutProp = serializedObject.FindProperty( nameof(instance._tweenersOut) );
			

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
				if ( _parentTheme != null && _parentTheme.stylePack != null && _parentTheme.stylePack.styles != null &&
				     _parentTheme.stylePack.styles.All( s => s.name != styleNameProp.stringValue ) ) {
					EditorGUILayout.HelpBox(
						$"Style \'{styleNameProp.stringValue}\' not found in parent theme game object {_parentTheme.name}",
						MessageType.Warning );
				}

				using (var check = new EditorGUI.ChangeCheckScope()) {
					EditorGUILayout.PropertyField( styleNameProp,
						new GUIContent( "Style name", styleNameProp.tooltip ) );
					if ( check.changed )
						styleNameProp.stringValue = styleNameProp.stringValue.ToLower();
				}
			}

			void drawOverrideButtons() {
				EditorGUILayout.PropertyField( applySpriteProp );
				EditorGUILayout.PropertyField( applyColorProp );
				EditorGUILayout.PropertyField( applyFontSizeProp );
				EditorGUILayout.PropertyField( applyHeightProp );
				EditorGUILayout.PropertyField( applyFontStyleProp );
				EditorGUILayout.PropertyField( applyFontProp );
				EditorGUILayout.PropertyField( applyInEaseProp );
				EditorGUILayout.PropertyField( applyInDurationProp );
				if ( applyInEaseProp.boolValue || applyInDurationProp.boolValue ) {
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField( TweenersInProp );
					EditorGUI.indentLevel--;
				}
				EditorGUILayout.PropertyField( applyOutEaseProp );
				EditorGUILayout.PropertyField( applyOutDurationProp );
				if ( applyOutEaseProp.boolValue || applyOutDurationProp.boolValue ) {
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField( TweenersOutProp );
					EditorGUI.indentLevel--;
				}
				
				
				using (new GUILayout.HorizontalScope()) {
					if ( GUILayout.Button( "select all", GUILayout.Width( 100 ) ) ) {
						applySpriteProp.boolValue = applyColorProp.boolValue = applyHeightProp.boolValue =
							applyFontSizeProp.boolValue = applyFontStyleProp.boolValue = applyInEaseProp.boolValue =
								applyOutEaseProp.boolValue = applyInDurationProp.boolValue =
									applyOutDurationProp.boolValue = applyFontProp.boolValue = true;
					}

					if ( GUILayout.Button( "deselect all", GUILayout.Width( 100 ) ) ) {
						applySpriteProp.boolValue = applyColorProp.boolValue = applyHeightProp.boolValue =
							applyFontSizeProp.boolValue = applyFontStyleProp.boolValue = applyInEaseProp.boolValue =
								applyOutEaseProp.boolValue = applyInDurationProp.boolValue =
									applyOutDurationProp.boolValue = applyFontProp.boolValue = false;
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
		}
	}
}