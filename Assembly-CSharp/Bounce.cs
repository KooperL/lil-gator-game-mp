using System;
using UnityEngine;
using UnityEngine.Events;

public class Bounce : MonoBehaviour
{
	// Token: 0x0600026E RID: 622 RVA: 0x0002013C File Offset: 0x0001E33C
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

	public Vector3 bounceVelocity;

	public float straightenBounceVelocity;

	public Vector3 dustOffset;

	public UnityEvent onBounce;

	public bool bounceWhileGrounded;

	private float cooldown = 0.5f;

	private float lastBounce = -1f;
}
