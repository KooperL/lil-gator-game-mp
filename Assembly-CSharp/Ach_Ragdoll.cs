using System;
using UnityEngine;

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

	public Achievement achievement;

	public Rigidbody rigidbody;

	private const float fastSpeedThreshold = 25f;

	private const float fastSpeedSqr = 625f;

	private const float slowSpeedThreshold = 5f;

	private const float slowSpeedSqr = 25f;

	private int stepsSinceFast = 1000;

	private float maxSpeed;
}
