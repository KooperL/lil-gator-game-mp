using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000162 RID: 354
[AddComponentMenu("Logic/LogicState - Persistent Objects")]
public class LSPersistentObjects : LogicState
{
	// Token: 0x06000752 RID: 1874 RVA: 0x00024707 File Offset: 0x00022907
	[ContextMenu("Collect Objects")]
	public void CollectObjects()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<PersistentObject>();
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00024734 File Offset: 0x00022934
	public override void Start()
	{
		if (!this.addedListeners)
		{
			PersistentObject[] array = this.targets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onSaveTrue.AddListener(new UnityAction(this.CheckLogic));
			}
		}
		base.Start();
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00024780 File Offset: 0x00022980
	public override void CheckLogic()
	{
		if (!base.enabled)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < this.targets.Length; i++)
		{
			if (this.targets[i].PersistentState)
			{
				num++;
			}
		}
		if (this.lastTargets != num)
		{
			foreach (LSPersistentObjects.DestroyEvent destroyEvent in this.events)
			{
				if (destroyEvent.disableOnAwake && this.lastTargets != -1 && destroyEvent.targetCount == num)
				{
					destroyEvent.onReachCount.Invoke();
				}
			}
		}
		if (num >= this.desiredTargets)
		{
			this.LogicCompleted();
		}
		this.lastTargets = num;
	}

	// Token: 0x04000993 RID: 2451
	[Space]
	public Transform parent;

	// Token: 0x04000994 RID: 2452
	public PersistentObject[] targets;

	// Token: 0x04000995 RID: 2453
	public int desiredTargets;

	// Token: 0x04000996 RID: 2454
	private int lastTargets = -1;

	// Token: 0x04000997 RID: 2455
	private bool addedListeners;

	// Token: 0x04000998 RID: 2456
	public LSPersistentObjects.DestroyEvent[] events;

	// Token: 0x020003C5 RID: 965
	[Serializable]
	public struct DestroyEvent
	{
		// Token: 0x04001BC4 RID: 7108
		public bool disableOnAwake;

		// Token: 0x04001BC5 RID: 7109
		public UnityEvent onReachCount;

		// Token: 0x04001BC6 RID: 7110
		public int targetCount;
	}
}
