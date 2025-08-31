using System;
using UnityEngine;

public class ChangeMusicLoop : MonoBehaviour
{
	// Token: 0x0600005C RID: 92 RVA: 0x00003654 File Offset: 0x00001854
	public void OnEnable()
	{
		this.system.ChangeLoop(this.loopStartBeats, this.loopEndBeats, this.changeBehavior);
	}

	public MusicSystem system;

	public MusicSystem.LoopChangeBehavior changeBehavior;

	public int loopStartBeats;

	public int loopEndBeats;
}
