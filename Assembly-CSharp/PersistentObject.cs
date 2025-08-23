using System;
using UnityEngine;
using UnityEngine.Events;

public class PersistentObject : MonoBehaviour
{
	// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00009E82 File Offset: 0x00008082
	public bool PersistentState
	{
		get
		{
			return SceneSaveData.s.GetPersistentObject(this.id);
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnValidate()
	{
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00009E94 File Offset: 0x00008094
	public virtual void Load(bool state)
	{
		if (!this.isPersistent)
		{
			return;
		}
		this.isLoaded = true;
		base.gameObject.SetActive(!state);
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0003D240 File Offset: 0x0003B440
	public virtual void SaveTrue()
	{
		if (!this.isPersistent)
		{
			return;
		}
		if (this.id != -1 && SceneSaveData.s != null)
		{
			SceneSaveData.s.SavePersistentObject(this.id, true);
		}
		if (this.onSaveTrue != null)
		{
			this.onSaveTrue.Invoke();
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0003D290 File Offset: 0x0003B490
	[ContextMenu("Find persistent objects")]
	public void FindPersistentObjects()
	{
		if (!this.isPersistent)
		{
			return;
		}
		SceneSaveData sceneSaveData = global::UnityEngine.Object.FindObjectOfType<SceneSaveData>();
		if (sceneSaveData != null)
		{
			sceneSaveData.FindPersistentObjects();
		}
	}

	public bool isPersistent = true;

	[ConditionalHide("isPersistent", true)]
	public int id = -1;

	public UnityEvent onSaveTrue;

	[ReadOnly]
	public bool isLoaded;
}
