using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions
{
	// Token: 0x06000A28 RID: 2600 RVA: 0x0003C55C File Offset: 0x0003A75C
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T t;
		if (gameObject.TryGetComponent<T>(out t))
		{
			return t;
		}
		return gameObject.AddComponent<T>();
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00009BC6 File Offset: 0x00007DC6
	public static bool Contains(this LayerMask mask, int layer)
	{
		return mask == (mask | (1 << layer));
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00009BDD File Offset: 0x00007DDD
	public static Color SetAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00009BF7 File Offset: 0x00007DF7
	public static Transform ApplyTransform(this Transform transform, Transform sourceTransform)
	{
		transform.position = sourceTransform.position;
		transform.rotation = sourceTransform.rotation;
		return transform;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00009C12 File Offset: 0x00007E12
	public static Transform ApplyTransformLocal(this Transform transform, Transform sourceTransform)
	{
		transform.localPosition = sourceTransform.localPosition;
		transform.localRotation = sourceTransform.localRotation;
		return transform;
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00009C2D File Offset: 0x00007E2D
	public static Transform ApplyParent(this Transform transform, Transform newParent)
	{
		transform.parent = newParent;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		return transform;
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0003C57C File Offset: 0x0003A77C
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

	// Token: 0x06000A2F RID: 2607 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
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

	// Token: 0x06000A30 RID: 2608 RVA: 0x00009C58 File Offset: 0x00007E58
	public static Quaternion Inverse(this Quaternion quaternion)
	{
		return Quaternion.Inverse(quaternion);
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
	public static void ApplyState(this Animator animator, string state)
	{
		ActorState actorState;
		if (Enum.TryParse<ActorState>(state, out actorState))
		{
			animator.ApplyState((int)actorState);
		}
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00009C60 File Offset: 0x00007E60
	public static void ApplyState(this Animator animator, int state)
	{
		animator.SetInteger(UnityExtensions.stateID, state);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0003C608 File Offset: 0x0003A808
	public static void ApplyPosition(this Animator animator, string position)
	{
		ActorPosition actorPosition;
		if (Enum.TryParse<ActorPosition>(position, out actorPosition))
		{
			animator.ApplyPosition((int)actorPosition);
		}
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00009C6E File Offset: 0x00007E6E
	public static void ApplyPosition(this Animator animator, int position)
	{
		animator.SetInteger(UnityExtensions.positionID, position);
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0003C628 File Offset: 0x0003A828
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

	// Token: 0x06000A36 RID: 2614 RVA: 0x0003C6A8 File Offset: 0x0003A8A8
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

	// Token: 0x06000A37 RID: 2615 RVA: 0x0003C6DC File Offset: 0x0003A8DC
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

	// Token: 0x06000A38 RID: 2616 RVA: 0x00009C7C File Offset: 0x00007E7C
	public static T[] Remove<T>(this T[] array, T elementToRemove)
	{
		List<T> list = new List<T>(array);
		list.Remove(elementToRemove);
		array = list.ToArray();
		return array;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00009C94 File Offset: 0x00007E94
	public static T[] Add<T>(this T[] array, T newElement)
	{
		Array.Resize<T>(ref array, array.Length + 1);
		array[array.Length - 1] = newElement;
		return array;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x0003C714 File Offset: 0x0003A914
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

	// Token: 0x06000A3B RID: 2619 RVA: 0x00009CAF File Offset: 0x00007EAF
	public static Vector4 ToVector4(this Quaternion quaternion)
	{
		return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00009CCE File Offset: 0x00007ECE
	public static Quaternion ToQuaternion(this Vector4 vector4)
	{
		return new Quaternion(vector4.x, vector4.y, vector4.z, vector4.w);
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00009CED File Offset: 0x00007EED
	public static Vector2Int PixelSize(this Resolution res)
	{
		return new Vector2Int(res.width, res.height);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00009D02 File Offset: 0x00007F02
	public static Vector3 Flat(this Vector3 vector3)
	{
		vector3.y = 0f;
		return vector3;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0003C750 File Offset: 0x0003A950
	public static bool IsNaN(this Vector3 vector3)
	{
		return float.IsNaN(vector3.x) || float.IsPositiveInfinity(vector3.x) || float.IsNegativeInfinity(vector3.x) || (float.IsNaN(vector3.y) || float.IsPositiveInfinity(vector3.y) || float.IsNegativeInfinity(vector3.y)) || (float.IsNaN(vector3.z) || float.IsPositiveInfinity(vector3.z) || float.IsNegativeInfinity(vector3.z));
	}

	public static int stateID = Animator.StringToHash("State");

	public static int positionID = Animator.StringToHash("Position");

	private static int lastRandomIndex = -1;
}
