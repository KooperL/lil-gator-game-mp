using System;
using UnityEngine;
using UnityEngine.Events;

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

	public Transform parent;

	public PersistentObject[] targets;

	public int desiredTargetCount;

	public UnityEvent lessThanTarget;

	public UnityEvent atLeastTarget;
}
