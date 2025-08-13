using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class ChangeMusicLoop : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x00003654 File Offset: 0x00001854
	public void OnEnable()
	{
		this.system.ChangeLoop(this.loopStartBeats, this.loopEndBeats, this.changeBehavior);
	}

	// Token: 0x04000082 RID: 130
	public MusicSystem system;

	// Token: 0x04000083 RID: 131
	public MusicSystem.LoopChangeBehavior changeBehavior;

	// Token: 0x04000084 RID: 132
	public int loopStartBeats;

	// Token: 0x04000085 RID: 133
	public int loopEndBeats;
}
