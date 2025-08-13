using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
[AddComponentMenu("Music/Music State Manager")]
public class MusicStateManager : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000071 RID: 113 RVA: 0x000038FC File Offset: 0x00001AFC
	private void Awake()
	{
		if (MusicStateManager.m != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		MusicStateManager.m = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00003928 File Offset: 0x00001B28
	private void Start()
	{
		this.UpdateState(this.stateIndex);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00003936 File Offset: 0x00001B36
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00003943 File Offset: 0x00001B43
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00003954 File Offset: 0x00001B54
	public void ManagedUpdate()
	{
		float time = Time.time;
		int num = this.stateIndex;
		int num2 = -1;
		for (int i = 0; i < this.states.Length; i++)
		{
			if (this.states[i].isEligible && this.states[i].priority > num2)
			{
				num = i;
				num2 = this.states[i].priority;
			}
		}
		if (num != this.stateIndex)
		{
			this.UpdateState(num);
		}
		int num3 = -1;
		if (this.activeMusicSystem != null)
		{
			MusicSystem.current.TryFindIndex(this.activeMusicSystem, out num3);
		}
		int num4 = num3;
		int num5 = -1;
		for (int j = 0; j < MusicSystem.current.Count; j++)
		{
			if (MusicSystem.current[j].IsEligible && !MusicSystem.current[j].isUnloading && MusicSystem.current[j].priority > num5)
			{
				num4 = j;
				num5 = MusicSystem.current[j].priority;
			}
		}
		if (num4 != num3 || this.nextSongIndex != -1)
		{
			if (this.nextSongIndex != num4)
			{
				this.nextSongIndex = num4;
				this.nextSongTime = Time.time + this.songBufferTime;
			}
			if ((Time.time > this.nextSongTime || Time.timeSinceLevelLoad < 1f) && (this.activeMusicSystem == null || this.activeMusicSystem.masterWeightSmooth == 0f))
			{
				num3 = this.nextSongIndex;
				this.nextSongTime = -1f;
			}
			if (num3 == this.nextSongIndex)
			{
				num3 = this.nextSongIndex;
				this.nextSongIndex = -1;
				this.UpdateSong(num3);
			}
			else
			{
				this.UpdateSong(-1);
			}
		}
		if (num3 == -1)
		{
			this.activeMusicSystem = null;
		}
		else
		{
			this.activeMusicSystem = MusicSystem.current[num3];
		}
		if (this.activeMusicSystem != null)
		{
			this.activeMusicSystem.masterWeightSmoothTimeMultiplier = 1f;
		}
		if (this.isDucking)
		{
			if (time > this.duckEndTime)
			{
				this.isDucking = false;
			}
			if (this.activeMusicSystem != null)
			{
				this.activeMusicSystem.masterWeight = (this.isDucking ? 0.02f : 1f);
				this.activeMusicSystem.masterWeightSmoothTimeMultiplier = (this.isDucking ? 0.25f : 1f);
			}
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00003BB4 File Offset: 0x00001DB4
	private void UpdateState(int newActiveStateIndex)
	{
		this.stateIndex = newActiveStateIndex;
		string name = this.states[newActiveStateIndex].name;
		foreach (MusicSystemStates musicSystemStates in MusicSystemStates.current)
		{
			musicSystemStates.SetState(name);
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00003C20 File Offset: 0x00001E20
	private void UpdateSong(int songIndex)
	{
		for (int i = 0; i < MusicSystem.current.Count; i++)
		{
			if (i == songIndex)
			{
				MusicSystem.current[i].masterWeight = 1f;
			}
			else
			{
				MusicSystem.current[i].masterWeight = 0f;
			}
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00003C74 File Offset: 0x00001E74
	public void MarkState(string stateName)
	{
		for (int i = 0; i < this.states.Length; i++)
		{
			if (this.states[i].name == stateName)
			{
				this.states[i].eligibleTime = Time.time;
				return;
			}
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00003CC4 File Offset: 0x00001EC4
	public void LockState(string stateName)
	{
		for (int i = 0; i < this.states.Length; i++)
		{
			if (this.states[i].name == stateName)
			{
				this.states[i].isLocked = true;
				return;
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00003D10 File Offset: 0x00001F10
	public void ClearState(string stateName)
	{
		for (int i = 0; i < this.states.Length; i++)
		{
			if (this.states[i].name == stateName)
			{
				this.states[i].isLocked = false;
				return;
			}
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00003D5C File Offset: 0x00001F5C
	public void DuckMusic(float duration)
	{
		this.isDucking = true;
		this.duckEndTime = Time.time + duration;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00003D72 File Offset: 0x00001F72
	public void CancelDuck()
	{
	}

	// Token: 0x04000097 RID: 151
	public const float duckDelay = 0.25f;

	// Token: 0x04000098 RID: 152
	public static MusicStateManager m;

	// Token: 0x04000099 RID: 153
	public MusicStateManager.State[] states;

	// Token: 0x0400009A RID: 154
	public int stateIndex;

	// Token: 0x0400009B RID: 155
	public float isPlayerActiveSmooth;

	// Token: 0x0400009C RID: 156
	public MusicSystem activeMusicSystem;

	// Token: 0x0400009D RID: 157
	public float songBufferTime = 2f;

	// Token: 0x0400009E RID: 158
	private float nextSongTime = -1f;

	// Token: 0x0400009F RID: 159
	public int nextSongIndex;

	// Token: 0x040000A0 RID: 160
	private bool isDucking;

	// Token: 0x040000A1 RID: 161
	private float duckEndTime = -1f;

	// Token: 0x040000A2 RID: 162
	private List<string> keysToRemove = new List<string>();

	// Token: 0x040000A3 RID: 163
	private List<MusicSystem> musicSystemKeysToRemove = new List<MusicSystem>();

	// Token: 0x02000350 RID: 848
	[Serializable]
	public struct State
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000666F9 File Offset: 0x000648F9
		public bool isEligible
		{
			get
			{
				return this.isLocked || this.eligibleTime + 0.5f > Time.time;
			}
		}

		// Token: 0x040019C4 RID: 6596
		public string name;

		// Token: 0x040019C5 RID: 6597
		public float eligibleTime;

		// Token: 0x040019C6 RID: 6598
		public bool isLocked;

		// Token: 0x040019C7 RID: 6599
		public int priority;
	}

	// Token: 0x02000351 RID: 849
	[Serializable]
	public struct SongState
	{
		// Token: 0x040019C8 RID: 6600
		public string name;

		// Token: 0x040019C9 RID: 6601
		public bool isEligible;

		// Token: 0x040019CA RID: 6602
		public int priority;

		// Token: 0x040019CB RID: 6603
		public MusicSystem musicSystem;
	}
}
