using System;
using System.Collections.Generic;
using UnityEngine;

public static class TransformDeepChildExtension
{
	// Token: 0x06000FF2 RID: 4082 RVA: 0x000534C0 File Offset: 0x000516C0
	public static Transform FindDeepChild(this Transform aParent, string aName)
	{
		Queue<Transform> queue = new Queue<Transform>();
		queue.Enqueue(aParent);
		while (queue.Count > 0)
		{
			Transform transform = queue.Dequeue();
			if (transform.name == aName)
			{
				return transform;
			}
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				queue.Enqueue(transform2);
			}
		}
		return null;
	}
}
