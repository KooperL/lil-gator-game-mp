using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031B RID: 795
public static class TransformDeepChildExtension
{
	// Token: 0x06000F96 RID: 3990 RVA: 0x000512D4 File Offset: 0x0004F4D4
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
