using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000022 RID: 34
[AddComponentMenu("Music/Music State Manager")]
public class MusicStateManager : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600006A RID: 106 RVA: 0x00002623 File Offset: 0x00000823
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

	// Token: 0x0600006B RID: 107 RVA: 0x0000264F File Offset: 0x0000084F
	private void Start()
	{
		this.UpdateState(this.stateIndex);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000265D File Offset: 0x0000085D
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000266A File Offset: 0x0000086A
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00018070 File Offset: 0x00016270
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

	// Token: 0x0600006F RID: 111 RVA: 0x000182D0 File Offset: 0x000164D0
	private void UpdateState(int newActiveStateIndex)
	{
		this.stateIndex = newActiveStateIndex;
		string name = this.states[newActiveStateIndex].name;
		foreach (MusicSystemStates musicSystemStates in MusicSystemStates.current)
		{
			musicSystemStates.SetState(name);
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0001833C File Offset: 0x0001653C
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

	// Token: 0x06000071 RID: 113 RVA: 0x00018390 File Offset: 0x00016590
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

	// Token: 0x06000072 RID: 114 RVA: 0x000183E0 File Offset: 0x000165E0
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

	// Token: 0x06000073 RID: 115 RVA: 0x0001842C File Offset: 0x0001662C
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

	// Token: 0x06000074 RID: 116 RVA: 0x00002678 File Offset: 0x00000878
	public void DuckMusic(float duration)
	{
		this.isDucking = true;
		this.duckEndTime = Time.time + duration;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00002229 File Offset: 0x00000429
	public void CancelDuck()
	{
	}

	// Token: 0x04000081 RID: 129
	public const float duckDelay = 0.25f;

	// Token: 0x04000082 RID: 130
	public static MusicStateManager m;

	// Token: 0x04000083 RID: 131
	public MusicStateManager.State[] states;

	// Token: 0x04000084 RID: 132
	public int stateIndex;

	// Token: 0x04000085 RID: 133
	public float isPlayerActiveSmooth;

	// Token: 0x04000086 RID: 134
	public MusicSystem activeMusicSystem;

	// Token: 0x04000087 RID: 135
	public float songBufferTime = 2f;

	// Token: 0x04000088 RID: 136
	private float nextSongTime = -1f;

	// Token: 0x04000089 RID: 137
	public int nextSongIndex;

	// Token: 0x0400008A RID: 138
	private bool isDucking;

	// Token: 0x0400008B RID: 139
	private float duckEndTime = -1f;

	// Token: 0x0400008C RID: 140
	private List<string> keysToRemove = new List<string>();

	// Token: 0x0400008D RID: 141
	private List<MusicSystem> musicSystemKeysToRemove = new List<MusicSystem>();

	// Token: 0x02000023 RID: 35
	[Serializable]
	public struct State
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000026CD File Offset: 0x000008CD
		public bool isEligible
		{
			get
			{
				return this.isLocked || this.eligibleTime + 0.5f > Time.time;
			}
		}

		// Token: 0x0400008E RID: 142
		public string name;

		// Token: 0x0400008F RID: 143
		public float eligibleTime;

		// Token: 0x04000090 RID: 144
		public bool isLocked;

		// Token: 0x04000091 RID: 145
		public int priority;
	}

	// Token: 0x02000024 RID: 36
	[Serializable]
	public struct SongState
	{
		// Token: 0x04000092 RID: 146
		public string name;

		// Token: 0x04000093 RID: 147
		public bool isEligible;

		// Token: 0x04000094 RID: 148
		public int priority;

		// Token: 0x04000095 RID: 149
		public MusicSystem musicSystem;
	}
}
