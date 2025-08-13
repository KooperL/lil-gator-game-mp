using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001C6 RID: 454
public class ForkByPersistentObjects : MonoBehaviour
{
	// Token: 0x0600088D RID: 2189 RVA: 0x000086AF File Offset: 0x000068AF
	[ContextMenu("Collect Objects")]
	public void CollectObjects()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<PersistentObject>();
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0003771C File Offset: 0x0003591C
	public void ForkLogic()
	{
		int num = 0;
		PersistentObject[] array = this.targets;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].PersistentState)
			{
				num++;
			}
		}
		if (num >= this.desiredTargetCount)
		{
			this.atLeastTarget.Invoke();
			return;
		}
		this.lessThanTarget.Invoke();
	}

	// Token: 0x04000B22 RID: 2850
	public Transform parent;

	// Token: 0x04000B23 RID: 2851
	public PersistentObject[] targets;

	// Token: 0x04000B24 RID: 2852
	public int desiredTargetCount;

	// Token: 0x04000B25 RID: 2853
	public UnityEvent lessThanTarget;

	// Token: 0x04000B26 RID: 2854
	public UnityEvent atLeastTarget;
}
