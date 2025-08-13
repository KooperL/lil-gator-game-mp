using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class SyncAnimationToMusic : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060000BF RID: 191 RVA: 0x00005610 File Offset: 0x00003810
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

	// Token: 0x060000C0 RID: 192 RVA: 0x00005677 File Offset: 0x00003877
	public void ManagedUpdate()
	{
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000567C File Offset: 0x0000387C
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

	// Token: 0x040000E9 RID: 233
	public MusicSystem[] musicToIgnore;

	// Token: 0x040000EA RID: 234
	public float sittingLength = 1.6667f;

	// Token: 0x040000EB RID: 235
	public float standingLength = 2.5f;

	// Token: 0x040000EC RID: 236
	public Animator animator;

	// Token: 0x040000ED RID: 237
	private readonly int standingMultID = Animator.StringToHash("StandingSpeedMult");

	// Token: 0x040000EE RID: 238
	private readonly int sittingMultID = Animator.StringToHash("SittingSpeedMult");

	// Token: 0x040000EF RID: 239
	private MusicSystem currentMusicSystem;

	// Token: 0x040000F0 RID: 240
	private bool isTransitioning;

	// Token: 0x040000F1 RID: 241
	private float bpm;

	// Token: 0x040000F2 RID: 242
	private float standingMult = 1f;

	// Token: 0x040000F3 RID: 243
	private float standingMultTarget = 1f;

	// Token: 0x040000F4 RID: 244
	private float standingBeatMult = 1f;

	// Token: 0x040000F5 RID: 245
	private float sittingMult = 1f;

	// Token: 0x040000F6 RID: 246
	private float sittingMultTarget = 1f;

	// Token: 0x040000F7 RID: 247
	private float sittingBeatMult = 1f;

	// Token: 0x040000F8 RID: 248
	private bool isSynchronizing;

	// Token: 0x040000F9 RID: 249
	private bool isSynchronized;

	// Token: 0x040000FA RID: 250
	private const float minimumMult = 0.74f;

	// Token: 0x040000FB RID: 251
	private const float maximumMult = 1.5f;

	// Token: 0x040000FC RID: 252
	private readonly int standingHash = Animator.StringToHash("Happy");

	// Token: 0x040000FD RID: 253
	private readonly int sittingHash = Animator.StringToHash("Sitting|Happy");

	// Token: 0x040000FE RID: 254
	[Range(0f, 1f)]
	public float animationBeatT;

	// Token: 0x040000FF RID: 255
	[Range(0f, 1f)]
	public float musicBeatT;

	// Token: 0x04000100 RID: 256
	public float syncMult;

	// Token: 0x04000101 RID: 257
	public float currentBeatOffset;

	// Token: 0x04000102 RID: 258
	private const float minSync = 0.025f;

	// Token: 0x04000103 RID: 259
	private const float maxSync = 0.25f;

	// Token: 0x04000104 RID: 260
	private const float underSyncMult = 1.1f;

	// Token: 0x04000105 RID: 261
	private const float overSyncMult = 0.9f;
}
