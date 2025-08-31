using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TimedChallenge : PersistentObject
{
	// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00022E27 File Offset: 0x00021027
	// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00022E4D File Offset: 0x0002104D
	private float BestTime
	{
		get
		{
			return GameData.g.ReadFloat("Race" + this.id.ToString(), -1f);
		}
		set
		{
			GameData.g.Write("Race" + this.id.ToString(), value);
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x00022E6F File Offset: 0x0002106F
	public override void OnValidate()
	{
		if (this.raceIcon == null)
		{
			this.raceIcon = Object.FindObjectOfType<UIRaceIcon>(true);
		}
		if (this.racePreview == null)
		{
			this.racePreview = Object.FindObjectOfType<UIRacePreview>(true);
		}
		base.OnValidate();
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00022EAB File Offset: 0x000210AB
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.timedBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00022EBE File Offset: 0x000210BE
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x00022ECD File Offset: 0x000210CD
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x00022EDC File Offset: 0x000210DC
	protected virtual void LoadState()
	{
		this.startObject.SetActive(!this.isFinished || this.isRepeatable);
		foreach (BreakableObject breakableObject in this.timedBreakables)
		{
			if (!breakableObject.isBroken || this.isRepeatable)
			{
				breakableObject.gameObject.SetActive(this.isFinished && !this.isRepeatable);
			}
		}
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x00022F4D File Offset: 0x0002114D
	public virtual void EnterProximity()
	{
		if (this.isRepeatable && base.PersistentState)
		{
			this.racePreview.Load(this.startObject.transform, this.BestTime);
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x00022F7B File Offset: 0x0002117B
	public virtual void TriggerStay()
	{
		this.lastTrigger = Time.time;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00022F88 File Offset: 0x00021188
	public virtual void StartRace()
	{
		if (Time.time - this.raceTrigger < 0.5f)
		{
			return;
		}
		if (Time.time - this.lastTrigger < 0.1f)
		{
			return;
		}
		this.raceTrigger = Time.time;
		if (this.isRacing)
		{
			this.FailedRace();
			return;
		}
		if (TimedChallenge.current != null && TimedChallenge.current != this)
		{
			TimedChallenge.current.CancelRace();
		}
		TimedChallenge.current = this;
		foreach (BreakableObject breakableObject in this.timedBreakables)
		{
			if (this.isRepeatable && breakableObject.isPersistent && breakableObject.PersistentState)
			{
				breakableObject.isBroken = false;
				breakableObject.isPersistent = false;
			}
			breakableObject.waitForValidation = true;
			breakableObject.gameObject.SetActive(true);
		}
		this.DoParticleEffects();
		this.isRacing = true;
		this.startTime = Time.time;
		this.finishTime = Time.time + this.raceTime;
		base.enabled = true;
		if (this.raceStartSound != null)
		{
			PlayAudio.p.Play(this.raceStartSound, 0.5f, 1f);
		}
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(true);
		}
		if (this.raceIconAnchor != null)
		{
			this.raceIconAnchor.gameObject.SetActive(true);
		}
		this.racePreview.Clear();
		if (this.resistCulling != null)
		{
			this.resistCulling.enabled = true;
		}
		if (base.PersistentState)
		{
			this.raceIcon.LoadRace(this.raceTime, this.RaceGoal(), this.BestTime);
			return;
		}
		this.raceIcon.LoadRace(this.raceTime, this.RaceGoal(), -1f);
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0002314C File Offset: 0x0002134C
	protected virtual Transform RaceGoal()
	{
		if (this.raceIconAnchor != null)
		{
			return this.raceIconAnchor;
		}
		return this.startObject.transform;
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002316E File Offset: 0x0002136E
	private void Update()
	{
		if (!this.isRacing)
		{
			base.enabled = false;
		}
		if (Time.time > this.finishTime)
		{
			this.FailedRace();
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x00023192 File Offset: 0x00021392
	protected virtual void FailedRace()
	{
		if (this.raceFailedSound != null)
		{
			PlayAudio.p.Play(this.raceFailedSound, 0.5f, 1f);
		}
		this.onFail.Invoke();
		this.CancelRace();
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x000231D0 File Offset: 0x000213D0
	public virtual void CancelRace()
	{
		BreakableObject[] array = this.timedBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].gameObject.SetActive(false);
		}
		this.DoParticleEffects();
		this.ClearRace();
		if (this.isRepeatable && base.PersistentState && Vector3.Distance(Player.Position, this.startObject.transform.position) < 6f)
		{
			this.racePreview.Load(this.startObject.transform, this.BestTime);
		}
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0002325C File Offset: 0x0002145C
	public void FinishRace()
	{
		bool flag = !base.PersistentState;
		if (!this.isRacing)
		{
			return;
		}
		this.playerFinishTime = Time.time - this.startTime;
		if (!this.isRepeatable)
		{
			this.startObject.SetActive(false);
		}
		this.SaveTrue();
		foreach (BreakableObject breakableObject in this.timedBreakables)
		{
			if (breakableObject.isPersistent)
			{
				breakableObject.ValidateBreak();
			}
			if (this.isRepeatable)
			{
				breakableObject.gameObject.SetActive(false);
			}
		}
		if (this.raceFinishedSound != null)
		{
			PlayAudio.p.Play(this.raceFinishedSound, 0.75f, 1f);
		}
		this.ClearRace();
		if (flag)
		{
			this.onFinish.Invoke();
			if (this.isRepeatable)
			{
				this.BestTime = this.playerFinishTime;
				return;
			}
		}
		else
		{
			this.onRepeat.Invoke();
			float bestTime = this.BestTime;
			if (this.playerFinishTime < bestTime || bestTime < 0f)
			{
				this.BestTime = this.playerFinishTime;
			}
			this.raceIcon.LoadEnd(this.RaceGoal(), bestTime, this.playerFinishTime);
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00023380 File Offset: 0x00021580
	public virtual void ClearRace()
	{
		TimedChallenge.current = null;
		base.enabled = false;
		this.isRacing = false;
		if (this.isRacingSound != null)
		{
			this.isRacingSound.SetActive(false);
		}
		this.raceIcon.Clear();
		if (this.raceIconAnchor != null)
		{
			this.raceIconAnchor.gameObject.SetActive(false);
		}
		if (this.resistCulling != null)
		{
			this.resistCulling.enabled = false;
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00023400 File Offset: 0x00021600
	protected virtual void DoParticleEffects()
	{
		foreach (BreakableObject breakableObject in this.timedBreakables)
		{
			if (!breakableObject.isBroken)
			{
				EffectsManager.e.Dust(breakableObject.transform.position, 10);
			}
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00023445 File Offset: 0x00021645
	public void SetPlaceholderTextToFinishTime(int index)
	{
		MultilingualTextDocument.SetPlaceholder(index, this.playerFinishTime.ToString("0.00"));
	}

	private static TimedChallenge current;

	public UIRaceIcon raceIcon;

	public UIRacePreview racePreview;

	public Transform raceIconAnchor;

	public GameObject startObject;

	[FormerlySerializedAs("raceObjects")]
	public BreakableObject[] timedBreakables;

	private float raceTrigger = -1f;

	private const float raceTriggerDelay = 0.5f;

	public float raceTime = 5f;

	[ReadOnly]
	public float playerFinishTime;

	private float finishTime;

	private float startTime;

	protected bool isRacing;

	private bool isFinished;

	public AudioClip raceStartSound;

	public AudioClip raceFailedSound;

	public AudioClip raceFinishedSound;

	public GameObject isRacingSound;

	public UnityEvent onFinish;

	public UnityEvent onFail;

	public UnityEvent onRepeat;

	public bool isRepeatable;

	public ResistCulling resistCulling;

	private float lastTrigger = -10f;
}
