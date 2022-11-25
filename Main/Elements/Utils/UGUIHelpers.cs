using System;
using UnityEngine;

namespace UIFlex.Elements.Utils
{
	internal static class UGUIHelpers
	{
		/// <summary>
		/// Sets a rect transform's properties, given a set of easier-to-handle arguments 
		/// </summary>
		public static void SetRectTransformProps(RectTransform trans, Alignment alignment, Vector2 size, float pad)
		{
			switch (alignment)
			{
				case Alignment.Fill:
					trans.anchorMin = Vector2.zero;
					trans.anchorMax = Vector2.one;
					trans.pivot = new Vector2(0.5f, 0.5f);
					trans.anchoredPosition = Vector2.zero;
					trans.sizeDelta = -Vector2.one * pad;
					break;
				case Alignment.TopLeft:
					trans.anchorMax = trans.anchorMin = new Vector2(0, 1);
					trans.pivot = new Vector2(0, 1);
					trans.sizeDelta = size;
					trans.anchoredPosition = new Vector2(pad, -pad);
					break;
				case Alignment.TopCenter:
					trans.anchorMax = trans.anchorMin = new Vector2(0.5f, 1);
					trans.pivot = new Vector2(0.5f, 1);
					trans.sizeDelta = size;
					trans.anchoredPosition = new Vector2(0, -pad);
					break;
				case Alignment.TopRight:
					trans.anchorMax = trans.anchorMin = new Vector2(1, 1);
					trans.pivot = new Vector2(1, 1);
					trans.anchoredPosition = new Vector2(-pad, -pad);
					trans.sizeDelta = size;
					break;
				case Alignment.MidLeft:
					trans.anchorMax = trans.anchorMin = new Vector2(0, 0.5f);
					trans.pivot = new Vector2(0, 0.5f);
					trans.anchoredPosition = new Vector2(pad, 0);
					trans.sizeDelta = size;
					break;
				case Alignment.MidCenter:
					trans.anchorMax = trans.anchorMin = new Vector2(0.5f, 0.5f);
					trans.pivot = new Vector2(0.5f, 0.5f);
					trans.anchoredPosition = new Vector2(0, 0);
					trans.sizeDelta = size;
					break;
				case Alignment.MidRight:
					trans.anchorMax = trans.anchorMin = new Vector2(1, 0.5f);
					trans.pivot = new Vector2(1, 0.5f);
					trans.anchoredPosition = new Vector2(-pad, 0);
					trans.sizeDelta = size;
					break;
				case Alignment.BottomLeft:
					trans.anchorMax = trans.anchorMin = new Vector2(0, 0);
					trans.pivot = new Vector2(0, 0);
					trans.anchoredPosition = new Vector2(pad, pad);
					trans.sizeDelta = size;
					break;
				case Alignment.BottomCenter:
					trans.anchorMax = trans.anchorMin = new Vector2(0.5f, 0);
					trans.pivot = new Vector2(0.5f, 0);
					trans.anchoredPosition = new Vector2(0, pad);
					trans.sizeDelta = size;
					break;
				case Alignment.BottomRight:
					trans.anchorMax = trans.anchorMin = new Vector2(1, 0);
					trans.pivot = new Vector2(1, 0);
					trans.anchoredPosition = new Vector2(-pad, pad);
					trans.sizeDelta = size;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
			}
		}
	}
}