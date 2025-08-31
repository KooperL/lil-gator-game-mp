using System;
using UnityEngine;
using UnityEngine.Events;

public class PositionChallenge : PersistentObject
{
	// Token: 0x060006CA RID: 1738 RVA: 0x000227E4 File Offset: 0x000209E4
	private void Awake()
	{
		BreakableObject[] array = this.countedBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onBreak.AddListener(new UnityAction(this.BreakableBroken));
		}
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0002281F File Offset: 0x00020A1F
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.challengeBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00022832 File Offset: 0x00020A32
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00022841 File Offset: 0x00020A41
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00022850 File Offset: 0x00020A50
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

	// Token: 0x060006CF RID: 1743 RVA: 0x000228A3 File Offset: 0x00020AA3
	public void OnTrigger()
	{
		this.raceTrigger = Time.time;
		if (!this.isInChallenge)
		{
			this.StartChallenge();
		}
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x000228C0 File Offset: 0x00020AC0
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

	// Token: 0x060006D1 RID: 1745 RVA: 0x00022946 File Offset: 0x00020B46
	protected virtual Transform RaceGoal()
	{
		return this.startObject.transform;
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00022953 File Offset: 0x00020B53
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

	// Token: 0x060006D3 RID: 1747 RVA: 0x00022980 File Offset: 0x00020B80
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

	// Token: 0x060006D4 RID: 1748 RVA: 0x000229E4 File Offset: 0x00020BE4
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

	// Token: 0x060006D5 RID: 1749 RVA: 0x00022A34 File Offset: 0x00020C34
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

	// Token: 0x060006D6 RID: 1750 RVA: 0x00022ACC File Offset: 0x00020CCC
	public void ClearRace()
	{
		base.enabled = false;
		this.isInChallenge = false;
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(false);
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00022AF8 File Offset: 0x00020CF8
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
