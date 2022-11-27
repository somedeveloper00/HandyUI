using UnityEngine;
using UnityEngine.UI;

namespace UIFlex.Elements.UGUI
{
	public sealed class UIElementBool : UGUIElement<bool>
	{
		[SerializeField] private Toggle toggle;

		internal override void OnInit()
		{
			base.OnInit();
			toggle.onValueChanged.AddListener(onToggleChanged);
		}

		private void onToggleChanged(bool v) => value = v;

		public override bool IsValid => base.IsValid && toggle != null;

		internal override void OnDeactivate() => toggle.interactable = false;
		internal override void OnActivate() => toggle.interactable = true;

		internal override void OnValueSet() => toggle.isOn = value;
		
	}
}
