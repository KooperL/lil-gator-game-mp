using System;
using UnityEngine;

public class NPCPlayerProximity : MonoBehaviour
{
	// Token: 0x0600098F RID: 2447 RVA: 0x0003AF00 File Offset: 0x00039100
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

	// Token: 0x06000990 RID: 2448 RVA: 0x0000947D File Offset: 0x0000767D
	private void OnTriggerEnter(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0000947D File Offset: 0x0000767D
	private void OnTriggerStay(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0003AF7C File Offset: 0x0003917C
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

	// Token: 0x06000993 RID: 2451 RVA: 0x00009485 File Offset: 0x00007685
	private void OnTriggerExit(Collider other)
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x00009485 File Offset: 0x00007685
	private void OnDisable()
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	public DialogueActor dialogueActor;

	public ActorPathFollower pathFollower;
}
