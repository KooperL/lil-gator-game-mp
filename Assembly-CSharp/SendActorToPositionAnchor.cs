using System;
using UnityEngine;

public class SendActorToPositionAnchor : MonoBehaviour
{
	// Token: 0x060004D9 RID: 1241 RVA: 0x000058C4 File Offset: 0x00003AC4
	private void OnEnable()
	{
		this.mount.InviteActor(this.actor, this.skipToMount, false);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x000058DE File Offset: 0x00003ADE
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
