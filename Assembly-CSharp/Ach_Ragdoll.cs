using System;
using UnityEngine;

public class Ach_Ragdoll : MonoBehaviour
{
	// Token: 0x060001F2 RID: 498 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x000039BC File Offset: 0x00001BBC
	private void OnEnable()
	{
		this.stepsSinceFast = 1000;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0001E494 File Offset: 0x0001C694
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

	public Achievement achievement;

	public Rigidbody rigidbody;

	private const float fastSpeedThreshold = 25f;

	private const float fastSpeedSqr = 625f;

	private const float slowSpeedThreshold = 5f;

	private const float slowSpeedSqr = 25f;

	private int stepsSinceFast = 1000;

	private float maxSpeed;
}
