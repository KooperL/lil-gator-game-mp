using System;
using UnityEngine;

// Token: 0x02000181 RID: 385
public class NPCPlayerProximity : MonoBehaviour
{
	// Token: 0x060007EC RID: 2028 RVA: 0x00026658 File Offset: 0x00024858
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

	// Token: 0x060007ED RID: 2029 RVA: 0x000266D3 File Offset: 0x000248D3
	private void OnTriggerEnter(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x000266DB File Offset: 0x000248DB
	private void OnTriggerStay(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x000266E4 File Offset: 0x000248E4
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

	// Token: 0x060007F0 RID: 2032 RVA: 0x00026758 File Offset: 0x00024958
	private void OnTriggerExit(Collider other)
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00026780 File Offset: 0x00024980
	private void OnDisable()
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	// Token: 0x04000A23 RID: 2595
	public DialogueActor dialogueActor;

	// Token: 0x04000A24 RID: 2596
	public ActorPathFollower pathFollower;
}
