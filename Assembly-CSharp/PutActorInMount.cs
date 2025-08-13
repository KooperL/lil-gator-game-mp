using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class PutActorInMount : MonoBehaviour
{
	// Token: 0x060003FA RID: 1018 RVA: 0x00017727 File Offset: 0x00015927
	private void OnEnable()
	{
		if (this.inviteOnEnable)
		{
			this.Invite();
		}
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00017737 File Offset: 0x00015937
	public void Invite()
	{
		this.mount.InviteActor(this.actor, this.skipToMount, false);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00017751 File Offset: 0x00015951
	private void OnDisable()
	{
		if (this.getOutOnDisable)
		{
			this.GetOut();
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00017761 File Offset: 0x00015961
	public void GetOut()
	{
		if (this.mount.actor == this.actor)
		{
			this.mount.GetOut();
		}
	}

	// Token: 0x04000586 RID: 1414
	public DialogueActor actor;

	// Token: 0x04000587 RID: 1415
	public ActorMount mount;

	// Token: 0x04000588 RID: 1416
	public bool skipToMount;

	// Token: 0x04000589 RID: 1417
	public bool inviteOnEnable = true;

	// Token: 0x0400058A RID: 1418
	public bool getOutOnDisable;
}
