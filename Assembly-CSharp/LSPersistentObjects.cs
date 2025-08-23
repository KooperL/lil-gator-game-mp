using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Persistent Objects")]
public class LSPersistentObjects : LogicState
{
	// Token: 0x060008E5 RID: 2277 RVA: 0x00008AF4 File Offset: 0x00006CF4
	[ContextMenu("Collect Objects")]
	public void CollectObjects()
	{
		if (this.parent == null)
		{
			this.parent = base.transform;
		}
		this.targets = this.parent.GetComponentsInChildren<PersistentObject>();
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0003956C File Offset: 0x0003776C
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

	// Token: 0x060008E7 RID: 2279 RVA: 0x000395B8 File Offset: 0x000377B8
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

	[Space]
	public Transform parent;

	public PersistentObject[] targets;

	public int desiredTargets;

	private int lastTargets = -1;

	private bool addedListeners;

	public LSPersistentObjects.DestroyEvent[] events;

	[Serializable]
	public struct DestroyEvent
	{
		public bool disableOnAwake;

		public UnityEvent onReachCount;

		public int targetCount;
	}
}
