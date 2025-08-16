using System;
using UnityEngine;
using UnityEngine.Events;

public class PositionChallenge : PersistentObject
{
	// Token: 0x06000848 RID: 2120 RVA: 0x00037720 File Offset: 0x00035920
	private void Awake()
	{
		BreakableObject[] array = this.countedBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onBreak.AddListener(new UnityAction(this.BreakableBroken));
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00008278 File Offset: 0x00006478
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.challengeBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0000828B File Offset: 0x0000648B
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0000829A File Offset: 0x0000649A
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0003775C File Offset: 0x0003595C
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

	// Token: 0x0600084D RID: 2125 RVA: 0x000082A9 File Offset: 0x000064A9
	public void OnTrigger()
	{
		this.raceTrigger = Time.time;
		if (!this.isInChallenge)
		{
			this.StartChallenge();
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x000377B0 File Offset: 0x000359B0
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

	// Token: 0x0600084F RID: 2127 RVA: 0x000082C4 File Offset: 0x000064C4
	protected virtual Transform RaceGoal()
	{
		return this.startObject.transform;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x000082D1 File Offset: 0x000064D1
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

	// Token: 0x06000851 RID: 2129 RVA: 0x00037838 File Offset: 0x00035A38
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

	// Token: 0x06000852 RID: 2130 RVA: 0x0003789C File Offset: 0x00035A9C
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

	// Token: 0x06000853 RID: 2131 RVA: 0x000378EC File Offset: 0x00035AEC
	public void FinishChallenge()
	{
		if (!this.isInChallenge)
		{
			return;
		}
		ParticleSystem component = global::UnityEngine.Object.Instantiate<GameObject>(this.rewardObject, base.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
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

	// Token: 0x06000854 RID: 2132 RVA: 0x000082FC File Offset: 0x000064FC
	public void ClearRace()
	{
		base.enabled = false;
		this.isInChallenge = false;
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(false);
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00037984 File Offset: 0x00035B84
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

	public GameObject startObject;

	public BreakableObject[] challengeBreakables;

	public BreakableObject[] countedBreakables;

	private float raceTrigger = -1f;

	private const float raceTriggerDelay = 0.5f;

	private const float raceTimeout = 0.2f;

	private bool isInChallenge;

	private bool isFinished;

	public AudioClip raceStartSound;

	public AudioClip raceFailedSound;

	public AudioClip raceFinishedSound;

	public GameObject isRacingSound;

	public UnityEvent onFinish;

	public UnityEvent onFail;

	public GameObject rewardObject;

	public int rewardAmount = 30;
}
