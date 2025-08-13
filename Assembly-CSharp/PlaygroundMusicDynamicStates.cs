using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000034 RID: 52
public class PlaygroundMusicDynamicStates : MonoBehaviour
{
	// Token: 0x060000B7 RID: 183 RVA: 0x00002A35 File Offset: 0x00000C35
	private void Awake()
	{
		this.musicSystem.rightBeforePlaying.AddListener(new UnityAction(this.UpdateMusicSystem));
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00002A53 File Offset: 0x00000C53
	private void Start()
	{
		this.UpdateMusicSystem();
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00019AF8 File Offset: 0x00017CF8
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

	// Token: 0x04000108 RID: 264
	public MusicSystem musicSystem;

	// Token: 0x04000109 RID: 265
	public TownNPCManager townNPCManager;
}
