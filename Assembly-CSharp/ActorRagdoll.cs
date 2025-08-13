using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class ActorRagdoll : MonoBehaviour
{
	// Token: 0x06000374 RID: 884 RVA: 0x00026198 File Offset: 0x00024398
	private void OnValidate()
	{
		if (this.dialogueActor == null)
		{
			this.dialogueActor = base.GetComponent<DialogueActor>();
		}
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (this.ragdollBodies == null)
		{
			this.GatherRigidbodies();
		}
	}

	// Token: 0x06000375 RID: 885 RVA: 0x000261E8 File Offset: 0x000243E8
	private void Start()
	{
		Rigidbody[] array = this.ragdollBodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = true;
		}
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00004A68 File Offset: 0x00002C68
	[ContextMenu("Gather Rigidbodies")]
	private void GatherRigidbodies()
	{
		this.ragdollBodies = base.GetComponentsInChildren<Rigidbody>();
		Rigidbody componentInChildren = base.GetComponentInChildren<Rigidbody>();
		this.rootRigidbody = ((componentInChildren != null) ? componentInChildren.transform : null);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00026214 File Offset: 0x00024414
	[ContextMenu("Enable Rigidbody")]
	public void EnableRagdoll()
	{
		this.animator.enabled = false;
		this.dialogueActor.enabled = false;
		this.dialogueActor.isRagdolling = true;
		Vector3 vector = base.transform.TransformVector(this.initialPush);
		foreach (Rigidbody rigidbody in this.ragdollBodies)
		{
			rigidbody.isKinematic = false;
			rigidbody.velocity = vector;
		}
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0002627C File Offset: 0x0002447C
	[ContextMenu("Disable Rigidbody")]
	public void DisableRagdoll()
	{
		if (this.applyTransform)
		{
			base.transform.position = this.rootRigidbody.position;
			this.dialogueActor.SnapToFloor();
		}
		this.dialogueActor.isRagdolling = false;
		this.animator.enabled = true;
		Rigidbody[] array = this.ragdollBodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = true;
		}
	}

	// Token: 0x04000500 RID: 1280
	public DialogueActor dialogueActor;

	// Token: 0x04000501 RID: 1281
	public Animator animator;

	// Token: 0x04000502 RID: 1282
	public Rigidbody[] ragdollBodies;

	// Token: 0x04000503 RID: 1283
	public Transform rootRigidbody;

	// Token: 0x04000504 RID: 1284
	public bool applyTransform;

	// Token: 0x04000505 RID: 1285
	public Vector3 initialPush = Vector3.back;
}
