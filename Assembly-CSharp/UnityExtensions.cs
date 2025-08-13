using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000213 RID: 531
public static class UnityExtensions
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x0003AAB4 File Offset: 0x00038CB4
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T t;
		if (gameObject.TryGetComponent<T>(ref t))
		{
			return t;
		}
		return gameObject.AddComponent<T>();
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00009892 File Offset: 0x00007A92
	public static bool Contains(this LayerMask mask, int layer)
	{
		return mask == (mask | (1 << layer));
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x000098A9 File Offset: 0x00007AA9
	public static Color SetAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x000098C3 File Offset: 0x00007AC3
	public static Transform ApplyTransform(this Transform transform, Transform sourceTransform)
	{
		transform.position = sourceTransform.position;
		transform.rotation = sourceTransform.rotation;
		return transform;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000098DE File Offset: 0x00007ADE
	public static Transform ApplyTransformLocal(this Transform transform, Transform sourceTransform)
	{
		transform.localPosition = sourceTransform.localPosition;
		transform.localRotation = sourceTransform.localRotation;
		return transform;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x000098F9 File Offset: 0x00007AF9
	public static Transform ApplyParent(this Transform transform, Transform newParent)
	{
		transform.parent = newParent;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		return transform;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0003AAD4 File Offset: 0x00038CD4
	public static T GetComponentUpHeirarchy<T>(this Transform transform) where T : Component
	{
		while (transform != null)
		{
			T t;
			if (transform.TryGetComponent<T>(ref t))
			{
				return t;
			}
			transform = transform.parent;
		}
		return default(T);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0003AB0C File Offset: 0x00038D0C
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

	// Token: 0x060009E8 RID: 2536 RVA: 0x00009924 File Offset: 0x00007B24
	public static Quaternion Inverse(this Quaternion quaternion)
	{
		return Quaternion.Inverse(quaternion);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0003AB40 File Offset: 0x00038D40
	public static void ApplyState(this Animator animator, string state)
	{
		ActorState actorState;
		if (Enum.TryParse<ActorState>(state, out actorState))
		{
			animator.ApplyState((int)actorState);
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0000992C File Offset: 0x00007B2C
	public static void ApplyState(this Animator animator, int state)
	{
		animator.SetInteger(UnityExtensions.stateID, state);
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0003AB60 File Offset: 0x00038D60
	public static void ApplyPosition(this Animator animator, string position)
	{
		ActorPosition actorPosition;
		if (Enum.TryParse<ActorPosition>(position, out actorPosition))
		{
			animator.ApplyPosition((int)actorPosition);
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0000993A File Offset: 0x00007B3A
	public static void ApplyPosition(this Animator animator, int position)
	{
		animator.SetInteger(UnityExtensions.positionID, position);
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0003AB80 File Offset: 0x00038D80
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

	// Token: 0x060009EE RID: 2542 RVA: 0x0003AC00 File Offset: 0x00038E00
	public static bool HasPersistentTarget(this UnityEvent unityEvent, Object target)
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

	// Token: 0x060009EF RID: 2543 RVA: 0x0003AC34 File Offset: 0x00038E34
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

	// Token: 0x060009F0 RID: 2544 RVA: 0x00009948 File Offset: 0x00007B48
	public static T[] Remove<T>(this T[] array, T elementToRemove)
	{
		List<T> list = new List<T>(array);
		list.Remove(elementToRemove);
		array = list.ToArray();
		return array;
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x00009960 File Offset: 0x00007B60
	public static T[] Add<T>(this T[] array, T newElement)
	{
		Array.Resize<T>(ref array, array.Length + 1);
		array[array.Length - 1] = newElement;
		return array;
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0003AC6C File Offset: 0x00038E6C
	public static T RandomValue<T>(this T[] array)
	{
		int num = Mathf.FloorToInt(Random.value * (float)array.Length);
		if (num == UnityExtensions.lastRandomIndex)
		{
			num = (num + 1) % array.Length;
		}
		UnityExtensions.lastRandomIndex = num;
		return array[num];
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0000997B File Offset: 0x00007B7B
	public static Vector4 ToVector4(this Quaternion quaternion)
	{
		return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0000999A File Offset: 0x00007B9A
	public static Quaternion ToQuaternion(this Vector4 vector4)
	{
		return new Quaternion(vector4.x, vector4.y, vector4.z, vector4.w);
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x000099B9 File Offset: 0x00007BB9
	public static Vector2Int PixelSize(this Resolution res)
	{
		return new Vector2Int(res.width, res.height);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x000099CE File Offset: 0x00007BCE
	public static Vector3 Flat(this Vector3 vector3)
	{
		vector3.y = 0f;
		return vector3;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0003ACA8 File Offset: 0x00038EA8
	public static bool IsNaN(this Vector3 vector3)
	{
		return float.IsNaN(vector3.x) || float.IsPositiveInfinity(vector3.x) || float.IsNegativeInfinity(vector3.x) || (float.IsNaN(vector3.y) || float.IsPositiveInfinity(vector3.y) || float.IsNegativeInfinity(vector3.y)) || (float.IsNaN(vector3.z) || float.IsPositiveInfinity(vector3.z) || float.IsNegativeInfinity(vector3.z));
	}

	// Token: 0x04000C7D RID: 3197
	public static int stateID = Animator.StringToHash("State");

	// Token: 0x04000C7E RID: 3198
	public static int positionID = Animator.StringToHash("Position");

	// Token: 0x04000C7F RID: 3199
	private static int lastRandomIndex = -1;
}
