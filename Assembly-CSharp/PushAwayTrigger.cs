using System;
using UnityEngine;

// Token: 0x0200029E RID: 670
public class PushAwayTrigger : MonoBehaviour
{
	// Token: 0x06000D05 RID: 3333 RVA: 0x0000C083 File Offset: 0x0000A283
	private void Awake()
	{
		this.collider = base.GetComponent<CapsuleCollider>();
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x00048F60 File Offset: 0x00047160
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

	// Token: 0x04001160 RID: 4448
	private CapsuleCollider collider;

	// Token: 0x04001161 RID: 4449
	public float pushAmount = 2f;

	// Token: 0x04001162 RID: 4450
	public float control = 0.2f;
}
