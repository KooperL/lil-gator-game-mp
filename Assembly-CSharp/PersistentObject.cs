using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200021C RID: 540
public class PersistentObject : MonoBehaviour
{
	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00009B44 File Offset: 0x00007D44
	public bool PersistentState
	{
		get
		{
			return SceneSaveData.s.GetPersistentObject(this.id);
		}
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnValidate()
	{
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00009B56 File Offset: 0x00007D56
	public virtual void Load(bool state)
	{
		if (!this.isPersistent)
		{
			return;
		}
		this.isLoaded = true;
		base.gameObject.SetActive(!state);
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0003B4CC File Offset: 0x000396CC
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

	// Token: 0x06000A15 RID: 2581 RVA: 0x0003B51C File Offset: 0x0003971C
	[ContextMenu("Find persistent objects")]
	public void FindPersistentObjects()
	{
		if (!this.isPersistent)
		{
			return;
		}
		SceneSaveData sceneSaveData = Object.FindObjectOfType<SceneSaveData>();
		if (sceneSaveData != null)
		{
			sceneSaveData.FindPersistentObjects();
		}
	}

	// Token: 0x04000C9A RID: 3226
	public bool isPersistent = true;

	// Token: 0x04000C9B RID: 3227
	[ConditionalHide("isPersistent", true)]
	public int id = -1;

	// Token: 0x04000C9C RID: 3228
	public UnityEvent onSaveTrue;

	// Token: 0x04000C9D RID: 3229
	[ReadOnly]
	public bool isLoaded;
}
