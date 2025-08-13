using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

// Token: 0x020001B5 RID: 437
public class TimedChallenge : PersistentObject
{
	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000825 RID: 2085 RVA: 0x00008112 File Offset: 0x00006312
	// (set) Token: 0x06000826 RID: 2086 RVA: 0x00008138 File Offset: 0x00006338
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

	// Token: 0x06000827 RID: 2087 RVA: 0x0000815A File Offset: 0x0000635A
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

	// Token: 0x06000828 RID: 2088 RVA: 0x00008196 File Offset: 0x00006396
	[ContextMenu("Gather Breakables")]
	public void GatherBreakables()
	{
		this.timedBreakables = base.transform.GetComponentsInChildren<BreakableObject>();
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x000081A9 File Offset: 0x000063A9
	public override void Load(bool state)
	{
		this.isFinished = state;
		this.LoadState();
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000081B8 File Offset: 0x000063B8
	protected virtual void Start()
	{
		this.LoadState();
		base.enabled = false;
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00036440 File Offset: 0x00034640
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

	// Token: 0x0600082C RID: 2092 RVA: 0x000081C7 File Offset: 0x000063C7
	public virtual void EnterProximity()
	{
		if (this.isRepeatable && base.PersistentState)
		{
			this.racePreview.Load(this.startObject.transform, this.BestTime);
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x000081F5 File Offset: 0x000063F5
	public virtual void TriggerStay()
	{
		this.lastTrigger = Time.time;
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000364B4 File Offset: 0x000346B4
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

	// Token: 0x0600082F RID: 2095 RVA: 0x00008202 File Offset: 0x00006402
	protected virtual Transform RaceGoal()
	{
		if (this.raceIconAnchor != null)
		{
			return this.raceIconAnchor;
		}
		return this.startObject.transform;
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x00008224 File Offset: 0x00006424
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

	// Token: 0x06000831 RID: 2097 RVA: 0x00008248 File Offset: 0x00006448
	protected virtual void FailedRace()
	{
		if (this.raceFailedSound != null)
		{
			PlayAudio.p.Play(this.raceFailedSound, 0.5f, 1f);
		}
		this.onFail.Invoke();
		this.CancelRace();
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00036678 File Offset: 0x00034878
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

	// Token: 0x06000833 RID: 2099 RVA: 0x00036704 File Offset: 0x00034904
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

	// Token: 0x06000834 RID: 2100 RVA: 0x00036828 File Offset: 0x00034A28
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

	// Token: 0x06000835 RID: 2101 RVA: 0x000368A8 File Offset: 0x00034AA8
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

	// Token: 0x06000836 RID: 2102 RVA: 0x00008283 File Offset: 0x00006483
	public void SetPlaceholderTextToFinishTime(int index)
	{
		MultilingualTextDocument.SetPlaceholder(index, this.playerFinishTime.ToString("0.00"));
	}

	// Token: 0x04000AD1 RID: 2769
	private static TimedChallenge current;

	// Token: 0x04000AD2 RID: 2770
	public UIRaceIcon raceIcon;

	// Token: 0x04000AD3 RID: 2771
	public UIRacePreview racePreview;

	// Token: 0x04000AD4 RID: 2772
	public Transform raceIconAnchor;

	// Token: 0x04000AD5 RID: 2773
	public GameObject startObject;

	// Token: 0x04000AD6 RID: 2774
	[FormerlySerializedAs("raceObjects")]
	public BreakableObject[] timedBreakables;

	// Token: 0x04000AD7 RID: 2775
	private float raceTrigger = -1f;

	// Token: 0x04000AD8 RID: 2776
	private const float raceTriggerDelay = 0.5f;

	// Token: 0x04000AD9 RID: 2777
	public float raceTime = 5f;

	// Token: 0x04000ADA RID: 2778
	[ReadOnly]
	public float playerFinishTime;

	// Token: 0x04000ADB RID: 2779
	private float finishTime;

	// Token: 0x04000ADC RID: 2780
	private float startTime;

	// Token: 0x04000ADD RID: 2781
	protected bool isRacing;

	// Token: 0x04000ADE RID: 2782
	private bool isFinished;

	// Token: 0x04000ADF RID: 2783
	public AudioClip raceStartSound;

	// Token: 0x04000AE0 RID: 2784
	public AudioClip raceFailedSound;

	// Token: 0x04000AE1 RID: 2785
	public AudioClip raceFinishedSound;

	// Token: 0x04000AE2 RID: 2786
	public GameObject isRacingSound;

	// Token: 0x04000AE3 RID: 2787
	public UnityEvent onFinish;

	// Token: 0x04000AE4 RID: 2788
	public UnityEvent onFail;

	// Token: 0x04000AE5 RID: 2789
	public UnityEvent onRepeat;

	// Token: 0x04000AE6 RID: 2790
	public bool isRepeatable;

	// Token: 0x04000AE7 RID: 2791
	public ResistCulling resistCulling;

	// Token: 0x04000AE8 RID: 2792
	private float lastTrigger = -10f;
}
