using System;
using AnimFlex.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks the gameObject as a specific element for theme system</summary>
	internal sealed class ThemedElement : MonoBehaviour
	{
		public string styleName;
		[SerializeField] internal bool applySprite = true;
		[SerializeField] internal bool applyColor = true;
		[SerializeField] internal bool applyHeight = true;
		[SerializeField] internal bool applyFont = true;
		[SerializeField] internal bool applyFontSize = true;
		[SerializeField] internal bool applyFontStyle = true;
		[SerializeField] internal bool applyInEase = true;
		[SerializeField] internal bool applyOutEase = true;
		[SerializeField] internal bool applyInDuration = true;
		[SerializeField] internal bool applyOutDuration = true;

		private Image _image;
		private TMP_Text _text;
		private LayoutElement _layoutElement;
		[SerializeField] internal TweenerComponent[] _tweenersIn;
		[SerializeField] internal TweenerComponent[] _tweenersOut;

		public void PlayInAnim() {
			foreach (var tweener in _tweenersIn) tweener.PlayOrRestart();
		}
		public void PlayOutAnim(out Tweener lastTweener, out float duration) {
			duration = 0;
			lastTweener = null;
			foreach (var tweener in _tweenersOut) {
				var t = tweener.PlayOrRestart();
				if ( tweener.duration > duration ) {
					duration = tweener.duration;
					lastTweener = t;
				}
			}
		}

		private void OnValidate() {
			_image = GetComponent<Image>();
			_text = GetComponent<TMP_Text>();
			_layoutElement = GetComponent<LayoutElement>();
		}

		internal void UpdateTheme(Style style) {
			if ( _tweenersIn != null ) {
				if ( applyInEase && style.TryGetInEase( out var ease ) )
					foreach (var tweener in _tweenersIn) tweener.ease = ease;
				if ( applyInEase && style.TryGetInDuration( out var duration ) )
					foreach (var tweener in _tweenersIn) tweener.duration = duration;
			}

			if ( _tweenersOut != null ) {
				if ( applyOutEase && style.TryGetOutEase( out var ease ) ) 
					foreach (var tweener in _tweenersOut) tweener.ease = ease;
				if ( applyOutEase && style.TryGetOutDuration( out var duration ) ) 
					foreach (var tweener in _tweenersOut) tweener.duration = duration;
			}

			if ( _layoutElement != null ) {
				if ( applyHeight && style.TryGetHeight( out var height ) ) _layoutElement.preferredHeight = height;
			}

			if ( _image != null ) {
				if ( applySprite && style.TryGetSprite( out var sprite ) ) _image.sprite = sprite;
				if ( applyColor && style.TryGetColor( out var color ) ) _image.color = color;
			}

			if ( _text != null ) {
				if ( applyFont && style.TryGetFont( out var font ) ) _text.font = font;
				if ( applyColor && style.TryGetColor( out var color ) ) _text.color = color;
				if ( applyFontSize && style.TryGetFontSize( out var fontSize ) ) _text.fontSize = fontSize;
				if ( applyFontStyle && style.TryGetFontStyle( out var fontStyle ) ) _text.fontStyle = fontStyle;
			}
		}
	}
}