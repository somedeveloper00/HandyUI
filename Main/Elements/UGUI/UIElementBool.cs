using UnityEngine;
using UnityEngine.UI;

namespace UIFlex.Elements.UGUI
{
	public sealed class UIElementBool : UGUIElement<bool>
	{
		[SerializeField] private Toggle toggle;

		protected override void OnInit()
		{
			base.OnInit();
			toggle.onValueChanged.AddListener(onToggleChanged);
		}

		private void onToggleChanged(bool v) => value = v;

		public override bool IsValid => base.IsValid && toggle != null;

		protected override void OnDeactivate() => toggle.interactable = false;
		protected override void OnActivate() => toggle.interactable = true;
		
		protected override void OnValueSet() => toggle.isOn = value;
		
	}
}
