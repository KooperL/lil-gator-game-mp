using System;
using UnityEngine;

public class NPCPlayerProximity : MonoBehaviour
{
	// Token: 0x06000990 RID: 2448 RVA: 0x0003B1C8 File Offset: 0x000393C8
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

	// Token: 0x06000991 RID: 2449 RVA: 0x00009487 File Offset: 0x00007687
	private void OnTriggerEnter(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00009487 File Offset: 0x00007687
	private void OnTriggerStay(Collider other)
	{
		this.OnTrigger();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0003B244 File Offset: 0x00039444
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

	// Token: 0x06000994 RID: 2452 RVA: 0x0000948F File Offset: 0x0000768F
	private void OnTriggerExit(Collider other)
	{
		this.dialogueActor.LookAt = false;
		if (this.pathFollower != null)
		{
			this.pathFollower.moving = true;
		}
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0000948F File Offset: 0x0000768F
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
