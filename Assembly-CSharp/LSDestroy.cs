using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200015F RID: 351
[AddComponentMenu("Logic/LogicState - Destroy")]
public class LSDestroy : LogicState
{
	// Token: 0x0600074A RID: 1866 RVA: 0x0002455B File Offset: 0x0002275B
	[ContextMenu("Collect Breakables")]
	public void CollectBreakables()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00024588 File Offset: 0x00022788
	public override void Start()
	{
		if (!this.addedListeners)
		{
			BreakableObject[] array = this.targets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onBreak.AddListener(new UnityAction(this.CheckLogic));
			}
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x000245CC File Offset: 0x000227CC
	public override void CheckLogic()
	{
		if (!base.enabled)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < this.targets.Length; i++)
		{
			if (!this.targets[i].IsBroken)
			{
				num++;
			}
		}
		if (this.lastAliveTargets != num)
		{
			foreach (LSDestroy.DestroyEvent destroyEvent in this.events)
			{
				if ((!destroyEvent.disableOnAwake || this.lastAliveTargets != -1) && destroyEvent.aliveTargetCount == num)
				{
					destroyEvent.onReachCount.Invoke();
				}
			}
		}
		if (num <= this.desiredUnbrokenTargets)
		{
			this.LogicCompleted();
		}
		this.lastAliveTargets = num;
	}

	// Token: 0x0400098C RID: 2444
	[Space]
	public Transform parent;

	// Token: 0x0400098D RID: 2445
	public BreakableObject[] targets;

	// Token: 0x0400098E RID: 2446
	public int desiredUnbrokenTargets;

	// Token: 0x0400098F RID: 2447
	private int lastAliveTargets = -1;

	// Token: 0x04000990 RID: 2448
	private bool addedListeners;

	// Token: 0x04000991 RID: 2449
	public LSDestroy.DestroyEvent[] events;

	// Token: 0x020003C3 RID: 963
	[Serializable]
	public struct DestroyEvent
	{
		// Token: 0x04001BBE RID: 7102
		public bool disableOnAwake;

		// Token: 0x04001BBF RID: 7103
		public UnityEvent onReachCount;

		// Token: 0x04001BC0 RID: 7104
		public int aliveTargetCount;
	}
}
