using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class SyncAnimationToMusic : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060000CB RID: 203 RVA: 0x00019CC4 File Offset: 0x00017EC4
	private float GetMultForBpm(MusicSystem musicSystem, float animationLength, int beatsInAnimation, float minSpeed, float maxSpeed, out float beatMult)
	{
		float num = musicSystem.bpm * animationLength / 60f / (float)beatsInAnimation;
		beatMult = 1f;
		while (num < minSpeed)
		{
			num *= 2f;
			beatMult *= 2f;
		}
		while (num > maxSpeed)
		{
			num *= 0.5f;
			beatMult *= 0.5f;
		}
		return num * musicSystem.beatSyncMultiplier;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00002229 File Offset: 0x00000429
	public void ManagedUpdate()
	{
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00019D2C File Offset: 0x00017F2C
	private void Update()
	{
		MusicSystem musicSystem = null;
		if (Settings.hasMusic && MusicStateManager.m != null)
		{
			musicSystem = MusicStateManager.m.activeMusicSystem;
		}
		if (this.currentMusicSystem != musicSystem)
		{
			this.currentMusicSystem = musicSystem;
			if (this.currentMusicSystem != null && !this.musicToIgnore.Contains(this.currentMusicSystem))
			{
				this.bpm = this.currentMusicSystem.bpm;
				this.isTransitioning = true;
				this.sittingMultTarget = this.GetMultForBpm(this.currentMusicSystem, this.sittingLength, 2, 0.55f, 1.11f, out this.sittingBeatMult);
				this.standingMultTarget = this.GetMultForBpm(this.currentMusicSystem, this.standingLength, 4, 0.62f, 1.25f, out this.standingBeatMult);
				this.isSynchronizing = true;
			}
			else
			{
				this.isTransitioning = true;
				this.sittingMultTarget = 1f;
				this.standingMultTarget = 1f;
				this.isSynchronizing = false;
			}
		}
		if (this.isTransitioning)
		{
			this.isTransitioning = true;
			this.sittingMult = Mathf.MoveTowards(this.sittingMult, this.sittingMultTarget, 0.5f * Time.deltaTime);
			this.standingMult = Mathf.MoveTowards(this.standingMult, this.standingMultTarget, 0.5f * Time.deltaTime);
			if (this.sittingMult == this.sittingMultTarget && this.standingMult == this.standingMultTarget)
			{
				this.isTransitioning = false;
			}
			if (!this.isSynchronizing)
			{
				this.animator.SetFloat(this.standingMultID, this.standingMult);
				this.animator.SetFloat(this.sittingMultID, this.sittingMult);
			}
		}
		if (this.isSynchronizing)
		{
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.shortNameHash == this.sittingHash || currentAnimatorStateInfo.shortNameHash == this.standingHash)
			{
				bool flag = currentAnimatorStateInfo.shortNameHash == this.sittingHash;
				float num = (flag ? (2f / this.sittingBeatMult) : (4f / this.standingBeatMult));
				float num2 = (flag ? (-0.23f) : (-0.127f));
				this.animationBeatT = (currentAnimatorStateInfo.normalizedTime + num2) * num % 1f;
				this.musicBeatT = this.currentMusicSystem.GetCurrentBeatT();
				this.syncMult = 1f;
				if (this.musicBeatT >= 0f)
				{
					this.currentBeatOffset = this.animationBeatT - this.musicBeatT;
					if (this.currentBeatOffset > 0.5f)
					{
						this.currentBeatOffset -= 1f;
					}
					if (this.currentBeatOffset < -0.5f)
					{
						this.currentBeatOffset += 1f;
					}
					if (Mathf.Abs(this.currentBeatOffset) < 0.025f)
					{
						this.syncMult = 1f;
					}
					else if (this.currentBeatOffset < 0f)
					{
						this.syncMult = Mathf.Lerp(1f, 1.1f, Mathf.InverseLerp(-0.025f, -0.25f, this.currentBeatOffset));
					}
					else
					{
						this.syncMult = Mathf.Lerp(1f, 0.9f, Mathf.InverseLerp(0.025f, 0.25f, this.currentBeatOffset));
					}
				}
				this.animator.SetFloat(this.standingMultID, this.standingMult * this.syncMult);
				this.animator.SetFloat(this.sittingMultID, this.sittingMult * this.syncMult);
			}
		}
	}

	// Token: 0x04000110 RID: 272
	public MusicSystem[] musicToIgnore;

	// Token: 0x04000111 RID: 273
	public float sittingLength = 1.6667f;

	// Token: 0x04000112 RID: 274
	public float standingLength = 2.5f;

	// Token: 0x04000113 RID: 275
	public Animator animator;

	// Token: 0x04000114 RID: 276
	private readonly int standingMultID = Animator.StringToHash("StandingSpeedMult");

	// Token: 0x04000115 RID: 277
	private readonly int sittingMultID = Animator.StringToHash("SittingSpeedMult");

	// Token: 0x04000116 RID: 278
	private MusicSystem currentMusicSystem;

	// Token: 0x04000117 RID: 279
	private bool isTransitioning;

	// Token: 0x04000118 RID: 280
	private float bpm;

	// Token: 0x04000119 RID: 281
	private float standingMult = 1f;

	// Token: 0x0400011A RID: 282
	private float standingMultTarget = 1f;

	// Token: 0x0400011B RID: 283
	private float standingBeatMult = 1f;

	// Token: 0x0400011C RID: 284
	private float sittingMult = 1f;

	// Token: 0x0400011D RID: 285
	private float sittingMultTarget = 1f;

	// Token: 0x0400011E RID: 286
	private float sittingBeatMult = 1f;

	// Token: 0x0400011F RID: 287
	private bool isSynchronizing;

	// Token: 0x04000120 RID: 288
	private bool isSynchronized;

	// Token: 0x04000121 RID: 289
	private const float minimumMult = 0.74f;

	// Token: 0x04000122 RID: 290
	private const float maximumMult = 1.5f;

	// Token: 0x04000123 RID: 291
	private readonly int standingHash = Animator.StringToHash("Happy");

	// Token: 0x04000124 RID: 292
	private readonly int sittingHash = Animator.StringToHash("Sitting|Happy");

	// Token: 0x04000125 RID: 293
	[Range(0f, 1f)]
	public float animationBeatT;

	// Token: 0x04000126 RID: 294
	[Range(0f, 1f)]
	public float musicBeatT;

	// Token: 0x04000127 RID: 295
	public float syncMult;

	// Token: 0x04000128 RID: 296
	public float currentBeatOffset;

	// Token: 0x04000129 RID: 297
	private const float minSync = 0.025f;

	// Token: 0x0400012A RID: 298
	private const float maxSync = 0.25f;

	// Token: 0x0400012B RID: 299
	private const float underSyncMult = 1.1f;

	// Token: 0x0400012C RID: 300
	private const float overSyncMult = 0.9f;
}
