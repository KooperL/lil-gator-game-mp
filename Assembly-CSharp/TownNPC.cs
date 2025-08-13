using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class TownNPC : MonoBehaviour
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0000966F File Offset: 0x0000786F
	private string SaveID
	{
		get
		{
			return "TNPC" + this.id;
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00009681 File Offset: 0x00007881
	private void Start()
	{
		if (this.inTown)
		{
			base.gameObject.SetActive(this.alwaysActive || GameData.g.ReadBool(this.SaveID, false));
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0003A4E4 File Offset: 0x000386E4
	[ContextMenu("Find Town Instance")]
	public void FindTownInstance()
	{
		if (this.inTown)
		{
			this.townInstance = this;
			return;
		}
		foreach (TownNPC townNPC in Object.FindObjectsOfType<TownNPC>())
		{
			if (townNPC != this && townNPC.id == this.id && townNPC.inTown)
			{
				this.townInstance = townNPC;
				return;
			}
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x000096B2 File Offset: 0x000078B2
	public void EnableTownInstance()
	{
		GameData.g.Write(this.SaveID, true);
		this.townInstance.gameObject.SetActive(true);
	}

	// Token: 0x04000C5C RID: 3164
	public string id;

	// Token: 0x04000C5D RID: 3165
	public bool inTown;

	// Token: 0x04000C5E RID: 3166
	public bool alwaysActive;

	// Token: 0x04000C5F RID: 3167
	public TownNPC townInstance;
}
