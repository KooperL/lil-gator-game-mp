using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200002A RID: 42
public class PlaygroundMusicDynamicStates : MonoBehaviour
{
	// Token: 0x060000AB RID: 171 RVA: 0x0000530B File Offset: 0x0000350B
	private void Awake()
	{
		this.musicSystem.rightBeforePlaying.AddListener(new UnityAction(this.UpdateMusicSystem));
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00005329 File Offset: 0x00003529
	private void Start()
	{
		this.UpdateMusicSystem();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00005334 File Offset: 0x00003534
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

	// Token: 0x040000E1 RID: 225
	public MusicSystem musicSystem;

	// Token: 0x040000E2 RID: 226
	public TownNPCManager townNPCManager;
}
