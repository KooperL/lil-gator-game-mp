using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class IgnorePlayerCollision : MonoBehaviour
{
	// Token: 0x06000769 RID: 1897 RVA: 0x00033F20 File Offset: 0x00032120
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

	// Token: 0x0600076A RID: 1898 RVA: 0x00007718 File Offset: 0x00005918
	private void OnValidate()
	{
		if (this.colliders == null || this.colliders.Length == 0)
		{
			this.colliders = base.GetComponents<Collider>();
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00033FCC File Offset: 0x000321CC
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

	// Token: 0x040009D8 RID: 2520
	public static Collider[] playerColliders = new Collider[0];

	// Token: 0x040009D9 RID: 2521
	public Collider[] colliders;

	// Token: 0x040009DA RID: 2522
	public bool getCollidersFromParents;

	// Token: 0x040009DB RID: 2523
	public bool getCollidersFromChildren;
}
