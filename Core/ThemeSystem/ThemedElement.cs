using System;
using System.Collections.Generic;
using AnimFlex;
using AnimFlex.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace HandyUI.ThemeSystem
{
	/// <summary>Marks the gameObject as a specific element for theme system</summary>
	[DisallowMultipleComponent]
	[AddComponentMenu( "Handy UI/Themed Element" )]
	internal sealed class ThemedElement : MonoBehaviour
	{
		public string styleName;
		[SerializeField] internal bool applySprite = true;
		[SerializeField] internal bool applyColor = true;
		[SerializeField] internal bool applyOutlineColor = true;
		[SerializeField] internal bool applyHeight = true;
		[SerializeField] internal bool applyWidth = false;
		[SerializeField] internal bool applyFont = true;
		[SerializeField] internal bool applyFontSize = true;
		[SerializeField] internal bool applyFontStyle = true;
		
		[SerializeField] internal bool applyPlayInAnim = true;
		[SerializeField] internal bool applyInEase = true;
		[SerializeField] internal bool applyInDuration = true;
		
		[SerializeField] internal bool applyPlayOutAnim = true;
		[SerializeField] internal bool applyOutEase = true;
		[SerializeField] internal bool applyOutDuration = true;

		[SerializeField] internal bool playInAnim = true;
		[SerializeField] internal bool playOutAnim = true;

		private Image _image;
		private TMP_Text _text;
		private Shadow _shadow;
		private Outline _outline;
		private LayoutElement _layoutElement;
		[SerializeField] internal TweenerComponent[] _tweenersIn;
		[SerializeField] internal TweenerComponent[] _tweenersOut;

		private Tweener[] _tweeners;

		private void Awake() {
			_tweeners = new Tweener[_tweenersIn.Length + _tweenersOut.Length];
			getComponents();
		}

		public void PlayInAnim(out Tweener lastTweener, out float lastDuration) {
			if ( !playInAnim ) {
				lastTweener = null;
				lastDuration = 0;
				return;
			}
			
			// kill all tweeners
			for ( var i = 0; i < _tweeners.Length; i++ ) {
				if ( _tweeners[i] != null ) {
					_tweeners[i].Kill( true, false );
				}

				_tweeners[i] = null;
			}

			lastDuration = 0;
			lastTweener = null;
			for ( var i = 0; i < _tweenersIn.Length; i++ ) {
				var t = _tweenersIn[i].PlayOrRestart();
				if ( _tweenersIn[i].duration > lastDuration ) {
					lastDuration = _tweenersIn[i].duration;
					lastTweener = t;
				}

				_tweeners[i] = t;
			}
		}

		public void PlayOutAnim(out Tweener lastTweener, out float lastDuration) {
			if ( !playInAnim ) {
				lastTweener = null;
				lastDuration = 0;
				return;
			}

			// kill all tweeners
			for ( var i = 0; i < _tweeners.Length; i++ ) {
				if ( _tweeners[i] != null ) _tweeners[i].Kill( true, false );
				_tweeners[i] = null;
			}

			lastDuration = 0;
			lastTweener = null;
			for ( var i = 0; i < _tweenersOut.Length; i++ ) {
				var t = _tweenersOut[i].PlayOrRestart();
				if ( _tweenersOut[i].duration > lastDuration ) {
					lastDuration = _tweenersOut[i].duration;
					lastTweener = t;
				}

				_tweeners[i] = t;
			}
		}

		private void getComponents() {
			_image = GetComponent<Image>();
			_text = GetComponent<TMP_Text>();
			_shadow = GetComponent<Shadow>();
			_outline = GetComponent<Outline>();
		}

#if UNITY_EDITOR
		private void OnValidate() {
			Profiler.BeginSample( "ThemedElement OnValidate" );
			getComponents();
			_layoutElement = GetComponent<LayoutElement>();
			var theme = gameObject.GetComponentInParent<Theme>();
			if ( theme != null )
				theme.UpdateTheme();
			Profiler.EndSample();
		}

#endif

		internal void UpdateTheme(Style style) {

			if ( _tweenersIn?.Length > 0 ) {
				if ( applyPlayInAnim && style.TryGetInAnim( out var value ) ) {
					playInAnim = value;
				}
			}
			
			if ( _tweenersOut?.Length > 0 ) {
				if ( applyPlayOutAnim && style.TryGetOutAnim( out var value ) ) {
					playOutAnim = value;
				}
			}
			
			if ( _tweenersIn != null ) {
				if ( applyInEase && style.TryGetInEase( out var ease ) )
					foreach ( var tweener in _tweenersIn )
						tweener.ease = ease;
				if ( applyInEase && style.TryGetInDuration( out var duration ) )
					foreach ( var tweener in _tweenersIn ) { // error without the extra null-check :(
						if ( tweener != null ) tweener.duration = duration;
					}
			}

			if ( _tweenersOut != null ) {
				if ( applyOutEase && style.TryGetOutEase( out var ease ) )
					foreach ( var tweener in _tweenersOut )
						tweener.ease = ease;
				if ( applyOutEase && style.TryGetOutDuration( out var duration ) )
					foreach ( var tweener in _tweenersOut ) {
						if ( tweener != null ) tweener.duration = duration;
					}
			}

			if ( _layoutElement != null ) {
				if ( applyHeight && style.TryGetHeight( out var height ) ) _layoutElement.preferredHeight = height;
				if ( applyWidth && style.TryGetWidth( out var width ) ) _layoutElement.preferredWidth = width;
			}

			if ( _image != null ) {
				if ( applySprite && style.TryGetSprite( out var sprite ) ) _image.sprite = sprite;
				if ( applyColor && style.TryGetColor( out var color ) ) _image.color = color;
			}

			if ( _outline != null ) {
				if ( applyOutlineColor && style.TryGetOutlineColor( out var color ) ) _outline.effectColor = color;
			}
			if ( _shadow != null ) {
				if ( applyOutlineColor && style.TryGetOutlineColor( out var color ) ) _shadow.effectColor = color;
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