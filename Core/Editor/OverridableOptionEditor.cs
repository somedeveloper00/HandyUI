using AnimFlex.Editor;
using HandyUI.ThemeSystem;
using UnityEditor;
using UnityEngine;

namespace HandyUI.Editor
{
	[CustomPropertyDrawer( typeof(Style.OverridableOption), true )]
	public class OverridableOptionEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var enabledProp = property.FindPropertyRelative( nameof(Style.OverridableOption<bool>.enabled) );
			var valueProp = property.FindPropertyRelative( nameof(Style.OverridableOption<bool>.value) );

			var r = new Rect( position );
			r.height = AFStyles.Height;
			
			using (new EditorGUI.PropertyScope( position, label, property )) {
				r.width = EditorGUIUtility.labelWidth;
				using (new AFStyles.EditorLabelWidth( 80 ))
					EditorGUI.PropertyField( r, enabledProp, label );
				
				r.x += r.width ;
				r.width = position.width - EditorGUIUtility.labelWidth ;
				if ( enabledProp.boolValue ) {
					using (new AFStyles.EditorLabelWidth( 10 ))
					EditorGUI.PropertyField( r, valueProp, new GUIContent(" ") );
				}
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			var enabledProp = property.FindPropertyRelative( nameof(Style.OverridableOption<bool>.enabled) );
			var valueProp = property.FindPropertyRelative( nameof(Style.OverridableOption<bool>.value) );
			var h = AFStyles.Height + AFStyles.VerticalSpace;
			if ( enabledProp.boolValue ) 
				h = Mathf.Max( EditorGUI.GetPropertyHeight( valueProp ), AFStyles.Height ) + AFStyles.VerticalSpace;
			return h;
		}
	}
}