using System;
using UnityEngine;
using UnityEngine.Events;

public class PositionChallenge : PersistentObject
{
	// Token: 0x06000848 RID: 2120 RVA: 0x000378DC File Offset: 0x00035ADC
	private void Awake()
	{
		BreakableObject[] array = this.countedBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onBreak.AddListener(new UnityAction(this.BreakableBroken));
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0000828D File Offset: 0x0000648D
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.challengeBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x000082A0 File Offset: 0x000064A0
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x000082AF File Offset: 0x000064AF
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00037918 File Offset: 0x00035B18
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

	// Token: 0x0600084D RID: 2125 RVA: 0x000082BE File Offset: 0x000064BE
	public void OnTrigger()
	{
		this.raceTrigger = Time.time;
		if (!this.isInChallenge)
		{
			this.StartChallenge();
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0003796C File Offset: 0x00035B6C
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

	// Token: 0x0600084F RID: 2127 RVA: 0x000082D9 File Offset: 0x000064D9
	protected virtual Transform RaceGoal()
	{
		return this.startObject.transform;
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x000082E6 File Offset: 0x000064E6
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

	// Token: 0x06000851 RID: 2129 RVA: 0x000379F4 File Offset: 0x00035BF4
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

	// Token: 0x06000852 RID: 2130 RVA: 0x00037A58 File Offset: 0x00035C58
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

	// Token: 0x06000853 RID: 2131 RVA: 0x00037AA8 File Offset: 0x00035CA8
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

	// Token: 0x06000854 RID: 2132 RVA: 0x00008311 File Offset: 0x00006511
	public void ClearRace()
	{
		base.enabled = false;
		this.isInChallenge = false;
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(false);
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x00037B40 File Offset: 0x00035D40
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
