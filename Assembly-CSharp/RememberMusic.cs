using System;
using System.Collections.Generic;
using UnityEngine;

public class RememberMusic : MonoBehaviour, IManagedUpdate
{
	// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002ACE File Offset: 0x00000CCE
	// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002AED File Offset: 0x00000CED
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

	// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002B07 File Offset: 0x00000D07
	// (set) Token: 0x060000CA RID: 202 RVA: 0x00002B26 File Offset: 0x00000D26
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

	// Token: 0x060000CB RID: 203 RVA: 0x0001A3F4 File Offset: 0x000185F4
	[ContextMenu("Update Music List")]
	public void UpdateMusicList()
	{
		MusicSystemDynamicStates[] array = global::UnityEngine.Object.FindObjectsOfType<MusicSystemDynamicStates>();
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

	// Token: 0x060000CC RID: 204 RVA: 0x0001A458 File Offset: 0x00018658
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

	// Token: 0x060000CD RID: 205 RVA: 0x00002B40 File Offset: 0x00000D40
	public void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000028C1 File Offset: 0x00000AC1
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00002B4D File Offset: 0x00000D4D
	void IManagedUpdate.ManagedUpdate()
	{
		this.UpdateCurrentState();
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0001A49C File Offset: 0x0001869C
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

	// Token: 0x060000D1 RID: 209 RVA: 0x00002B55 File Offset: 0x00000D55
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
