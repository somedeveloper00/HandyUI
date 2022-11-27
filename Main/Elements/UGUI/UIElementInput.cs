using TMPro;
using UIFlex.Elements.Utils;
using UnityEngine;

namespace UIFlex.Elements.UGUI
{
	public sealed class UIElementInput : UGUIElement<string>
	{
		public enum FieldType
		{
			Any, Float, Int, Custom
		}
		[SerializeField] private TMP_InputField inputField;
		public FieldType fieldType;
		
		public delegate string OnValidation(string input);
		public event OnValidation OnCustomValidation;

		public override bool IsValid => base.IsValid && inputField != null;

		internal override void OnDeactivate() => inputField.interactable = false;
		internal override void OnActivate() => inputField.interactable = true;

		internal override void OnValueSet()
		{
			switch (fieldType)
			{
				case FieldType.Int:
					value = ((int)StringHelpers.GetFloatFromStr(value)).ToString();
					break;
				
				case FieldType.Float:
					value = StringHelpers.GetFloatFromStr(value).ToString();
					break;
				
				case FieldType.Custom:
					value = OnCustomValidation?.Invoke(value);
					break;
			}
			inputField.text = value;
		}
	}
}