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

		internal override void OnDeactivate() => button.interactable = false;
		internal override void OnActivate() => button.interactable = true;

		internal override void OnValueSet()
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(value.Invoke);
		}

	}
}