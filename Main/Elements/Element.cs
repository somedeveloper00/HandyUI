using System;
using UIFlex.Common;
using UnityEngine;

namespace UIFlex.Elements
{
	[ExecuteAlways]
	public abstract class Element : MonoBehaviour
	{
		public Action onValueChanged;
	}
	
	/// <summary>
	/// A UGUI (Unity GUI system) Element
	/// </summary>
	public abstract class UGUIElement<T> : Element, IValidityCheck
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

		/// <summary>
		/// Whether the element is responsive and active or not
		/// </summary>
		public bool Visible
		{
			get => this.enabled;
			set => this.enabled = value;
		}

		protected void Awake() => OnInit();

		private void OnEnable()
		{
			if(IsValid) OnActivate();
		}

		private void OnDisable()
		{
			if(IsValid) OnDeactivate();
		}

		public abstract bool IsValid { get; }
		protected abstract void OnDeactivate();
		protected abstract void OnActivate();
		protected abstract void OnValueSet();
		
		protected virtual void OnValidate()
		{
			if (IsValid) OnValueSet();
		}
		protected virtual void OnInit() { }
	}
}