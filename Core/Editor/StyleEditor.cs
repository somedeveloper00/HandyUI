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
			var theme = property.serializedObject.targetObject as Theme;
			var nameProp = property.FindPropertyRelative( nameof(Style.name) );
			var parentNameProp = property.FindPropertyRelative( nameof(Style.parentName) );
			var fontSizeNameProp = property.FindPropertyRelative( "_fontSize" );
			var spriteProp = property.FindPropertyRelative( "_sprite" );
			var colorProp = property.FindPropertyRelative( "_color" );
			var fontStyleProp = property.FindPropertyRelative( "_fontStyle" );


			var styles = new List<string>();
			styles.Add( "None" );
			styles.AddRange( theme.styles.Select( s => s.name ) );
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
				EditorGUI.PropertyField( position, spriteProp );
				position.y += EditorGUI.GetPropertyHeight( spriteProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, fontSizeNameProp );
				position.y += EditorGUI.GetPropertyHeight( fontSizeNameProp ) + AFStyles.VerticalSpace;
				EditorGUI.PropertyField( position, fontStyleProp );
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			var nameProp = property.FindPropertyRelative( nameof(Style.name) );
			var parentNameProp = property.FindPropertyRelative( nameof(Style.parentName) );
			var fontSizeNameProp = property.FindPropertyRelative( "_fontSize" );
			var spriteProp = property.FindPropertyRelative( "_sprite" );
			var colorProp = property.FindPropertyRelative( "_color" );
			var fontStyleProp = property.FindPropertyRelative( "_fontStyle" );

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
			}
			return h;
		}
	}
}