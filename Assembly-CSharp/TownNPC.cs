using System;
using UnityEngine;

public class TownNPC : MonoBehaviour
{
	// (get) Token: 0x060009FF RID: 2559 RVA: 0x000099AD File Offset: 0x00007BAD
	private string SaveID
	{
		get
		{
			return "TNPC" + this.id;
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x000099BF File Offset: 0x00007BBF
	private void Start()
	{
		if (this.inTown)
		{
			base.gameObject.SetActive(this.alwaysActive || GameData.g.ReadBool(this.SaveID, false));
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0003BF68 File Offset: 0x0003A168
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

	// Token: 0x06000A02 RID: 2562 RVA: 0x000099F0 File Offset: 0x00007BF0
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
