using System.Collections.Generic;
using System.Linq;
using AnimFlex.Editor;
using HandyUI.ThemeSystem;
using UnityEditor;
using UnityEngine;

namespace HandyUI.Editor
{
	[CustomPropertyDrawer( typeof(Style) )]
	public class StyleEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var target = property.GetValue() as Style;
			var stylePack = property.serializedObject.targetObject as StylePack;
			var nameProp = property.FindPropertyRelative( nameof(Style.name) );
			var parentNameProp = property.FindPropertyRelative( nameof(Style.parentName) );
			var heightNameProp = property.FindPropertyRelative( "_height" );
			var fontSizeProp = property.FindPropertyRelative( "_fontSize" );
			var spriteProp = property.FindPropertyRelative( "_sprite" );
			var colorProp = property.FindPropertyRelative( "_color" );
			var outlineColorProp = property.FindPropertyRelative( "_outlineColor" );
			var fontStyleProp = property.FindPropertyRelative( "_fontStyle" );
			var fontProp = property.FindPropertyRelative( "_font" );
			var inEaseProp = property.FindPropertyRelative( "_inEase" );
			var outEaseProp = property.FindPropertyRelative( "_outEase" );
			var inDurationProp = property.FindPropertyRelative( "_inDuration" );
			var outDurationProp = property.FindPropertyRelative( "_outDuration" );
			var inAnimProp = property.FindPropertyRelative( "_inAnim" );
			var outAnimProp = property.FindPropertyRelative( "_outAnim" );


			var styles = new List<string>();
			styles.Add( "None" );
			if ( stylePack != null ) styles.AddRange( stylePack.styles.Select( s => s.name ) );
			styles.Remove( nameProp.stringValue ); // remove self

			int selected = styles.IndexOf( parentNameProp.stringValue, 1 );
			if ( selected == -1 ) selected = 0;

			position.height = AFStyles.Height;

			using (new EditorGUI.PropertyScope( position, label, property )) {
				using (new AFStyles.StyledGuiScope()) {
					if ( !target.valid ) {
						AFStyles.DrawHelpBox( position, "Style not valid.", MessageType.Warning );
						position.y += AFStyles.Height + AFStyles.VerticalSpace;
					}

					property.isExpanded = EditorGUI.Foldout( position, property.isExpanded, nameProp.stringValue, true, AFStyles.Foldout );
					position.y += AFStyles.Height + AFStyles.VerticalSpace;
					
					if ( property.isExpanded ) {
						drawBody();
					}
				}
			}

			void drawBody() {
				using (var check = new EditorGUI.ChangeCheckScope()) {
					selected = EditorGUI.Popup( position, "Parent", selected, styles.ToArray() );
					if ( check.changed ) {
						parentNameProp.stringValue = selected == 0 ? string.Empty : styles[selected];
					}
				}

				position.y += AFStyles.Height + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, nameProp );
				position.y += EditorGUI.GetPropertyHeight( nameProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, colorProp );
				position.y += EditorGUI.GetPropertyHeight( colorProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, outlineColorProp );
				position.y += EditorGUI.GetPropertyHeight( outlineColorProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, spriteProp );
				position.y += EditorGUI.GetPropertyHeight( spriteProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, heightNameProp );
				position.y += EditorGUI.GetPropertyHeight( heightNameProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, fontSizeProp );
				position.y += EditorGUI.GetPropertyHeight( fontSizeProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, fontStyleProp );
				position.y += EditorGUI.GetPropertyHeight( fontStyleProp ) + AFStyles.VerticalSpace;
				
				EditorGUI.PropertyField( position, inAnimProp );
				position.y += EditorGUI.GetPropertyHeight( inAnimProp ) + AFStyles.VerticalSpace;
				if ( inAnimProp.FindPropertyRelative("value").boolValue ) {
					EditorGUI.PropertyField( position, inEaseProp );
					position.y += EditorGUI.GetPropertyHeight( inEaseProp ) + AFStyles.VerticalSpace;
					EditorGUI.PropertyField( position, inDurationProp );
					position.y += EditorGUI.GetPropertyHeight( inDurationProp ) + AFStyles.VerticalSpace;
				}
				EditorGUI.PropertyField( position, outAnimProp );
				position.y += EditorGUI.GetPropertyHeight( outAnimProp ) + AFStyles.VerticalSpace;
				if ( outAnimProp.FindPropertyRelative("value").boolValue ) {
					EditorGUI.PropertyField( position, outEaseProp );
					position.y += EditorGUI.GetPropertyHeight( outEaseProp ) + AFStyles.VerticalSpace;
					EditorGUI.PropertyField( position, outDurationProp );
					position.y += EditorGUI.GetPropertyHeight( outDurationProp ) + AFStyles.VerticalSpace;
				}

				EditorGUI.PropertyField( position, fontProp );
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			var nameProp = property.FindPropertyRelative( nameof(Style.name) );
			var parentNameProp = property.FindPropertyRelative( nameof(Style.parentName) );
			var fontSizeNameProp = property.FindPropertyRelative( "_fontSize" );
			var spriteProp = property.FindPropertyRelative( "_sprite" );
			var heightNameProp = property.FindPropertyRelative( "_height" );
			var widthNameProp = property.FindPropertyRelative( "_width" );
			var colorProp = property.FindPropertyRelative( "_color" );
			var outlineColorProp = property.FindPropertyRelative( "_outlineColor" );
			var fontStyleProp = property.FindPropertyRelative( "_fontStyle" );
			var fontProp = property.FindPropertyRelative( "_font" );
			var inEaseProp = property.FindPropertyRelative( "_inEase" );
			var outEaseProp = property.FindPropertyRelative( "_outEase" );
			var inDurationProp = property.FindPropertyRelative( "_inDuration" );
			var outDurationProp = property.FindPropertyRelative( "_outDuration" );
			var inAnimProp = property.FindPropertyRelative( "_inAnim" );
			var outAnimProp = property.FindPropertyRelative( "_outAnim" );
			
			var target = property.GetValue() as Style;
			var h = 0f;
			if ( !target.valid )
				h += AFStyles.Height + AFStyles.VerticalSpace;
			h += AFStyles.Height + AFStyles.VerticalSpace; // header
			if ( property.isExpanded ) {
				h += EditorGUI.GetPropertyHeight( nameProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( parentNameProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( fontSizeNameProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( fontStyleProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( spriteProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( colorProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( outlineColorProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( fontProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( heightNameProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( widthNameProp ) + AFStyles.VerticalSpace;
				h += EditorGUI.GetPropertyHeight( inAnimProp ) + AFStyles.VerticalSpace;
				if ( inAnimProp.FindPropertyRelative("value").boolValue ) {
					h += EditorGUI.GetPropertyHeight( inEaseProp ) + AFStyles.VerticalSpace;
					h += EditorGUI.GetPropertyHeight( inDurationProp ) + AFStyles.VerticalSpace;
				}
				h += EditorGUI.GetPropertyHeight( outAnimProp ) + AFStyles.VerticalSpace;
				if ( outAnimProp.FindPropertyRelative("value").boolValue ) {
					h += EditorGUI.GetPropertyHeight( outEaseProp ) + AFStyles.VerticalSpace;
					h += EditorGUI.GetPropertyHeight( outDurationProp ) + AFStyles.VerticalSpace;
				}
			}
			return h;
		}
	}
}