using System;
using UnityEngine;
using UnityEngine.UI;

namespace UIFlex.Elements
{
	public class UIElementButton : UGUIElement<Action>
	{
		[SerializeField] private Button button;

		public override bool IsValid => button != null;

		protected override void OnDeactivate() => button.interactable = false;
		protected override void OnActivate() => button.interactable = true;

		protected override void OnValueSet()
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(value.Invoke);
		}
	}
}