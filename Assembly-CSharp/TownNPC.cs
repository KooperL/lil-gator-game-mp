using System;
using UnityEngine;

// Token: 0x02000195 RID: 405
public class TownNPC : MonoBehaviour
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000840 RID: 2112 RVA: 0x00027631 File Offset: 0x00025831
	private string SaveID
	{
		get
		{
			return "TNPC" + this.id;
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00027643 File Offset: 0x00025843
	private void Start()
	{
		if (this.inTown)
		{
			base.gameObject.SetActive(this.alwaysActive || GameData.g.ReadBool(this.SaveID, false));
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00027674 File Offset: 0x00025874
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

	// Token: 0x06000843 RID: 2115 RVA: 0x000276D5 File Offset: 0x000258D5
	public void EnableTownInstance()
	{
		GameData.g.Write(this.SaveID, true);
		this.townInstance.gameObject.SetActive(true);
	}

	// Token: 0x04000A66 RID: 2662
	public string id;

	// Token: 0x04000A67 RID: 2663
	public bool inTown;

	// Token: 0x04000A68 RID: 2664
	public bool alwaysActive;

	// Token: 0x04000A69 RID: 2665
	public TownNPC townInstance;
}
