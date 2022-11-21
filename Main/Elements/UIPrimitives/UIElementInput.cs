using TMPro;
using UIFlex.Elements.Utils;
using UnityEngine;

namespace UIFlex.Elements
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

		public override bool IsValid => inputField != null;

		protected override void OnDeactivate() => inputField.interactable = false;
		protected override void OnActivate() => inputField.interactable = true;

		protected override void OnValueSet()
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