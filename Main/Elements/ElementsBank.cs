using UIFlex.Elements.UGUI;
using UnityEngine;

namespace UIFlex.Elements
{
	[CreateAssetMenu(menuName = "Elements Bank")]
	public class ElementsBank : ScriptableObject
	{
		[SerializeField] private UIElementLabel uiElementLabel;
		[SerializeField] private UIElementBool uiElementBool;
		[SerializeField] private UIElementInput uiElementInput;

		public UIElementLabel NewLabelElement(Transform parent)
		{
			return Instantiate(uiElementLabel, parent);
		}

		public UIElementBool NewBoolElement(Transform parent)
		{
			return Instantiate(uiElementBool, parent);
		}

		public UIElementInput NewInputElement(Transform parent)
		{
			return Instantiate(uiElementInput, parent);
		}
	}
}