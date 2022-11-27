using TMPro;
using UnityEngine;

namespace UIFlex.Elements.UGUI
{
	public sealed class UIElementLabel : UGUIElement<string>
	{
		[SerializeField] private TMP_Text labelText;

		public override bool IsValid => base.IsValid && labelText != null;

		internal override void OnDeactivate() => labelText.alpha = 0.5f;
		internal override void OnActivate() => labelText.alpha = 1;

		internal override void OnValueSet()
		{
			labelText.text = value;
		}
	}
}