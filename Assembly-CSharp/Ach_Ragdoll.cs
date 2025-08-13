using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class Ach_Ragdoll : MonoBehaviour
{
	// Token: 0x060001E5 RID: 485 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000038D0 File Offset: 0x00001AD0
	private void OnEnable()
	{
		this.stepsSinceFast = 1000;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0001DA3C File Offset: 0x0001BC3C
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

	// Token: 0x040002D2 RID: 722
	public Achievement achievement;

	// Token: 0x040002D3 RID: 723
	public Rigidbody rigidbody;

	// Token: 0x040002D4 RID: 724
	private const float fastSpeedThreshold = 25f;

	// Token: 0x040002D5 RID: 725
	private const float fastSpeedSqr = 625f;

	// Token: 0x040002D6 RID: 726
	private const float slowSpeedThreshold = 5f;

	// Token: 0x040002D7 RID: 727
	private const float slowSpeedSqr = 25f;

	// Token: 0x040002D8 RID: 728
	private int stepsSinceFast = 1000;

	// Token: 0x040002D9 RID: 729
	private float maxSpeed;
}
