using System;
using UnityEngine;

public class SendActorToPositionAnchor : MonoBehaviour
{
	// Token: 0x060003FF RID: 1023 RVA: 0x00017795 File Offset: 0x00015995
	private void OnEnable()
	{
		this.mount.InviteActor(this.actor, this.skipToMount, false);
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x000177AF File Offset: 0x000159AF
	private void OnDisable()
	{
		if (this.actor != null && this != null)
		{
			this.mount.GetOut();
		}
	}

	public DialogueActor actor;

	public ActorMount mount;

	public bool skipToMount;
}
