using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001D0 RID: 464
[AddComponentMenu("Logic/LogicState - Persistent Objects")]
public class LSPersistentObjects : LogicState
{
	// Token: 0x060008A4 RID: 2212 RVA: 0x000087C1 File Offset: 0x000069C1
	[ContextMenu("Collect Objects")]
	public void CollectObjects()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<PersistentObject>();
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00037934 File Offset: 0x00035B34
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

	// Token: 0x060008A6 RID: 2214 RVA: 0x00037980 File Offset: 0x00035B80
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

	// Token: 0x04000B3A RID: 2874
	[Space]
	public Transform parent;

	// Token: 0x04000B3B RID: 2875
	public PersistentObject[] targets;

	// Token: 0x04000B3C RID: 2876
	public int desiredTargets;

	// Token: 0x04000B3D RID: 2877
	private int lastTargets = -1;

	// Token: 0x04000B3E RID: 2878
	private bool addedListeners;

	// Token: 0x04000B3F RID: 2879
	public LSPersistentObjects.DestroyEvent[] events;

	// Token: 0x020001D1 RID: 465
	[Serializable]
	public struct DestroyEvent
	{
		// Token: 0x04000B40 RID: 2880
		public bool disableOnAwake;

		// Token: 0x04000B41 RID: 2881
		public UnityEvent onReachCount;

		// Token: 0x04000B42 RID: 2882
		public int targetCount;
	}
}
