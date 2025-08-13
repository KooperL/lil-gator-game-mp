using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000255 RID: 597
public static class TransformDeepChildExtension
{
	// Token: 0x06000CE9 RID: 3305 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
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
