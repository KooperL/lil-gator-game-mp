using System;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
	// Token: 0x060007A9 RID: 1961 RVA: 0x00035888 File Offset: 0x00033A88
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

	// Token: 0x060007AA RID: 1962 RVA: 0x00007A27 File Offset: 0x00005C27
	private void OnValidate()
	{
		if (this.colliders == null || this.colliders.Length == 0)
		{
			this.colliders = base.GetComponents<Collider>();
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00035934 File Offset: 0x00033B34
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

	public static Collider[] playerColliders = new Collider[0];

	public Collider[] colliders;

	public bool getCollidersFromParents;

	public bool getCollidersFromChildren;
}
