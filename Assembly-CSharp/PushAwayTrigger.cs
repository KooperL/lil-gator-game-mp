using System;
using UnityEngine;

public class PushAwayTrigger : MonoBehaviour
{
	// Token: 0x06000D51 RID: 3409 RVA: 0x0000C395 File Offset: 0x0000A595
	private void Awake()
	{
		this.collider = base.GetComponent<CapsuleCollider>();
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0004AAC4 File Offset: 0x00048CC4
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

	private CapsuleCollider collider;

	public float pushAmount = 2f;

	public float control = 0.2f;
}
