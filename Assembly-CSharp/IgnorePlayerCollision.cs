using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class IgnorePlayerCollision : MonoBehaviour
{
	// Token: 0x06000644 RID: 1604 RVA: 0x000205A0 File Offset: 0x0001E7A0
	public static void IgnoreCollider(Collider collider)
	{
		List<Collider> list = new List<Collider>();
		foreach (Collider collider2 in IgnorePlayerCollision.playerColliders)
		{
			if (collider2 != null)
			{
				Physics.IgnoreCollision(collider, collider2, true);
			}
			else
			{
				list.Add(collider2);
			}
		}
		if (list.Count > 0)
		{
			List<Collider> list2 = new List<Collider>(IgnorePlayerCollision.playerColliders);
			foreach (Collider collider3 in list)
			{
				list2.Remove(collider3);
			}
			IgnorePlayerCollision.playerColliders = list2.ToArray();
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0002064C File Offset: 0x0001E84C
	private void OnValidate()
	{
		if (this.colliders == null || this.colliders.Length == 0)
		{
			this.colliders = base.GetComponents<Collider>();
		}
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0002066C File Offset: 0x0001E86C
	public void Start()
	{
		Collider[] array = this.colliders;
		for (int i = 0; i < array.Length; i++)
		{
			IgnorePlayerCollision.IgnoreCollider(array[i]);
		}
		if (this.getCollidersFromParents)
		{
			array = base.GetComponentsInParent<Collider>();
			for (int i = 0; i < array.Length; i++)
			{
				IgnorePlayerCollision.IgnoreCollider(array[i]);
			}
		}
		if (this.getCollidersFromChildren)
		{
			array = base.GetComponentsInChildren<Collider>();
			for (int i = 0; i < array.Length; i++)
			{
				IgnorePlayerCollision.IgnoreCollider(array[i]);
			}
		}
	}

	// Token: 0x0400086E RID: 2158
	public static Collider[] playerColliders = new Collider[0];

	// Token: 0x0400086F RID: 2159
	public Collider[] colliders;

	// Token: 0x04000870 RID: 2160
	public bool getCollidersFromParents;

	// Token: 0x04000871 RID: 2161
	public bool getCollidersFromChildren;
}
