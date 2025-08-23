using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	public static class UITools
	{
		// Token: 0x06001CB1 RID: 7345 RVA: 0x00071264 File Offset: 0x0006F464
		public static GameObject InstantiateGUIObject<T>(GameObject prefab, Transform parent, string name) where T : Component
		{
			GameObject gameObject = UITools.InstantiateGUIObject_Pre<T>(prefab, parent, name);
			if (gameObject == null)
			{
				return null;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component == null)
			{
				Debug.LogError(name + " prefab is missing RectTransform component!");
			}
			else
			{
				component.localScale = Vector3.one;
			}
			return gameObject;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000712B4 File Offset: 0x0006F4B4
		public static GameObject InstantiateGUIObject<T>(GameObject prefab, Transform parent, string name, Vector2 pivot, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition) where T : Component
		{
			GameObject gameObject = UITools.InstantiateGUIObject_Pre<T>(prefab, parent, name);
			if (gameObject == null)
			{
				return null;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component == null)
			{
				Debug.LogError(name + " prefab is missing RectTransform component!");
			}
			else
			{
				component.localScale = Vector3.one;
				component.pivot = pivot;
				component.anchorMin = anchorMin;
				component.anchorMax = anchorMax;
				component.anchoredPosition = anchoredPosition;
			}
			return gameObject;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00071324 File Offset: 0x0006F524
		private static GameObject InstantiateGUIObject_Pre<T>(GameObject prefab, Transform parent, string name) where T : Component
		{
			if (prefab == null)
			{
				Debug.LogError(name + " prefab is null!");
				return null;
			}
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(prefab);
			if (!string.IsNullOrEmpty(name))
			{
				gameObject.name = name;
			}
			T component = gameObject.GetComponent<T>();
			if (component == null)
			{
				Debug.LogError(name + " prefab is missing the " + component.GetType().ToString() + " component!");
				return null;
			}
			if (parent != null)
			{
				gameObject.transform.SetParent(parent, false);
			}
			return gameObject;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000713B8 File Offset: 0x0006F5B8
		public static Vector3 GetPointOnRectEdge(RectTransform rectTransform, Vector2 dir)
		{
			if (rectTransform == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			Rect rect = rectTransform.rect;
			dir = rect.center + Vector2.Scale(rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00071438 File Offset: 0x0006F638
		public static Rect GetWorldSpaceRect(RectTransform rt)
		{
			if (rt == null)
			{
				return default(Rect);
			}
			Rect rect = rt.rect;
			Vector2 vector = rt.TransformPoint(new Vector2(rect.xMin, rect.yMin));
			Vector2 vector2 = rt.TransformPoint(new Vector2(rect.xMin, rect.yMax));
			Vector2 vector3 = rt.TransformPoint(new Vector2(rect.xMax, rect.yMin));
			return new Rect(vector.x, vector.y, vector3.x - vector.x, vector2.y - vector.y);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000714F8 File Offset: 0x0006F6F8
		public static Rect TransformRectTo(Transform from, Transform to, Rect rect)
		{
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			if (from != null)
			{
				vector = from.TransformPoint(new Vector2(rect.xMin, rect.yMin));
				vector2 = from.TransformPoint(new Vector2(rect.xMin, rect.yMax));
				vector3 = from.TransformPoint(new Vector2(rect.xMax, rect.yMin));
			}
			else
			{
				vector = new Vector2(rect.xMin, rect.yMin);
				vector2 = new Vector2(rect.xMin, rect.yMax);
				vector3 = new Vector2(rect.xMax, rect.yMin);
			}
			if (to != null)
			{
				vector = to.InverseTransformPoint(vector);
				vector2 = to.InverseTransformPoint(vector2);
				vector3 = to.InverseTransformPoint(vector3);
			}
			return new Rect(vector.x, vector.y, vector3.x - vector.x, vector.y - vector2.y);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00015EE3 File Offset: 0x000140E3
		public static Rect InvertY(Rect rect)
		{
			return new Rect(rect.xMin, rect.yMin, rect.width, -rect.height);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00071604 File Offset: 0x0006F804
		public static void SetInteractable(Selectable selectable, bool state, bool playTransition)
		{
			if (selectable == null)
			{
				return;
			}
			if (playTransition)
			{
				selectable.interactable = state;
				return;
			}
			if (selectable.transition == Selectable.Transition.ColorTint)
			{
				ColorBlock colors = selectable.colors;
				float fadeDuration = colors.fadeDuration;
				colors.fadeDuration = 0f;
				selectable.colors = colors;
				selectable.interactable = state;
				colors.fadeDuration = fadeDuration;
				selectable.colors = colors;
				return;
			}
			selectable.interactable = state;
		}
	}
}
