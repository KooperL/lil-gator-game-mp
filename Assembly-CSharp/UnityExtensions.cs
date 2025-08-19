using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions
{
	// Token: 0x06000A28 RID: 2600 RVA: 0x0003C538 File Offset: 0x0003A738
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T t;
		if (gameObject.TryGetComponent<T>(out t))
		{
			return t;
		}
		return gameObject.AddComponent<T>();
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00009BD0 File Offset: 0x00007DD0
	public static bool Contains(this LayerMask mask, int layer)
	{
		return mask == (mask | (1 << layer));
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00009BE7 File Offset: 0x00007DE7
	public static Color SetAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00009C01 File Offset: 0x00007E01
	public static Transform ApplyTransform(this Transform transform, Transform sourceTransform)
	{
		transform.position = sourceTransform.position;
		transform.rotation = sourceTransform.rotation;
		return transform;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00009C1C File Offset: 0x00007E1C
	public static Transform ApplyTransformLocal(this Transform transform, Transform sourceTransform)
	{
		transform.localPosition = sourceTransform.localPosition;
		transform.localRotation = sourceTransform.localRotation;
		return transform;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00009C37 File Offset: 0x00007E37
	public static Transform ApplyParent(this Transform transform, Transform newParent)
	{
		transform.parent = newParent;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		return transform;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0003C558 File Offset: 0x0003A758
	public static T GetComponentUpHeirarchy<T>(this Transform transform) where T : Component
	{
		while (transform != null)
		{
			T t;
			if (transform.TryGetComponent<T>(out t))
			{
				return t;
			}
			transform = transform.parent;
		}
		return default(T);
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0003C590 File Offset: 0x0003A790
	public static int GetDepth(this Transform transform)
	{
		int num = 0;
		while (transform.parent != null && num < 100)
		{
			num++;
			transform = transform.parent;
		}
		return num;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x00009C62 File Offset: 0x00007E62
	public static Quaternion Inverse(this Quaternion quaternion)
	{
		return Quaternion.Inverse(quaternion);
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003C5C4 File Offset: 0x0003A7C4
	public static void ApplyState(this Animator animator, string state)
	{
		ActorState actorState;
		if (Enum.TryParse<ActorState>(state, out actorState))
		{
			animator.ApplyState((int)actorState);
		}
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00009C6A File Offset: 0x00007E6A
	public static void ApplyState(this Animator animator, int state)
	{
		animator.SetInteger(UnityExtensions.stateID, state);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
	public static void ApplyPosition(this Animator animator, string position)
	{
		ActorPosition actorPosition;
		if (Enum.TryParse<ActorPosition>(position, out actorPosition))
		{
			animator.ApplyPosition((int)actorPosition);
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00009C78 File Offset: 0x00007E78
	public static void ApplyPosition(this Animator animator, int position)
	{
		animator.SetInteger(UnityExtensions.positionID, position);
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0003C604 File Offset: 0x0003A804
	public static bool TryFindIndex<T>(this IEnumerable<T> array, T target, out int index)
	{
		index = 0;
		if (array == null)
		{
			return false;
		}
		foreach (T t in array)
		{
			if (target == null)
			{
				if (t == null)
				{
					return true;
				}
			}
			else if (t.Equals(target))
			{
				return true;
			}
			index++;
		}
		return false;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0003C684 File Offset: 0x0003A884
	public static bool HasPersistentTarget(this UnityEvent unityEvent, global::UnityEngine.Object target)
	{
		int persistentEventCount = unityEvent.GetPersistentEventCount();
		for (int i = 0; i < persistentEventCount; i++)
		{
			if (unityEvent.GetPersistentTarget(i) == target)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0003C6B8 File Offset: 0x0003A8B8
	public static bool Contains<T>(this T[] array, T target)
	{
		foreach (T t in array)
		{
			if (EqualityComparer<T>.Default.Equals(t, target))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00009C86 File Offset: 0x00007E86
	public static T[] Remove<T>(this T[] array, T elementToRemove)
	{
		List<T> list = new List<T>(array);
		list.Remove(elementToRemove);
		array = list.ToArray();
		return array;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00009C9E File Offset: 0x00007E9E
	public static T[] Add<T>(this T[] array, T newElement)
	{
		Array.Resize<T>(ref array, array.Length + 1);
		array[array.Length - 1] = newElement;
		return array;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
	public static T RandomValue<T>(this T[] array)
	{
		int num = Mathf.FloorToInt(global::UnityEngine.Random.value * (float)array.Length);
		if (num == UnityExtensions.lastRandomIndex)
		{
			num = (num + 1) % array.Length;
		}
		UnityExtensions.lastRandomIndex = num;
		return array[num];
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00009CB9 File Offset: 0x00007EB9
	public static Vector4 ToVector4(this Quaternion quaternion)
	{
		return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00009CD8 File Offset: 0x00007ED8
	public static Quaternion ToQuaternion(this Vector4 vector4)
	{
		return new Quaternion(vector4.x, vector4.y, vector4.z, vector4.w);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00009CF7 File Offset: 0x00007EF7
	public static Vector2Int PixelSize(this Resolution res)
	{
		return new Vector2Int(res.width, res.height);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00009D0C File Offset: 0x00007F0C
	public static Vector3 Flat(this Vector3 vector3)
	{
		vector3.y = 0f;
		return vector3;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0003C72C File Offset: 0x0003A92C
	public static bool IsNaN(this Vector3 vector3)
	{
		return float.IsNaN(vector3.x) || float.IsPositiveInfinity(vector3.x) || float.IsNegativeInfinity(vector3.x) || (float.IsNaN(vector3.y) || float.IsPositiveInfinity(vector3.y) || float.IsNegativeInfinity(vector3.y)) || (float.IsNaN(vector3.z) || float.IsPositiveInfinity(vector3.z) || float.IsNegativeInfinity(vector3.z));
	}

	public static int stateID = Animator.StringToHash("State");

	public static int positionID = Animator.StringToHash("Position");

	private static int lastRandomIndex = -1;
}
