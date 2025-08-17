using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TimedChallenge : PersistentObject
{
	// (get) Token: 0x06000865 RID: 2149 RVA: 0x00008421 File Offset: 0x00006621
	// (set) Token: 0x06000866 RID: 2150 RVA: 0x00008447 File Offset: 0x00006647
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

	// Token: 0x06000867 RID: 2151 RVA: 0x00008469 File Offset: 0x00006669
	public override void OnValidate()
	{
		if (this.raceIcon == null)
		{
			this.raceIcon = global::UnityEngine.Object.FindObjectOfType<UIRaceIcon>(true);
		}
		if (this.racePreview == null)
		{
			this.racePreview = global::UnityEngine.Object.FindObjectOfType<UIRacePreview>(true);
		}
		base.OnValidate();
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000084A5 File Offset: 0x000066A5
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.timedBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x000084B8 File Offset: 0x000066B8
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x000084C7 File Offset: 0x000066C7
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x00037DA8 File Offset: 0x00035FA8
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

	// Token: 0x0600086C RID: 2156 RVA: 0x000084D6 File Offset: 0x000066D6
	public virtual void EnterProximity()
	{
		if (this.isRepeatable && base.PersistentState)
		{
			this.racePreview.Load(this.startObject.transform, this.BestTime);
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x00008504 File Offset: 0x00006704
	public virtual void TriggerStay()
	{
		this.lastTrigger = Time.time;
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x00037E1C File Offset: 0x0003601C
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

	// Token: 0x0600086F RID: 2159 RVA: 0x00008511 File Offset: 0x00006711
	protected virtual Transform RaceGoal()
	{
		if (this.raceIconAnchor != null)
		{
			return this.raceIconAnchor;
		}
		return this.startObject.transform;
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00008533 File Offset: 0x00006733
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

	// Token: 0x06000871 RID: 2161 RVA: 0x00008557 File Offset: 0x00006757
	protected virtual void FailedRace()
	{
		if (this.raceFailedSound != null)
		{
			PlayAudio.p.Play(this.raceFailedSound, 0.5f, 1f);
		}
		this.onFail.Invoke();
		this.CancelRace();
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x00037FE0 File Offset: 0x000361E0
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

	// Token: 0x06000873 RID: 2163 RVA: 0x0003806C File Offset: 0x0003626C
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

	// Token: 0x06000874 RID: 2164 RVA: 0x00038190 File Offset: 0x00036390
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

	// Token: 0x06000875 RID: 2165 RVA: 0x00038210 File Offset: 0x00036410
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

	// Token: 0x06000876 RID: 2166 RVA: 0x00008592 File Offset: 0x00006792
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
