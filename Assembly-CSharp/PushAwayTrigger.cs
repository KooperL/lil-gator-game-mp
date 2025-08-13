using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class PushAwayTrigger : MonoBehaviour
{
	// Token: 0x06000B3A RID: 2874 RVA: 0x00037EE2 File Offset: 0x000360E2
	private void Awake()
	{
		this.collider = base.GetComponent<CapsuleCollider>();
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00037EF0 File Offset: 0x000360F0
	private void OnTriggerStay(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (Player.input.inputDirection.magnitude > 0.1f)
		{
			return;
		}
		Vector3 vector = attachedRigidbody.position - base.transform.position;
		vector.y = 0f;
		Vector3 vector2 = this.pushAmount * vector.normalized;
		vector2.y = attachedRigidbody.velocity.y;
		attachedRigidbody.velocity = Vector3.MoveTowards(attachedRigidbody.velocity, vector2, Time.deltaTime * 20f);
		Player.movement.recoveringControl = Mathf.Min(Player.movement.recoveringControl, this.control);
	}

	// Token: 0x04000EFA RID: 3834
	private CapsuleCollider collider;

	// Token: 0x04000EFB RID: 3835
	public float pushAmount = 2f;

	// Token: 0x04000EFC RID: 3836
	public float control = 0.2f;
}
