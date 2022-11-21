using TMPro;
using UnityEngine;

namespace UIFlex.Elements
{
	public sealed class UIElementLabel : UGUIElement<string>
	{
		[SerializeField] private TMP_Text labelText;

		public override bool IsValid => labelText != null;

		protected override void OnDeactivate() => labelText.alpha = 0.5f;
		protected override void OnActivate() => labelText.alpha = 1;

		protected override void OnValueSet()
		{
			labelText.text = value;
		}
	}
}