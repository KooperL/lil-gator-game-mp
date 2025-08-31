using System;
using UnityEngine;
using UnityEngine.Events;

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

	[Space]
	public Transform parent;

	public BreakableObject[] targets;

	public int desiredUnbrokenTargets;

	private int lastAliveTargets = -1;

	private bool addedListeners;

	public LSDestroy.DestroyEvent[] events;

	[Serializable]
	public struct DestroyEvent
	{
		public bool disableOnAwake;

		public UnityEvent onReachCount;

		public int aliveTargetCount;
	}
}
