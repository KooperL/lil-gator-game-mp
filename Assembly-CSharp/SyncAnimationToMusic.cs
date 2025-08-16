using System;
using UnityEngine;

public class SyncAnimationToMusic : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060000D3 RID: 211 RVA: 0x0001A3A8 File Offset: 0x000185A8
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

	// Token: 0x060000D4 RID: 212 RVA: 0x00002229 File Offset: 0x00000429
	public void ManagedUpdate()
	{
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0001A410 File Offset: 0x00018610
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

	public MusicSystem[] musicToIgnore;

	public float sittingLength = 1.6667f;

	public float standingLength = 2.5f;

	public Animator animator;

	private readonly int standingMultID = Animator.StringToHash("StandingSpeedMult");

	private readonly int sittingMultID = Animator.StringToHash("SittingSpeedMult");

	private MusicSystem currentMusicSystem;

	private bool isTransitioning;

	private float bpm;

	private float standingMult = 1f;

	private float standingMultTarget = 1f;

	private float standingBeatMult = 1f;

	private float sittingMult = 1f;

	private float sittingMultTarget = 1f;

	private float sittingBeatMult = 1f;

	private bool isSynchronizing;

	private bool isSynchronized;

	private const float minimumMult = 0.74f;

	private const float maximumMult = 1.5f;

	private readonly int standingHash = Animator.StringToHash("Happy");

	private readonly int sittingHash = Animator.StringToHash("Sitting|Happy");

	[Range(0f, 1f)]
	public float animationBeatT;

	[Range(0f, 1f)]
	public float musicBeatT;

	public float syncMult;

	public float currentBeatOffset;

	private const float minSync = 0.025f;

	private const float maxSync = 0.25f;

	private const float underSyncMult = 1.1f;

	private const float overSyncMult = 0.9f;
}
