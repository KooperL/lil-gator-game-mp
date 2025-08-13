using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class PutActorInMount : MonoBehaviour
{
	// Token: 0x060004AE RID: 1198 RVA: 0x00005630 File Offset: 0x00003830
	private void OnEnable()
	{
		if (this.inviteOnEnable)
		{
			this.Invite();
		}
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00005640 File Offset: 0x00003840
	public void Invite()
	{
		this.mount.InviteActor(this.actor, this.skipToMount, false);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0000565A File Offset: 0x0000385A
	private void OnDisable()
	{
		if (this.getOutOnDisable)
		{
			this.GetOut();
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0000566A File Offset: 0x0000386A
	public void GetOut()
	{
		if (this.mount.actor == this.actor)
		{
			this.mount.GetOut();
		}
	}

	// Token: 0x04000698 RID: 1688
	public DialogueActor actor;

	// Token: 0x04000699 RID: 1689
	public ActorMount mount;

	// Token: 0x0400069A RID: 1690
	public bool skipToMount;

	// Token: 0x0400069B RID: 1691
	public bool inviteOnEnable = true;

	// Token: 0x0400069C RID: 1692
	public bool getOutOnDisable;
}
