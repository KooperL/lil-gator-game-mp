using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001A2 RID: 418
public class PersistentObject : MonoBehaviour
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000890 RID: 2192 RVA: 0x00028886 File Offset: 0x00026A86
	public bool PersistentState
	{
		get
		{
			return SceneSaveData.s.GetPersistentObject(this.id);
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00028898 File Offset: 0x00026A98
	public virtual void OnValidate()
	{
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0002889A File Offset: 0x00026A9A
	public virtual void Load(bool state)
	{
		if (!this.isPersistent)
		{
			return;
		}
		this.isLoaded = true;
		base.gameObject.SetActive(!state);
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x000288BC File Offset: 0x00026ABC
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

	// Token: 0x06000894 RID: 2196 RVA: 0x0002890C File Offset: 0x00026B0C
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

	// Token: 0x04000A9A RID: 2714
	public bool isPersistent = true;

	// Token: 0x04000A9B RID: 2715
	[ConditionalHide("isPersistent", true)]
	public int id = -1;

	// Token: 0x04000A9C RID: 2716
	public UnityEvent onSaveTrue;

	// Token: 0x04000A9D RID: 2717
	[ReadOnly]
	public bool isLoaded;
}
