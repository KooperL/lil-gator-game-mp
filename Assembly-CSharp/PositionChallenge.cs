using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001B1 RID: 433
public class PositionChallenge : PersistentObject
{
	// Token: 0x06000808 RID: 2056 RVA: 0x00035F98 File Offset: 0x00034198
	private void Awake()
	{
		BreakableObject[] array = this.countedBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onBreak.AddListener(new UnityAction(this.BreakableBroken));
		}
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00007F7E File Offset: 0x0000617E
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.challengeBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00007F91 File Offset: 0x00006191
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00007FA0 File Offset: 0x000061A0
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00035FD4 File Offset: 0x000341D4
	protected virtual void LoadState()
	{
		this.startObject.SetActive(!this.isFinished);
		foreach (BreakableObject breakableObject in this.challengeBreakables)
		{
			if (!breakableObject.isBroken)
			{
				breakableObject.gameObject.SetActive(this.isFinished);
			}
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00007FAF File Offset: 0x000061AF
	public void OnTrigger()
	{
		this.raceTrigger = Time.time;
		if (!this.isInChallenge)
		{
			this.StartChallenge();
		}
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x00036028 File Offset: 0x00034228
	public virtual void StartChallenge()
	{
		BreakableObject[] array = this.challengeBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(true);
		}
		this.DoParticleEffects();
		this.isInChallenge = true;
		base.enabled = true;
		if (this.raceStartSound != null)
		{
			PlayAudio.p.Play(this.raceStartSound, 0.5f, 1f);
		}
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(true);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00007FCA File Offset: 0x000061CA
	protected virtual Transform RaceGoal()
	{
		return this.startObject.transform;
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00007FD7 File Offset: 0x000061D7
	private void Update()
	{
		if (!this.isInChallenge)
		{
			base.enabled = false;
			return;
		}
		if (this.raceTrigger + 0.2f < Time.time)
		{
			this.CancelChallenge();
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x000360B0 File Offset: 0x000342B0
	public virtual void CancelChallenge()
	{
		if (this.raceFailedSound != null)
		{
			PlayAudio.p.Play(this.raceFailedSound, 0.5f, 1f);
		}
		BreakableObject[] array = this.challengeBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(false);
		}
		this.DoParticleEffects();
		this.ClearRace();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00036114 File Offset: 0x00034314
	private void BreakableBroken()
	{
		int num = 0;
		BreakableObject[] array = this.countedBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isBroken)
			{
				num++;
			}
		}
		if (num == this.countedBreakables.Length)
		{
			this.FinishChallenge();
			return;
		}
		PlayAudio.p.PlayQuestSting(num);
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00036164 File Offset: 0x00034364
	public void FinishChallenge()
	{
		if (!this.isInChallenge)
		{
			return;
		}
		ParticleSystem component = Object.Instantiate<GameObject>(this.rewardObject, base.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
		component.main.maxParticles = Mathf.RoundToInt((float)this.rewardAmount / component.GetComponent<ParticlePickup>().rewardPerPickup);
		this.startObject.SetActive(false);
		this.SaveTrue();
		if (this.raceFinishedSound != null)
		{
			PlayAudio.p.PlayQuestEndSting();
		}
		this.ClearRace();
		this.onFinish.Invoke();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00008002 File Offset: 0x00006202
	public void ClearRace()
	{
		base.enabled = false;
		this.isInChallenge = false;
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(false);
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000361FC File Offset: 0x000343FC
	protected virtual void DoParticleEffects()
	{
		foreach (BreakableObject breakableObject in this.challengeBreakables)
		{
			if (!breakableObject.isBroken)
			{
				EffectsManager.e.Dust(breakableObject.transform.position, 10);
			}
		}
	}

	// Token: 0x04000AB2 RID: 2738
	public GameObject startObject;

	// Token: 0x04000AB3 RID: 2739
	public BreakableObject[] challengeBreakables;

	// Token: 0x04000AB4 RID: 2740
	public BreakableObject[] countedBreakables;

	// Token: 0x04000AB5 RID: 2741
	private float raceTrigger = -1f;

	// Token: 0x04000AB6 RID: 2742
	private const float raceTriggerDelay = 0.5f;

	// Token: 0x04000AB7 RID: 2743
	private const float raceTimeout = 0.2f;

	// Token: 0x04000AB8 RID: 2744
	private bool isInChallenge;

	// Token: 0x04000AB9 RID: 2745
	private bool isFinished;

	// Token: 0x04000ABA RID: 2746
	public AudioClip raceStartSound;

	// Token: 0x04000ABB RID: 2747
	public AudioClip raceFailedSound;

	// Token: 0x04000ABC RID: 2748
	public AudioClip raceFinishedSound;

	// Token: 0x04000ABD RID: 2749
	public GameObject isRacingSound;

	// Token: 0x04000ABE RID: 2750
	public UnityEvent onFinish;

	// Token: 0x04000ABF RID: 2751
	public UnityEvent onFail;

	// Token: 0x04000AC0 RID: 2752
	public GameObject rewardObject;

	// Token: 0x04000AC1 RID: 2753
	public int rewardAmount = 30;
}
