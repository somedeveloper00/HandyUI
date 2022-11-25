using UIFlex.Elements.Utils;
using UnityEngine;

namespace UIFlex.Elements.UGUI
{
	/// <summary>
	/// UGUI (Unity's Canvas system) element
	/// </summary>
	public abstract class UGUIElement<T> : Element<T>
	{
		[SerializeField] private RectTransform anchorTransform;

		public override bool IsValid => anchorTransform != null;

		protected override void UpdateTransforms()
		{
			UGUIHelpers.SetRectTransformProps(anchorTransform, rectContent.alignment, rectContent.size, rectContent.padding);
		}
	}
}