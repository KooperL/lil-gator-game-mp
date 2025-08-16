using System;
using UnityEngine;

public class TownNPC : MonoBehaviour
{
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x0000998E File Offset: 0x00007B8E
	private string SaveID
	{
		get
		{
			return "TNPC" + this.id;
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x000099A0 File Offset: 0x00007BA0
	private void Start()
	{
		if (this.inTown)
		{
			base.gameObject.SetActive(this.alwaysActive || GameData.g.ReadBool(this.SaveID, false));
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0003BDF8 File Offset: 0x00039FF8
	[ContextMenu("Find Town Instance")]
	public void FindTownInstance()
	{
		if (this.inTown)
		{
			this.townInstance = this;
			return;
		}
		foreach (TownNPC townNPC in global::UnityEngine.Object.FindObjectsOfType<TownNPC>())
		{
			if (townNPC != this && townNPC.id == this.id && townNPC.inTown)
			{
				this.townInstance = townNPC;
				return;
			}
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x000099D1 File Offset: 0x00007BD1
	public void EnableTownInstance()
	{
		GameData.g.Write(this.SaveID, true);
		this.townInstance.gameObject.SetActive(true);
	}

	public string id;

	public bool inTown;

	public bool alwaysActive;

	public TownNPC townInstance;
}
