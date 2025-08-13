using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class ChangeMusicLoop : MonoBehaviour
{
	// Token: 0x06000055 RID: 85 RVA: 0x00002402 File Offset: 0x00000602
	public void OnEnable()
	{
		this.system.ChangeLoop(this.loopStartBeats, this.loopEndBeats, this.changeBehavior);
	}

	// Token: 0x0400006C RID: 108
	public MusicSystem system;

	// Token: 0x0400006D RID: 109
	public MusicSystem.LoopChangeBehavior changeBehavior;

	// Token: 0x0400006E RID: 110
	public int loopStartBeats;

	// Token: 0x0400006F RID: 111
	public int loopEndBeats;
}
