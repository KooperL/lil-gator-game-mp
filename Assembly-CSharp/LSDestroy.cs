using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001CB RID: 459
[AddComponentMenu("Logic/LogicState - Destroy")]
public class LSDestroy : LogicState
{
	// Token: 0x06000896 RID: 2198 RVA: 0x00008757 File Offset: 0x00006957
	[ContextMenu("Collect Breakables")]
	public void CollectBreakables()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00037770 File Offset: 0x00035970
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

	// Token: 0x06000898 RID: 2200 RVA: 0x000377B4 File Offset: 0x000359B4
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

	// Token: 0x04000B2D RID: 2861
	[Space]
	public Transform parent;

	// Token: 0x04000B2E RID: 2862
	public BreakableObject[] targets;

	// Token: 0x04000B2F RID: 2863
	public int desiredUnbrokenTargets;

	// Token: 0x04000B30 RID: 2864
	private int lastAliveTargets = -1;

	// Token: 0x04000B31 RID: 2865
	private bool addedListeners;

	// Token: 0x04000B32 RID: 2866
	public LSDestroy.DestroyEvent[] events;

	// Token: 0x020001CC RID: 460
	[Serializable]
	public struct DestroyEvent
	{
		// Token: 0x04000B33 RID: 2867
		public bool disableOnAwake;

		// Token: 0x04000B34 RID: 2868
		public UnityEvent onReachCount;

		// Token: 0x04000B35 RID: 2869
		public int aliveTargetCount;
	}
}
