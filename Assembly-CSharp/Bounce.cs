using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000085 RID: 133
public class Bounce : MonoBehaviour
{
	// Token: 0x06000229 RID: 553 RVA: 0x0000BE18 File Offset: 0x0000A018
	public void OnTriggerStay(Collider other)
	{
		if (Time.time - this.lastBounce < this.cooldown)
		{
			return;
		}
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			Vector3 vector = attachedRigidbody.velocity;
			Vector3 vector2 = Vector3.Slerp(base.transform.TransformDirection(this.bounceVelocity), this.bounceVelocity, this.straightenBounceVelocity);
			float num = Vector3.Dot(vector, vector2.normalized);
			if (Player.movement.IsClimbing)
			{
				return;
			}
			if (Player.movement.IsGrounded && !this.bounceWhileGrounded)
			{
				return;
			}
			if (Player.movement.isGliding)
			{
				Player.movement.ClearMods();
			}
			if (Player.movement.isRagdolling)
			{
				Player.ragdollController.Deactivate();
			}
			vector -= num * vector2.normalized;
			vector += vector2;
			attachedRigidbody.velocity = vector;
			Player.movement.ResetGrounded();
			Player.movement.lockJumpHeldForNow = true;
			EffectsManager.e.FloorDust(base.transform.TransformPoint(this.dustOffset), 10, base.transform.up);
			this.onBounce.Invoke();
			this.lastBounce = Time.time;
		}
	}

	// Token: 0x040002D3 RID: 723
	public Vector3 bounceVelocity;

	// Token: 0x040002D4 RID: 724
	public float straightenBounceVelocity;

	// Token: 0x040002D5 RID: 725
	public Vector3 dustOffset;

	// Token: 0x040002D6 RID: 726
	public UnityEvent onBounce;

	// Token: 0x040002D7 RID: 727
	public bool bounceWhileGrounded;

	// Token: 0x040002D8 RID: 728
	private float cooldown = 0.5f;

	// Token: 0x040002D9 RID: 729
	private float lastBounce = -1f;
}
