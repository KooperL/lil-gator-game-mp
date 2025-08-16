using System;
using UnityEngine;

public class PutActorInMount : MonoBehaviour
{
	// Token: 0x060004D4 RID: 1236 RVA: 0x00005856 File Offset: 0x00003A56
	private void OnEnable()
	{
		if (this.inviteOnEnable)
		{
			this.Invite();
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00005866 File Offset: 0x00003A66
	public void Invite()
	{
		this.mount.InviteActor(this.actor, this.skipToMount, false);
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00005880 File Offset: 0x00003A80
	private void OnDisable()
	{
		if (this.getOutOnDisable)
		{
			this.GetOut();
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00005890 File Offset: 0x00003A90
	public void GetOut()
	{
		if (this.mount.actor == this.actor)
		{
			this.mount.GetOut();
		}
	}

	public DialogueActor actor;

	public ActorMount mount;

	public bool skipToMount;

	public bool inviteOnEnable = true;

	public bool getOutOnDisable;
}
