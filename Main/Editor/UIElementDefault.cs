using AnimFlex.Editor;
using UIFlex.Elements;

namespace UIFlex.Editor
{
	[UnityEditor.CustomEditor(typeof(UIElement), true, isFallback = true)]
	public class UIElementDefault : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			using (new AFStyles.StyledGuiScope())
			{
				base.OnInspectorGUI();
			}
		}
	}
}