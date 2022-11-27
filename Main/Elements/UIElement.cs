using System;
using UIFlex.Common;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UIFlex.Elements
{
	public abstract class UIElement : MonoBehaviour, IValidityCheck
	{
		[SerializeField] protected RectContent rectContent = new RectContent()
		{
			alignment = Alignment.MidCenter,
			padding = 10,
			size = new Vector2(300, 80)
		};

		public Action onValueChanged;

		public abstract bool IsValid { get; }
		
		/// <summary>
		/// Whether the element is responsive and active or not
		/// </summary>
		public bool Visible
		{
			get => this.enabled;
			set => this.enabled = value;
		}


		internal abstract void UpdateTransforms();
		internal abstract void OnDeactivate();
		internal abstract void OnActivate();
		internal abstract void OnValueSet();

		internal virtual void OnInit() { }
	}

	public abstract class UIElement<T> : UIElement
	{
		[SerializeField] protected T value;

		/// <summary>
		/// The represented value by this element
		/// </summary>
		public T Value
		{
			get => value;
			set
			{
				if (Equals(this.value, value)) return;
				this.value = value;
				OnValueSet();
				onValueChanged?.Invoke();
			}
		}
	}

	public enum Alignment
	{
		Fill,
		TopLeft, TopCenter, TopRight,
		MidLeft, MidCenter, MidRight,
		BottomLeft, BottomCenter, BottomRight
	}

	[Serializable]
	public struct RectContent
	{
		public Alignment alignment;
		[Min(0)] 
		public float padding;
		public Vector2 size;
	}
}