using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UnityExtensions
{
	// Token: 0x0600085D RID: 2141 RVA: 0x00027B94 File Offset: 0x00025D94
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T t;
		if (gameObject.TryGetComponent<T>(out t))
		{
			return t;
		}
		return gameObject.AddComponent<T>();
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x00027BB3 File Offset: 0x00025DB3
	public static bool Contains(this LayerMask mask, int layer)
	{
		return mask == (mask | (1 << layer));
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x00027BCA File Offset: 0x00025DCA
	public static Color SetAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x00027BE4 File Offset: 0x00025DE4
	public static Transform ApplyTransform(this Transform transform, Transform sourceTransform)
	{
		transform.position = sourceTransform.position;
		transform.rotation = sourceTransform.rotation;
		return transform;
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x00027BFF File Offset: 0x00025DFF
	public static Transform ApplyTransformLocal(this Transform transform, Transform sourceTransform)
	{
		transform.localPosition = sourceTransform.localPosition;
		transform.localRotation = sourceTransform.localRotation;
		return transform;
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x00027C1A File Offset: 0x00025E1A
	public static Transform ApplyParent(this Transform transform, Transform newParent)
	{
		transform.parent = newParent;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		return transform;
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00027C48 File Offset: 0x00025E48
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

	// Token: 0x06000864 RID: 2148 RVA: 0x00027C80 File Offset: 0x00025E80
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

	// Token: 0x06000865 RID: 2149 RVA: 0x00027CB1 File Offset: 0x00025EB1
	public static Quaternion Inverse(this Quaternion quaternion)
	{
		return Quaternion.Inverse(quaternion);
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x00027CBC File Offset: 0x00025EBC
	public static void ApplyState(this Animator animator, string state)
	{
		ActorState actorState;
		if (Enum.TryParse<ActorState>(state, out actorState))
		{
			animator.ApplyState((int)actorState);
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x00027CDA File Offset: 0x00025EDA
	public static void ApplyState(this Animator animator, int state)
	{
		animator.SetInteger(UnityExtensions.stateID, state);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x00027CE8 File Offset: 0x00025EE8
	public static void ApplyPosition(this Animator animator, string position)
	{
		ActorPosition actorPosition;
		if (Enum.TryParse<ActorPosition>(position, out actorPosition))
		{
			animator.ApplyPosition((int)actorPosition);
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x00027D06 File Offset: 0x00025F06
	public static void ApplyPosition(this Animator animator, int position)
	{
		animator.SetInteger(UnityExtensions.positionID, position);
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x00027D14 File Offset: 0x00025F14
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

	// Token: 0x0600086B RID: 2155 RVA: 0x00027D94 File Offset: 0x00025F94
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

	// Token: 0x0600086C RID: 2156 RVA: 0x00027DC8 File Offset: 0x00025FC8
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

	// Token: 0x0600086D RID: 2157 RVA: 0x00027DFE File Offset: 0x00025FFE
	public static T[] Remove<T>(this T[] array, T elementToRemove)
	{
		List<T> list = new List<T>(array);
		list.Remove(elementToRemove);
		array = list.ToArray();
		return array;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00027E16 File Offset: 0x00026016
	public static T[] Add<T>(this T[] array, T newElement)
	{
		Array.Resize<T>(ref array, array.Length + 1);
		array[array.Length - 1] = newElement;
		return array;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x00027E34 File Offset: 0x00026034
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

	// Token: 0x06000870 RID: 2160 RVA: 0x00027E6E File Offset: 0x0002606E
	public static Vector4 ToVector4(this Quaternion quaternion)
	{
		return new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00027E8D File Offset: 0x0002608D
	public static Quaternion ToQuaternion(this Vector4 vector4)
	{
		return new Quaternion(vector4.x, vector4.y, vector4.z, vector4.w);
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00027EAC File Offset: 0x000260AC
	public static Vector2Int PixelSize(this Resolution res)
	{
		return new Vector2Int(res.width, res.height);
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x00027EC1 File Offset: 0x000260C1
	public static Vector3 Flat(this Vector3 vector3)
	{
		vector3.y = 0f;
		return vector3;
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00027ED0 File Offset: 0x000260D0
	public static bool IsNaN(this Vector3 vector3)
	{
		return float.IsNaN(vector3.x) || float.IsPositiveInfinity(vector3.x) || float.IsNegativeInfinity(vector3.x) || (float.IsNaN(vector3.y) || float.IsPositiveInfinity(vector3.y) || float.IsNegativeInfinity(vector3.y)) || (float.IsNaN(vector3.z) || float.IsPositiveInfinity(vector3.z) || float.IsNegativeInfinity(vector3.z));
	}

	public static int stateID = Animator.StringToHash("State");

	public static int positionID = Animator.StringToHash("Position");

	private static int lastRandomIndex = -1;
}
