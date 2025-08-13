using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class SendActorToPositionAnchor : MonoBehaviour
{
	// Token: 0x060004B3 RID: 1203 RVA: 0x0000569E File Offset: 0x0000389E
	private void OnEnable()
	{
		this.mount.InviteActor(this.actor, this.skipToMount, false);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x000056B8 File Offset: 0x000038B8
	private void OnDisable()
	{
		if (this.actor != null && this != null)
		{
			this.mount.GetOut();
		}
	}

	// Token: 0x0400069D RID: 1693
	public DialogueActor actor;

	// Token: 0x0400069E RID: 1694
	public ActorMount mount;

	// Token: 0x0400069F RID: 1695
	public bool skipToMount;
}
