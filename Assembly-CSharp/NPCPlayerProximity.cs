using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class NPCPlayerProximity : MonoBehaviour
{
	// Token: 0x0600094E RID: 2382 RVA: 0x00039590 File Offset: 0x00037790
	private void OnValidate()
	{
		if (this.dialogueActor == null && base.transform.parent != null)
		{
			this.dialogueActor = base.transform.parent.GetComponent<DialogueActor>();
		}
		if (this.pathFollower == null && base.transform.parent != null)
		{
			this.pathFollower = base.transform.parent.GetComponent<ActorPathFollower>();
		}
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0000914C File Offset: 0x0000734C
	private void OnTriggerEnter(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0000914C File Offset: 0x0000734C
	private void OnTriggerStay(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0003960C File Offset: 0x0003780C
	private void OnTrigger()
	{
		this.dialogueActor.LookAt = true;
		this.dialogueActor.ProximityTrigger();
		Vector3 vector = DialogueActor.playerActor.FocusPosition;
		if (ItemCamera.isActive && ItemCamera.instance.cameraMode != ItemCamera.CameraMode.Static)
		{
			vector = MainCamera.t.position;
		}
		this.dialogueActor.lookAtTarget = vector;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = false;
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00009154 File Offset: 0x00007354
	private void OnTriggerExit(Collider other)
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00009154 File Offset: 0x00007354
	private void OnDisable()
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	// Token: 0x04000BFF RID: 3071
	public DialogueActor dialogueActor;

	// Token: 0x04000C00 RID: 3072
	public ActorPathFollower pathFollower;
}
