using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Destroy")]
public class LSDestroy : LogicState
{
	// Token: 0x060008D7 RID: 2263 RVA: 0x00008A8A File Offset: 0x00006C8A
	[ContextMenu("Collect Breakables")]
	public void CollectBreakables()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x000393A8 File Offset: 0x000375A8
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

	// Token: 0x060008D9 RID: 2265 RVA: 0x000393EC File Offset: 0x000375EC
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
