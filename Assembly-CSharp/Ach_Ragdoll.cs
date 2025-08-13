using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class Ach_Ragdoll : MonoBehaviour
{
	// Token: 0x060001AF RID: 431 RVA: 0x000099FB File Offset: 0x00007BFB
	private void Awake()
	{
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x000099FD File Offset: 0x00007BFD
	private void OnEnable()
	{
		this.stepsSinceFast = 1000;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00009A0C File Offset: 0x00007C0C
	public void FixedUpdate()
	{
		float sqrMagnitude = this.rigidbody.velocity.sqrMagnitude;
		if (sqrMagnitude > 625f)
		{
			this.stepsSinceFast = 0;
		}
		else
		{
			this.stepsSinceFast++;
		}
		if (sqrMagnitude < 25f && this.stepsSinceFast < 20)
		{
			this.stepsSinceFast = 100;
			this.achievement.UnlockAchievement();
		}
	}

	// Token: 0x0400024E RID: 590
	public Achievement achievement;

	// Token: 0x0400024F RID: 591
	public Rigidbody rigidbody;

	// Token: 0x04000250 RID: 592
	private const float fastSpeedThreshold = 25f;

	// Token: 0x04000251 RID: 593
	private const float fastSpeedSqr = 625f;

	// Token: 0x04000252 RID: 594
	private const float slowSpeedThreshold = 5f;

	// Token: 0x04000253 RID: 595
	private const float slowSpeedSqr = 25f;

	// Token: 0x04000254 RID: 596
	private int stepsSinceFast = 1000;

	// Token: 0x04000255 RID: 597
	private float maxSpeed;
}
