using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200015C RID: 348
public class ForkByPersistentObjects : MonoBehaviour
{
	// Token: 0x06000743 RID: 1859 RVA: 0x00024454 File Offset: 0x00022654
	[ContextMenu("Collect Objects")]
	public void CollectObjects()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<PersistentObject>();
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00024484 File Offset: 0x00022684
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

	// Token: 0x04000982 RID: 2434
	public Transform parent;

	// Token: 0x04000983 RID: 2435
	public PersistentObject[] targets;

	// Token: 0x04000984 RID: 2436
	public int desiredTargetCount;

	// Token: 0x04000985 RID: 2437
	public UnityEvent lessThanTarget;

	// Token: 0x04000986 RID: 2438
	public UnityEvent atLeastTarget;
}
