using System;
using UnityEngine;
using UnityEngine.Events;

public class PlaygroundMusicDynamicStates : MonoBehaviour
{
	// Token: 0x060000BF RID: 191 RVA: 0x00002A99 File Offset: 0x00000C99
	private void Awake()
	{
		this.musicSystem.rightBeforePlaying.AddListener(new UnityAction(this.UpdateMusicSystem));
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00002AB7 File Offset: 0x00000CB7
	private void Start()
	{
		this.UpdateMusicSystem();
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0001A358 File Offset: 0x00018558
	private void UpdateMusicSystem()
	{
		bool isAveryQuestFinished = this.townNPCManager.isAveryQuestFinished;
		bool isJillQuestFinished = this.townNPCManager.isJillQuestFinished;
		bool isMartinQuestFinished = this.townNPCManager.isMartinQuestFinished;
		this.musicSystem.ClearLayerWeights();
		if (isAveryQuestFinished && isJillQuestFinished && isMartinQuestFinished)
		{
			this.musicSystem.layers[0].weight = 1f;
			return;
		}
		this.musicSystem.layers[1].weight = 1f;
		if (isMartinQuestFinished)
		{
			this.musicSystem.layers[2].weight = 1f;
		}
		if (isJillQuestFinished)
		{
			this.musicSystem.layers[3].weight = 1f;
		}
		if (isAveryQuestFinished)
		{
			this.musicSystem.layers[4].weight = 1f;
		}
	}

	public MusicSystem musicSystem;

	public TownNPCManager townNPCManager;
}
