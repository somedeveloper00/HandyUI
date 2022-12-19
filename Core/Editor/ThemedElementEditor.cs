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
	[CanEditMultipleObjects]
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
			var applyWidthProp = serializedObject.FindProperty( nameof(instance.applyWidth) );
			var applyFontSizeProp = serializedObject.FindProperty( nameof(instance.applyFontSize) );
			var applyFontStyleProp = serializedObject.FindProperty( nameof(instance.applyFontStyle) );
			var applyFontProp = serializedObject.FindProperty( nameof(instance.applyFont) );
			var applyPlayInAnimProp = serializedObject.FindProperty( nameof(instance.applyPlayInAnim) );
			var playInAnimProp = serializedObject.FindProperty( nameof(instance.playInAnim) );
			var applyInEaseProp = serializedObject.FindProperty( nameof(instance.applyInEase) );
			var applyInDurationProp = serializedObject.FindProperty( nameof(instance.applyInDuration) );
			var applyPlayOutAnimProp = serializedObject.FindProperty( nameof(instance.applyPlayOutAnim) );
			var playOutAnimProp = serializedObject.FindProperty( nameof(instance.playOutAnim) );
			var applyOutEaseProp = serializedObject.FindProperty( nameof(instance.applyOutEase) );
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
				using (var check = new EditorGUI.ChangeCheckScope()) {
					EditorGUILayout.PropertyField( styleNameProp,
						new GUIContent( "Style name", styleNameProp.tooltip ) );
					if ( check.changed )
						styleNameProp.stringValue = styleNameProp.stringValue.ToLower();
				}
				if ( _parentTheme != null && _parentTheme.stylePack != null && _parentTheme.stylePack.styles != null &&
				     _parentTheme.stylePack.styles.All( s => s.name != styleNameProp.stringValue ) ) {
					EditorGUILayout.HelpBox(
						$"Style \'{styleNameProp.stringValue}\' not found in parent theme game object {_parentTheme.name}",
						MessageType.Warning );
				}
			}

			void drawOverrideButtons() {
				EditorGUILayout.PropertyField( applySpriteProp );
				EditorGUILayout.PropertyField( applyColorProp );
				EditorGUILayout.PropertyField( applyFontSizeProp );
				EditorGUILayout.PropertyField( applyHeightProp );
				EditorGUILayout.PropertyField( applyWidthProp );
				EditorGUILayout.PropertyField( applyFontStyleProp );
				EditorGUILayout.PropertyField( applyFontProp );
				EditorGUILayout.PropertyField( applyPlayInAnimProp );
				EditorGUILayout.PropertyField( playInAnimProp );
				if ( playInAnimProp.boolValue ) {
					using (new EditorGUI.IndentLevelScope()) {
						EditorGUILayout.PropertyField( applyInEaseProp );
						EditorGUILayout.PropertyField( applyInDurationProp );
						EditorGUILayout.PropertyField( TweenersInProp );
					}
				}
				EditorGUILayout.PropertyField( applyPlayOutAnimProp );
				EditorGUILayout.PropertyField( playOutAnimProp );
				if ( playOutAnimProp.boolValue ) {
					using (new EditorGUI.IndentLevelScope()) {
						EditorGUILayout.PropertyField( applyOutEaseProp );
						EditorGUILayout.PropertyField( applyOutDurationProp );
						EditorGUILayout.PropertyField( TweenersOutProp );
					}
				}
				GUILayout.Space( 5 );
				
				
				using (new GUILayout.HorizontalScope()) {
					if ( GUILayout.Button( "select all", GUILayout.Width( 100 ) ) ) {
						applySpriteProp.boolValue = applyColorProp.boolValue = applyHeightProp.boolValue =
							applyWidthProp.boolValue = applyFontSizeProp.boolValue = applyFontStyleProp.boolValue = 
								applyInEaseProp.boolValue = applyOutEaseProp.boolValue = 
									applyInDurationProp.boolValue = applyOutDurationProp.boolValue = 
										applyFontProp.boolValue = true;
					}

					if ( GUILayout.Button( "deselect all", GUILayout.Width( 100 ) ) ) {
						applySpriteProp.boolValue = applyColorProp.boolValue = applyHeightProp.boolValue =
							applyWidthProp.boolValue = applyFontSizeProp.boolValue = applyFontStyleProp.boolValue = 
								applyInEaseProp.boolValue = applyOutEaseProp.boolValue = 
									applyInDurationProp.boolValue = applyOutDurationProp.boolValue = 
										applyFontProp.boolValue = false;
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