using System;
using UIFlex.Elements.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UIFlex.Elements.UGUI
{
	public class UIElementButton : UGUIElement<Action>
	{
		[SerializeField] private Button button;

		public override bool IsValid => base.IsValid && button != null;

		protected override void OnDeactivate() => button.interactable = false;
		protected override void OnActivate() => button.interactable = true;

		protected override void OnValueSet()
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(value.Invoke);
		}

	}
}