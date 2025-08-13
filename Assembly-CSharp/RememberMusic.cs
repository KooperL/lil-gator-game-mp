using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class RememberMusic : MonoBehaviour, IManagedUpdate
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000BF RID: 191 RVA: 0x00002A6A File Offset: 0x00000C6A
	// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002A89 File Offset: 0x00000C89
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

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002AA3 File Offset: 0x00000CA3
	// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002AC2 File Offset: 0x00000CC2
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

	// Token: 0x060000C3 RID: 195 RVA: 0x00019BB8 File Offset: 0x00017DB8
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

	// Token: 0x060000C4 RID: 196 RVA: 0x00019C1C File Offset: 0x00017E1C
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

	// Token: 0x060000C5 RID: 197 RVA: 0x00002ADC File Offset: 0x00000CDC
	public void OnEnable()
	{
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000285D File Offset: 0x00000A5D
	private void OnDisable()
	{
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00002AE9 File Offset: 0x00000CE9
	void IManagedUpdate.ManagedUpdate()
	{
		this.UpdateCurrentState();
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00019C60 File Offset: 0x00017E60
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

	// Token: 0x060000C9 RID: 201 RVA: 0x00002AF1 File Offset: 0x00000CF1
	public AudioClip GetItemGetForMusic()
	{
		if (this._currentIndex < 0 || this._currentIndex >= this.itemGets.Length)
		{
			return null;
		}
		return this.itemGets[this._currentIndex];
	}

	// Token: 0x0400010A RID: 266
	public MusicSystemDynamicStates[] dynamicMusic;

	// Token: 0x0400010B RID: 267
	public AudioClip[] itemGets;

	// Token: 0x0400010C RID: 268
	public int _currentIndex;

	// Token: 0x0400010D RID: 269
	public int _currentState;

	// Token: 0x0400010E RID: 270
	public string indexKey;

	// Token: 0x0400010F RID: 271
	public string stateKey;
}
