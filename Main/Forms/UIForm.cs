using System.Linq;
using UIFlex.Elements;
using UnityEditor;
using UnityEngine;

namespace UIFlex.Forms
{
	/// <summary>
	/// A form is a collection of elements
	/// </summary>
	[ExecuteAlways]
	internal class UIForm : UIElement
	{
		/// <summary>
		/// The bank this form uses to instantiate elements
		/// </summary>
		[Tooltip("The bank this form uses to instantiate elements")]
		public UIElementsBank bank;
		
		[SerializeField] private UIElement[] _elements;

		public override bool IsValid => _elements.All(e => e.IsValid);
		
		internal override void UpdateTransforms()	=> _elements.ForEach(e => e.UpdateTransforms());

		internal override void OnDeactivate()		=> _elements.ForEach(e => e.OnDeactivate());
		internal override void OnActivate()			=> _elements.ForEach(e => e.OnActivate());
		internal override void OnValueSet()			=> _elements.ForEach(e => e.OnValueSet());
		internal override void OnInit()
		{
			base.OnInit();
			_elements.ForEach(e => e.OnInit());
		}

		protected virtual void OnValidate()
		{
			if (!IsValid) return;
			var elems = GetComponentsInChildren<UIElement>();
			foreach (var elem in elems)
			{
				if(ReferenceEquals(elem, this)) continue;
				if (!_elements.Contains(elem))
				{
					ArrayUtility.Add(ref _elements, elem);
				}
			}
			
#if UNITY_EDITOR // some properties aren't allowed to be modified from OnValidate phase
			if (!Application.isPlaying)
				EditorApplication.delayCall += () =>
				{
					if(IsValid) UpdateTransforms();
				};
			else
#endif
			UpdateTransforms();
			OnValueSet();
		}

		private void Awake() => OnInit();
		private void OnEnable() => OnActivate();
		private void OnDisable() => OnDeactivate();
		
	}
}