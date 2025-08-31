using System;
using System.Collections.Generic;
using UnityEngine;

public class RememberMusic : MonoBehaviour, IManagedUpdate
{
	// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005438 File Offset: 0x00003638
	// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005457 File Offset: 0x00003657
	private int CurrentSongIndex
	{
		get
		{
			this._currentIndex = GameData.g.ReadInt(this.indexKey, -1);
			return this._currentIndex;
		}
		set
		{
			this._currentIndex = value;
			GameData.g.Write(this.indexKey, value);
		}
	}

	// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005471 File Offset: 0x00003671
	// (set) Token: 0x060000B6 RID: 182 RVA: 0x00005490 File Offset: 0x00003690
	private int CurrentState
	{
		get
		{
			this._currentState = GameData.g.ReadInt(this.stateKey, -1);
			return this._currentState;
		}
		set
		{
			this._currentState = value;
			GameData.g.Write(this.stateKey, value);
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x000054AC File Offset: 0x000036AC
	[ContextMenu("Update Music List")]
	public void UpdateMusicList()
	{
		MusicSystemDynamicStates[] array = Object.FindObjectsOfType<MusicSystemDynamicStates>();
		List<MusicSystemDynamicStates> list = new List<MusicSystemDynamicStates>();
		if (this.dynamicMusic != null && this.dynamicMusic.Length != 0)
		{
			list.AddRange(this.dynamicMusic);
		}
		foreach (MusicSystemDynamicStates musicSystemDynamicStates in array)
		{
			if (!list.Contains(musicSystemDynamicStates))
			{
				list.Add(musicSystemDynamicStates);
			}
		}
		this.dynamicMusic = list.ToArray();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005510 File Offset: 0x00003710
	private void Start()
	{
		int currentSongIndex = this.CurrentSongIndex;
		int currentState = this.CurrentState;
		if (currentSongIndex == -1 || currentState == -1)
		{
			return;
		}
		this.dynamicMusic[currentSongIndex].musicSystem.MarkEligible();
		this.dynamicMusic[currentSongIndex].MarkStateEligible(currentState);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00005554 File Offset: 0x00003754
	public void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005561 File Offset: 0x00003761
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000556F File Offset: 0x0000376F
	void IManagedUpdate.ManagedUpdate()
	{
		this.UpdateCurrentState();
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00005578 File Offset: 0x00003778
	private void UpdateCurrentState()
	{
		for (int i = 0; i < this.dynamicMusic.Length; i++)
		{
			if (this.dynamicMusic[i].musicSystem.isPlaying && !this.dynamicMusic[i].musicSystem.isUnloading)
			{
				this.CurrentSongIndex = i;
				this.CurrentState = this.dynamicMusic[i].currentState;
				return;
			}
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000055DB File Offset: 0x000037DB
	public AudioClip GetItemGetForMusic()
	{
		if (this._currentIndex < 0 || this._currentIndex >= this.itemGets.Length)
		{
			return null;
		}
		return this.itemGets[this._currentIndex];
	}

	public MusicSystemDynamicStates[] dynamicMusic;

	public AudioClip[] itemGets;

	public int _currentIndex;

	public int _currentState;

	public string indexKey;

	public string stateKey;
}
