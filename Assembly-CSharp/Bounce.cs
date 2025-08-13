using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000A8 RID: 168
public class Bounce : MonoBehaviour
{
	// Token: 0x06000261 RID: 609 RVA: 0x0001F71C File Offset: 0x0001D91C
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
			Vector3 vector2 = base.transform.TransformDirection(this.bounceVelocity);
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

	// Token: 0x04000363 RID: 867
	public Vector3 bounceVelocity;

	// Token: 0x04000364 RID: 868
	public Vector3 dustOffset;

	// Token: 0x04000365 RID: 869
	public UnityEvent onBounce;

	// Token: 0x04000366 RID: 870
	public bool bounceWhileGrounded;

	// Token: 0x04000367 RID: 871
	private float cooldown = 0.5f;

	// Token: 0x04000368 RID: 872
	private float lastBounce = -1f;
}
