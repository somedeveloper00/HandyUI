using System;
using UIFlex.Common;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UIFlex.Elements
{
	[ExecuteAlways]
	public abstract class Element : MonoBehaviour, IValidityCheck
	{
		[SerializeField] protected RectContent rectContent = new RectContent()
		{
			alignment = Alignment.MidCenter,
			padding = 10,
			size = new Vector2(300, 80)
		};

		public Action onValueChanged;

		protected void OnValidate()
		{
			if (!IsValid) return; 
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

		public abstract bool IsValid { get; }
		
		private void OnEnable()
		{
			if (IsValid) OnActivate();
		}

		private void OnDisable()
		{
			if (IsValid) OnDeactivate();
		}
		
		protected void Awake() => OnInit();

		/// <summary>
		/// Whether the element is responsive and active or not
		/// </summary>
		public bool Visible
		{
			get => this.enabled;
			set => this.enabled = value;
		}
		
		
		protected abstract void UpdateTransforms();
		protected abstract void OnDeactivate();
		protected abstract void OnActivate();
		protected abstract void OnValueSet();

		protected virtual void OnInit() { }
	}

	public abstract class Element<T> : Element
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