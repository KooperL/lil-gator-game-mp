using System;
using UnityEngine;

public class ActorRagdoll : MonoBehaviour
{
	// Token: 0x0600039A RID: 922 RVA: 0x00027144 File Offset: 0x00025344
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

	// Token: 0x0600039B RID: 923 RVA: 0x00027194 File Offset: 0x00025394
	private void Start()
	{
		Rigidbody[] array = this.ragdollBodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = true;
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00004C4C File Offset: 0x00002E4C
	[ContextMenu("Gather Rigidbodies")]
	private void GatherRigidbodies()
	{
		this.ragdollBodies = base.GetComponentsInChildren<Rigidbody>();
		Rigidbody componentInChildren = base.GetComponentInChildren<Rigidbody>();
		this.rootRigidbody = ((componentInChildren != null) ? componentInChildren.transform : null);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x000271C0 File Offset: 0x000253C0
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

	// Token: 0x0600039E RID: 926 RVA: 0x00027228 File Offset: 0x00025428
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
