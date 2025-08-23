using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkByPersistentObjects : MonoBehaviour
{
	// Token: 0x060008CE RID: 2254 RVA: 0x000089D6 File Offset: 0x00006BD6
	[ContextMenu("Collect Objects")]
	public void CollectObjects()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<PersistentObject>();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00039354 File Offset: 0x00037554
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
