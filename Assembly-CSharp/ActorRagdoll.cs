using System;
using UnityEngine;

public class ActorRagdoll : MonoBehaviour
{
	// Token: 0x0600032F RID: 815 RVA: 0x00012E18 File Offset: 0x00011018
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

	// Token: 0x06000330 RID: 816 RVA: 0x00012E68 File Offset: 0x00011068
	private void Start()
	{
		Rigidbody[] array = this.ragdollBodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = true;
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00012E93 File Offset: 0x00011093
	[ContextMenu("Gather Rigidbodies")]
	private void GatherRigidbodies()
	{
		this.ragdollBodies = base.GetComponentsInChildren<Rigidbody>();
		Rigidbody componentInChildren = base.GetComponentInChildren<Rigidbody>();
		this.rootRigidbody = ((componentInChildren != null) ? componentInChildren.transform : null);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00012EBC File Offset: 0x000110BC
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

	// Token: 0x06000333 RID: 819 RVA: 0x00012F24 File Offset: 0x00011124
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

	public DialogueActor dialogueActor;

	public Animator animator;

	public Rigidbody[] ragdollBodies;

	public Transform rootRigidbody;

	public bool applyTransform;

	public Vector3 initialPush = Vector3.back;
}
